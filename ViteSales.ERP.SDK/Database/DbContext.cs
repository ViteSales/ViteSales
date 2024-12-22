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
                        await DoInsert(db, operation);
                        break;
                    }
                    case DbOperationTypes.Update:
                    {
                        await DoUpdate(db, operation);
                        break;
                    }
                    case DbOperationTypes.Delete:
                    {
                        await DoDelete(db, operation);
                        break;
                    }
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

    private async Task DoInsert(QueryFactory db, IOperation operation)
    {
        var data = operation.Data().GetType();
        var genericValidatorType = typeof(FormValidator<>).MakeGenericType(data);
        dynamic validator = Activator.CreateInstance(genericValidatorType);
        var validate = await validator.ValidateAsync((dynamic)operation.Data());
        if (!validate.IsValid)
        {
            throw new ValidationException(validate.Errors);
        }
        await db.Query(data.Name).InsertAsync(operation.Data(), _connection.GetTransaction);
    }

    private async Task DoUpdate(QueryFactory db, IOperation operation)
    {
        var data = operation.Data().GetType();
        var genericValidatorType = typeof(FormValidator<>).MakeGenericType(data);
        dynamic validator = Activator.CreateInstance(genericValidatorType);
        var validate = await validator.ValidateAsync((dynamic)operation.Data());
        if (!validate.IsValid)
        {
            throw new ValidationException(validate.Errors);
        }
        var qu = db.Query(data.Name);
        if (operation?.Where() != null)
        {
            foreach (var whc in operation.Where()!)
            {
                qu = qu.Where(whc.Field, whc.Operator, whc.Value);
            }
        }
        await qu.UpdateAsync(operation!.Data(), _connection.GetTransaction);
    }

    private async Task DoDelete(QueryFactory db, IOperation operation)
    {
        var data = operation.Data().GetType();
        var qd = db.Query(data.Name);
        if (operation?.Where() != null)
        {
            foreach (var whc in operation.Where()!)
            {
                qd = qd.Where(whc.Field, whc.Operator, whc.Value);
            }
        }
        await qd.DeleteAsync(_connection.GetTransaction);
    }

    public async Task<Query> Get<T>() where T : class
    {
        await _connection.OpenConnectionAsync();
        var db = _connection.SqlCompiler();
        return db.Query(typeof(T).Name);
    }
}