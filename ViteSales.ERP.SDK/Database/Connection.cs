using Npgsql;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.SDK.Database;

/// <summary>
/// Initializes a new instance of the <see cref="Connection"/> class.
/// </summary>
/// <param name="config">The PostgreSQL connection config.</param>
public class Connection(ConnectionConfig config): IDisposable
{
    private NpgsqlConnection _connection = new ($"UserID={config.User};Password={config.Password};Host={config.Host};Port={config.Port};Database={config.Database};Pooling=true;Minimum Pool Size=1;Maximum Pool Size=20;");
    private NpgsqlTransaction? _transaction = null;
    private bool _disposed = false;
    
    /// <summary>
    /// Opens a new PostgreSQL connection asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous open operation.</returns>
    public async Task OpenConnectionAsync()
    {
        if (_connection.State != System.Data.ConnectionState.Open)
        {
            await _connection.OpenAsync();
        }
    }

    /// <summary>
    /// Closes the PostgreSQL connection asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous close operation.</returns>
    public async Task CloseConnectionAsync()
    {
        if (_connection.State != System.Data.ConnectionState.Closed)
        {
            await _connection.CloseAsync();
        }
    }

    /// <summary>
    /// Begins a new transaction asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous begin transaction operation.</returns>
    public async Task BeginTransactionAsync()
    {
        if (_connection == null)
        {
            throw new InvalidOperationException("Connection must be opened before beginning a transaction.");
        }

        if (_transaction == null)
        {
            _transaction = await _connection.BeginTransactionAsync();
        }
        else
        {
            throw new InvalidOperationException("A transaction is already in progress.");
        }
    }

    /// <summary>
    /// Commits the current transaction asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous commit operation.</returns>
    public async Task CommitTransactionAsync()
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction to commit.");
        }

        await _transaction.CommitAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    /// <summary>
    /// Rolls back the current transaction asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous rollback operation.</returns>
    public async Task RollbackTransactionAsync()
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction to rollback.");
        }

        await _transaction.RollbackAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }
    
    public NpgsqlTransaction? GetTransaction => _transaction;

    /// <summary>
    /// Creates a new PostgreSQL command associated with the current connection and transaction.
    /// </summary>
    /// <returns>A new instance of <see cref="NpgsqlCommand"/>.</returns>
    public NpgsqlCommand CreateCommand()
    {
        if (_connection == null)
        {
            throw new InvalidOperationException("Connection must be opened before creating commands.");
        }

        var command = _connection.CreateCommand();
        if (_transaction != null)
        {
            command.Transaction = _transaction;
        }

        return command;
    }

    /// <summary>
    /// Disposes the connection handler and releases all resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes the managed and unmanaged resources.
    /// </summary>
    /// <param name="disposing">Indicates whether to dispose managed resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Dispose managed resources
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
                _connection.Dispose();
            }
            _disposed = true;
        }
    }
    
    public QueryFactory DbInterface()
    {
        if (_connection is not { State: System.Data.ConnectionState.Open })
            throw new InvalidOperationException("Connection must be opened before creating commands.");
        var db = new QueryFactory(_connection, new PostgresCompiler());
        db.Logger = compiled => {
            Console.WriteLine(compiled.ToString());
        };
        return db;
    }

    /// <summary>
    /// Destructor to ensure resources are released.
    /// </summary>
    ~Connection()
    {
        Dispose(false);
    }
}