using FluentValidation;
using SqlKata;
using SqlKata.Execution;
using ViteSales.ERP.SDK.Const;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Models;
using ViteSales.ERP.SDK.Validator;

namespace ViteSales.ERP.SDK.Database;

public class DbContext(ConnectionConfig config)
{
    private readonly Connection _connection = new (config);
    public async Task SaveChanges(Func<List<IOperation>> callback)
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
                        await DoUpdate(operation);
                        break;
                    }
                    case DbOperationTypes.Delete:
                    {
                        await DoDelete(operation);
                        break;
                    }
                    case DbOperationTypes.Upsert:
                        await DoUpsert(operation);
                        break;
                    default:
                        throw new NotImplementedException("Only Insert, Update and Delete operations are supported.");
                }
            }
            await _connection.CommitTransactionAsync();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            await _connection.RollbackTransactionAsync();
            throw;
        }
        finally
        {
            await _connection.CloseConnectionAsync();
        }
    }

    public async Task<bool> IsTableExist(string tableName)
    {
        await _connection.OpenConnectionAsync();
        return await _connection.TableExistsAsync(tableName);
    }

    public async Task<bool> IsColumnExist(string tableName, string columnName)
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
            await _connection.UpdateAsync(operation);
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

    public async Task<Query> Get<T>() where T : class
    {
        await _connection.OpenConnectionAsync();
        var db = _connection.SqlCompiler();
        return db.Query(typeof(T).Name);
    }
}