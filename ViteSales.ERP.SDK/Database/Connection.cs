using System.Data;
using System.Reflection;
using System.Text.Json;
using Npgsql;
using NpgsqlTypes;
using SqlKata.Compilers;
using SqlKata.Execution;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Const;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.MessageQueue;
using ViteSales.ERP.SDK.Models;
using ViteSales.ERP.SDK.Utils;
using ViteSales.Shared.Exceptions;
using ViteSales.Shared.Extensions;

namespace ViteSales.ERP.SDK.Database;

/// <summary>
/// Initializes a new instance of the <see cref="Connection"/> class.
/// </summary>
/// <param name="config">The PostgreSQL connection config.</param>
internal sealed class Connection(ConnectionConfig config): IDisposable
{
    private readonly NpgsqlConnection _connection = new(CreateConnectionString(config));
    private NpgsqlTransaction? _transaction = null;
    private PubSub _pubSub = new();
    private bool _disposed = false;
    
    /// <summary>
    /// Opens a new PostgreSQL connection asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous open operation.</returns>
    public async Task OpenConnectionAsync()
    {
        if (_connection.State != ConnectionState.Open)
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
        if (_connection.State != ConnectionState.Closed)
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
            var bindType = property.GetCustomAttribute<BindDataTypeAttribute>();
            if (bindType is null) continue;
            
            var propertyValue = property.GetValue(data);
            if (propertyValue is null) continue;
            
            queueData.Add(property.Name, propertyValue.ToObjectInferred());
        }

