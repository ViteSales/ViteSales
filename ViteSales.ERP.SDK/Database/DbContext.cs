using SqlKata;
using SqlKata.Execution;
using ViteSales.ERP.SDK.Const;
using ViteSales.ERP.SDK.Interfaces;

namespace ViteSales.ERP.SDK.Database;

public class DbContext(Connection connectionHandler)
{
    public async Task SaveChanges(Func<List<IOperation>> callback)
    {
        await connectionHandler.OpenConnectionAsync();
        await connectionHandler.BeginTransactionAsync();
        var db = connectionHandler.DbInterface();
        try
        {
            var operations = callback.Invoke();
            foreach (var operation in operations)
            {
                var data = operation.Data().GetType();
                switch (operation.Type)
                {
                    case DbOperationTypes.Insert:
                    {
                        await db.Query(data.Name).InsertAsync(operation.Data(), connectionHandler.GetTransaction);
                        break;
                    }
                    case DbOperationTypes.Update:
                    {
                        var qu = db.Query(data.Name);
                        if (operation?.Where() != null)
                        {
                            foreach (var whc in operation.Where()!)
                            {
                                qu = qu.Where(whc.Field, whc.Operator, whc.Value);
                            }
                        }
                        await qu.UpdateAsync(operation!.Data(), connectionHandler.GetTransaction);
                        break;
                    }
                    case DbOperationTypes.Delete:
                    {
                        var qd = db.Query(data.Name);
                        if (operation?.Where() != null)
                        {
                            foreach (var whc in operation.Where()!)
                            {
                                qd = qd.Where(whc.Field, whc.Operator, whc.Value);
                            }
                        }
                        await qd.DeleteAsync(connectionHandler.GetTransaction);
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            await connectionHandler.CommitTransactionAsync();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            await connectionHandler.RollbackTransactionAsync();
            throw;
        }
        finally
        {
            await connectionHandler.CloseConnectionAsync();
        }
    }

    public Query Get<T>() where T : class
    {
        var db = connectionHandler.DbInterface();
        return db.Query(typeof(T).Name);
    }
}