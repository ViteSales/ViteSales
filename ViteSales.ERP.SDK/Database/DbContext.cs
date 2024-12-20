using SqlKata;

namespace ViteSales.ERP.SDK.Database;

public class DbContext(Connection connectionHandler)
{
    private IEnumerable<object> _changes = Array.Empty<object>();

    public DbContext Set<T>(T value) where T : class
    {
        _changes = _changes.Append(value);
        return this;
    }

    public async Task SaveChanges(Action callback)
    {
        try
        {
            await connectionHandler.OpenConnectionAsync();
            await connectionHandler.BeginTransactionAsync();
            callback.Invoke();
            await connectionHandler.CommitTransactionAsync();
        }
        catch
        {
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
        return new Query(typeof(T).Name);
    }
}