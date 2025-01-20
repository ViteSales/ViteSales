using System.Data;
using System.Reflection;
using System.Text.Json;
using FluentValidation;
using Microsoft.Extensions.Options;
using SqlKata;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Const;
using ViteSales.ERP.SDK.Database.Operation;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Internal.Core.Entities;
using ViteSales.ERP.SDK.Models;
using ViteSales.ERP.SDK.Services.MessageQueue;
using ViteSales.ERP.SDK.Utils;
using ViteSales.ERP.SDK.Validator;
using ViteSales.ERP.Shared.Exceptions;
using ViteSales.ERP.Shared.Extensions;
using ViteSales.ERP.Shared.Models;

namespace ViteSales.ERP.SDK.Database;

public class DbContext: IDbContext
{
    private readonly Connection _connection;
    private readonly ConnectionConfig _config;
    private readonly IPubSubService _pubSubService;
    
    public DbContext(IPubSubService pubSubService, IOptions<ConnectionConfig> cfg)
    {
        ArgumentNullException.ThrowIfNull(cfg.Value);
        _config = cfg.Value;
        _connection = new Connection(cfg.Value);
        _pubSubService = pubSubService;
    }
    
    public async Task SaveChangesAsync(Func<List<IOperation>> callback)
    {
        await _connection.OpenConnectionAsync();
        await _connection.BeginTransactionAsync();
        var db = _connection.SqlCompiler();
        try
        {
            var operations = callback.Invoke();
            foreach (var operation in from operation in operations let data = operation.Data().GetType() select operation)
            {
                switch (operation.Type)
                {
                    case DbOperationTypes.Insert:
                    {
                        await DoInsert(operation);
                        break;
                    }
                    case DbOperationTypes.Update:
                    {
                        var auditableRecords = await GetAuditableRecords(operation);
                        await DoUpdate(operation);
                        await DoAudit(auditableRecords, DbOperationTypes.Update);
                        break;
                    }
                    case DbOperationTypes.Delete:
                    {
                        var auditableRecords = await GetAuditableRecords(operation);
                        await DoDelete(operation);
                        await DoAudit(auditableRecords, DbOperationTypes.Delete);
                        break;
                    }
                    case DbOperationTypes.Upsert:
                        await DoUpsert(operation);
                        break;
                    default:
                        throw new NotImplementedException("Only Insert, Update and Delete operations are supported.");
                }
                await QueueMessageAsync(operation);
            }
            await _connection.CommitTransactionAsync();
        }
        catch(Exception ex)
        {
            await _connection.RollbackTransactionAsync();
            throw;
        }
        finally
        {
            await _connection.CloseConnectionAsync();
        }
    }

    public async Task<bool> IsTableExistAsync(string tableName)
    {
        await _connection.OpenConnectionAsync();
        return await _connection.TableExistsAsync(tableName);
    }

    public async Task<bool> IsColumnExistAsync(string tableName, string columnName)
    {
        await _connection.OpenConnectionAsync();
        return await _connection.ColumnExistsAsync(tableName, columnName);
    }

    private async Task DoInsert(IOperation operation)
    {
        var data = operation.Data().GetType();
        var genericValidatorType = typeof(FormValidator<>).MakeGenericType(data);
        dynamic validator = Activator.CreateInstance(genericValidatorType);
        var validate = await validator.ValidateAsync((dynamic)operation.Data());
        if (!validate.IsValid)
        {
            throw new ValidationException(validate.Errors);
        }
        await _connection.InsertAsync(operation);
    }

    private async Task DoUpdate(IOperation operation)
    {
        var data = operation.Data().GetType();
        var genericValidatorType = typeof(FormValidator<>).MakeGenericType(data);
        dynamic validator = Activator.CreateInstance(genericValidatorType);
        var validate = await validator.ValidateAsync((dynamic)operation.Data());
        if (!validate.IsValid)
        {
            throw new ValidationException(validate.Errors);
        }

        await _connection.UpdateAsync(operation);
    }