        var queueName = Utility.QueueName(config.Host, config.Database, tableName);
        var message = new PubSubMessage
        {
            QueuedBy = config.User,
            QueuedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            Action = operationType.ToString(),
            Data = JsonSerializer.Serialize(queueData)
        };
        await _pubSub.PublishAsync(queueName, message);
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
        var queueName = Utility.QueueName(config.Host, config.Database, tableName);
        return await _pubSub.InitTopicAsync(queueName);
    }

    public async Task InsertAsync(IOperation operation)
    {
        var data = operation.Data();
        var type = data.GetType();
        var tableName = type.Name;
        var typeProperties = type.GetProperties();
        var parameterDbTypes = new List<NpgsqlDbTypeWithValue>();
        var columns = new List<string>();
        foreach (var property in typeProperties)
        {
            var bindType = property.GetCustomAttribute<BindDataTypeAttribute>();
            if (bindType is null) continue;
            
            var propertyValue = property.GetValue(data);
            if (propertyValue is null) continue;
            
            var dbColumnType = bindType.Type.GetPostgresDbType();
            parameterDbTypes.Add(new NpgsqlDbTypeWithValue()
            {
                Parameter = $"@{property.Name}",
                DbType = dbColumnType,
                Value = propertyValue.ToObjectInferred()
            });
            columns.Add($"\"{property.Name}\"");
        }
        var cmd = CreateCommand();
        cmd.CommandText = $"INSERT INTO \"{tableName}\" ({string.Join(",", columns)}) VALUES ({string.Join(",", parameterDbTypes.Select(x => x.Parameter))});";
        Console.WriteLine(cmd.CommandText);
        foreach (var dbType in parameterDbTypes)
        {
            cmd.Parameters.AddWithValue(dbType.Parameter, dbType.DbType, dbType.Value);
        }
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task UpdateAsync(IOperation operation)
    {
        var type = operation.Data().GetType();
        var tableName = type.Name;
        var typeProperties = type.GetProperties();
        var parameterDbTypes = new Dictionary<string, NpgsqlDbTypeWithValue>();
        var whereParameterDbTypes = operation.Where();
        if (whereParameterDbTypes is null)
        {
            throw new ArgumentNullException(nameof(whereParameterDbTypes));
        }
        foreach (var property in typeProperties)
        {
            var bindType = property.GetCustomAttribute<BindDataTypeAttribute>();
            if (bindType is null) continue;

            var instance = Activator.CreateInstance(property.DeclaringType ?? throw new InvalidOperationException("Property does not have a declaring type"));
            var propertyValue = property.GetValue(instance);
            if (propertyValue is null) continue;

            var dbColumnType = bindType.Type.GetPostgresDbType();
            parameterDbTypes.Add(property.Name, new NpgsqlDbTypeWithValue()
            {
                Parameter = $"@{property.Name}",
                DbType = dbColumnType,
                Value = propertyValue.ToObjectInferred()
            });
        }

        var (whereClause, parameters) = whereParameterDbTypes.Build();

        var sql = $"UPDATE \"{tableName}\" SET";
        var values = parameterDbTypes.Select(kv => $" \"{kv.Key}\" = {kv.Value.Parameter}").ToList();
        sql += string.Join(",", values);
        sql += $" WHERE {whereClause}";
        
        var cmd = CreateCommand();
        cmd.CommandText = sql;
        Console.WriteLine(cmd.CommandText);
        foreach (var dbType in parameters)
        {
            cmd.Parameters.AddWithValue(dbType.Key, dbType.Value.DbType, dbType.Value.Value);
        }

        foreach (var dbType in parameterDbTypes)
        {
            cmd.Parameters.AddWithValue(dbType.Value.Parameter, dbType.Value.DbType, dbType.Value.Value);
        }
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(IOperation operation)
    {
        var type = operation.Data().GetType();
        var tableName = type.Name;
        var whereParameterDbTypes = operation.Where();
        if (whereParameterDbTypes is null)
        {
            throw new ArgumentNullException(nameof(whereParameterDbTypes));
        }
        var (whereClause, parameters) = whereParameterDbTypes.Build();
        
        var sql = $"DELETE FROM \"{tableName}\" WHERE {whereClause}";
        
        var cmd = CreateCommand();
        cmd.CommandText = sql;
        Console.WriteLine(cmd.CommandText);
        foreach (var dbType in parameters)
        {
            cmd.Parameters.AddWithValue(dbType.Key, dbType.Value.DbType, dbType.Value.Value);
        }
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<bool> RecordExistsAsync(IOperation operation)
    {
        var type = operation.Data().GetType();
        var tableName = type.Name;
        var whereParameterDbTypes = operation.Where();
        if (whereParameterDbTypes is null)
        {
            throw new ArgumentNullException(nameof(whereParameterDbTypes));
        }
        var (whereClause, parameters) = whereParameterDbTypes.Build();
        
        var sql = $"SELECT * FROM \"{tableName}\" WHERE {whereClause}";
        
        var cmd = CreateCommand();
        cmd.CommandText = sql;
        Console.WriteLine(cmd.CommandText);
        foreach (var dbType in parameters)
        {
            cmd.Parameters.AddWithValue(dbType.Key, dbType.Value.DbType, dbType.Value.Value);
        }
        
        var reader = await cmd.ExecuteReaderAsync();
        var hasRows = reader.HasRows;
        await reader.CloseAsync();
        return hasRows;
    }

    public async Task<DataTable> GetRecordsAsync(IOperation operation)
    {
        var type = operation.Data().GetType();
        var tableName = type.Name;
        var whereParameterDbTypes = operation.Where();
        if (whereParameterDbTypes is null)
        {
            throw new ArgumentNullException(nameof(whereParameterDbTypes));
        }
        var (whereClause, parameters) = whereParameterDbTypes.Build();
        
        var sql = $"SELECT * FROM \"{tableName}\" WHERE {whereClause}";
        
        var cmd = CreateCommand();
        cmd.CommandText = sql;
        Console.WriteLine(cmd.CommandText);
        foreach (var dbType in parameters)
        {
            cmd.Parameters.AddWithValue(dbType.Key, dbType.Value.DbType, dbType.Value.Value);
        }
        
        var reader = await cmd.ExecuteReaderAsync();
        var dataTable = new DataTable();
        dataTable.Load(reader);
        await reader.CloseAsync();
        return dataTable;
    }
    
    /// <summary>
    /// Checks if a column exists in a table.
    /// </summary>
    /// <param name="tableName">The name of the table to check.</param>
    /// <param name="columnName">The name of the column to check.</param>
    /// <returns>A boolean indicating whether the column exists or not.</returns>
    public async Task<bool> ColumnExistsAsync(string tableName, string columnName)
    {
        var query = @"SELECT EXISTS (
                          SELECT 1 
                          FROM information_schema.columns 
                          WHERE table_name = @tableName 
                          AND column_name = @columnName
                      );";
        
        var cmd = CreateCommand();
        cmd.CommandText = query;
        cmd.Parameters.AddWithValue("@tableName", NpgsqlDbType.Text, tableName);
        cmd.Parameters.AddWithValue("@columnName", NpgsqlDbType.Text, columnName);

        var result = await cmd.ExecuteScalarAsync();
        return result is bool exists && exists;
    }
    
    /// <summary>
    /// Checks if a table exists in the database.
    /// </summary>
    /// <param name="tableName">The name of the table to check.</param>
    /// <returns>A boolean indicating whether the table exists or not.</returns>
    public async Task<bool> TableExistsAsync(string tableName)
    {
        var query = "SELECT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = @tableName);";
        var cmd = CreateCommand();
        cmd.CommandText = query;
        cmd.Parameters.AddWithValue("@tableName", NpgsqlDbType.Text, tableName);

        var result = await cmd.ExecuteScalarAsync();
        return result is bool exists && exists;
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
    private void Dispose(bool disposing)
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
    
    public QueryFactory SqlCompiler()
    {
        if (_connection is not { State: ConnectionState.Open })
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
    
    private static string CreateConnectionString(ConnectionConfig config)
    {
        return $"UserID={config.User};Password={config.Password};Host={config.Host};Port={config.Port};Database={config.Database};Pooling=true;Minimum Pool Size=1;Maximum Pool Size=20;Include Error Detail=true;";
    }
}