    private async Task DoUpsert(IOperation operation)
    {
        var data = operation.Data().GetType();
        var genericValidatorType = typeof(FormValidator<>).MakeGenericType(data);
        dynamic validator = Activator.CreateInstance(genericValidatorType);
        var validate = await validator.ValidateAsync((dynamic)operation.Data());
        if (!validate.IsValid)
        {
            throw new ValidationException(validate.Errors);
        }

        if (await _connection.RecordExistsAsync(operation))
        {
            var auditableRecords = await GetAuditableRecords(operation);
            await _connection.UpdateAsync(operation);
            await DoAudit(auditableRecords, DbOperationTypes.Update);
        }
        else
        {
            await _connection.InsertAsync(operation);
        }
    }

    private async Task DoDelete(IOperation operation)
    {
        await _connection.DeleteAsync(operation);
    }

    private async Task DoAudit(DataTable? dt, DbOperationTypes operationTypes)
    {
        if (dt is null) return;
        foreach (DataRow row in dt.Rows)
        {
            var primaryKeyColumn = dt.PrimaryKey?.FirstOrDefault();
            var primaryKeyValue = primaryKeyColumn != null ? row[primaryKeyColumn] : null;
            if (primaryKeyValue is not null)
            {
                await _connection.InsertAsync(new Insert<AuditTrailInternal>(new AuditTrailInternal
                {
                    Action = operationTypes.ToAction(),
                    ActionAt = DateTime.Now,
                    ActionBy = _config.User,
                    Module = dt.TableName,
                    Data = JsonSerializer.Serialize(row),
                    DataId = primaryKeyValue.ToString()!,
                }));
            }
        }
    }

    private async Task<DataTable?> GetAuditableRecords(IOperation operation)
    {
        var type = operation.Data().GetType();
        var tableName = type.Name;
        if (tableName.Contains("Internal"))
            return null;
        var typeProperties = type.GetProperties();
        foreach (var property in typeProperties)
        {
            var bindType = property.GetCustomAttribute<FieldDataTypeAttribute>();
            if (bindType is null) continue;
            var primaryKeyType = property.GetCustomAttribute<PrimaryKeyAttribute>();
            if (primaryKeyType is not null)
            {
                var dt = await _connection.GetRecordsAsync(operation);
                dt.PrimaryKey = [new DataColumn(property.Name, property.PropertyType)];
                return dt;
            }
        }
        return null;
    }

    public async Task<Query> GetAsync<T>() where T : class
    {
        await _connection.OpenConnectionAsync();
        var db = _connection.SqlCompiler();
        return db.Query(typeof(T).Name);
    }
    
    public async Task<DataTable> GetRecordsAsync(IOperation operation) => await _connection.GetRecordsAsync(operation);
    
    public async Task QueueMessageAsync(IOperation operation)
    {
        var data = operation.Data();
        var type = data.GetType();
        var tableName = type.Name;
        var isMqStreamEnabled = type.GetCustomAttribute<MqStreamAttribute>();
        if (isMqStreamEnabled is null)
        {
            return;
        }
        var typeProperties = type.GetProperties();
        var operationType = operation.Type;
        var queueData = new Dictionary<string, object>();
        foreach (var property in typeProperties)
        {
            var bindType = property.GetCustomAttribute<FieldDataTypeAttribute>();
            if (bindType is null) continue;
            
            var propertyValue = property.GetValue(data);
            if (propertyValue is null) continue;
            
            queueData.Add(property.Name, propertyValue.ToObjectInferred());
        }

        var message = new PubSubMessage
        {
            QueuedBy = _config.User,
            QueuedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            Action = operationType.ToString(),
            Data = JsonSerializer.Serialize(queueData)
        };
        await _pubSubService.PublishAsync(tableName, message);
    }
    
    public async Task<Subscriber> ListenMessage<T>() where T: class
    {
        var type = typeof(T);
        var tableName = type.Name;
        var isMqStreamEnabled = type.GetCustomAttribute<MqStreamAttribute>();
        if (isMqStreamEnabled is null)
        {
            throw new StreamingException<string>("Streaming is not enabled for this module.");
        }
        return await _pubSubService.InitTopicAsync(tableName);
    }
}