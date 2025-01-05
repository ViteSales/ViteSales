using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Const;
using ViteSales.ERP.SDK.Database;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Models;
using ViteSales.ERP.SDK.Services.MessageQueue;
using ViteSales.ERP.SDK.Utils;

namespace ViteSales.ERP.SDK.Services;

public class TableSchemaService: ITableSchemaManager
{
    private readonly ConnectionConfig _config;
    private readonly Connection _connectionHandler;
    private readonly ILogger<TableSchemaService> _logger;
    private readonly IPubSub _pubSub;

    public TableSchemaService(IPubSub pubSub, IOptions<ConnectionConfig> cfg,ILogger<TableSchemaService> log)
    {
        ArgumentNullException.ThrowIfNull(cfg.Value);
        ArgumentNullException.ThrowIfNull(log);
        
        _logger = log;
        _config = cfg.Value;
        _connectionHandler = new Connection(cfg.Value);
        _pubSub = pubSub;
    }
    
    public async Task DropTablesAsync(IEnumerable<Type> types)
    {
        _logger.LogDebug("Starting to drop tables.");

        try
        {
            await _connectionHandler.OpenConnectionAsync();
            _logger.LogDebug("Database connection opened.");
            
            await _connectionHandler.BeginTransactionAsync();
            _logger.LogDebug("Transaction started.");

            foreach (var type in types)
            {
                var tableName = type.Name;
                _logger.LogDebug("Dropping table \"{TableName}\".", tableName);
                
                await using var cmd = _connectionHandler.CreateCommand();
                cmd.CommandText = $"DROP TABLE IF EXISTS \"{tableName}\" CASCADE;";
                await cmd.ExecuteNonQueryAsync();
                _logger.LogDebug("Table \"{TableName}\" dropped successfully.", tableName);
                
                await _pubSub.Drop(GetQueueName(tableName));
                _logger.LogDebug("Queue for table \"{TableName}\" dropped successfully.", tableName);
            }

            await _connectionHandler.CommitTransactionAsync();
            _logger.LogInformation("Transaction committed successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while dropping tables. Rolling back transaction.");
            await _connectionHandler.RollbackTransactionAsync();
            throw;
        }
        finally
        {
            await _connectionHandler.CloseConnectionAsync();
            _logger.LogDebug("Database connection closed.");
        }

        _logger.LogInformation("Finished dropping tables.");
    }
    
    public async Task CreateOrUpdateTablesAsync(IEnumerable<Type> types)
    {
        _logger.LogDebug("Start processing CreateOrUpdateTablesAsync.");

        var tables = new List<TableInfo>();
        var requiresHstore = false;
        var requiresPostgis = false;
        var allUniqueIndexes = new List<UniqueIndexInfo>();
        var allIndexes = new List<IndexInfo>();
        var allRelationalMappings = new List<RelationalMappingInfo>();

        foreach (var type in types)
        {
            var tableName = type.Name;
            _logger.LogDebug("Processing table schema for \"{TableName}\".", tableName);
            var properties = type.GetProperties();

            var columns = new List<ColumnInfo>();
            var uniqueColumns = new List<string>();

            var streamAttr = type.GetCustomAttribute<MqStreamAttribute>();
            if (streamAttr != null)
            {
                _logger.LogDebug("Initializing queue topic for \"{TableName}\".", tableName);
                await _pubSub.InitTopicAsync(GetQueueName(tableName));
            }

            foreach (var prop in properties)
            {
                bool isPrimaryKey = false, isNullable = false;
                var dataType = FieldTypes.Text.GetPostgresColumnType();
                var columnName = prop.Name;
                var bindAttr = prop.GetCustomAttribute<BindDataTypeAttribute>();

                // Log property being processed
                _logger.LogTrace("Processing property \"{PropertyName}\" of table \"{TableName}\".", prop.Name, tableName);

                // Handle RelationalMappingAttribute
                var relationalAttrs = prop.GetCustomAttributes<RelationalMappingAttribute>();
                allRelationalMappings.AddRange(relationalAttrs.Select(relAttr => new RelationalMappingInfo { ToTable = relAttr.ToTable, FromColumn = relAttr.FromColumn, ToColumn = relAttr.ToColumn, FromTable = tableName }));

                if (typeof(System.Collections.IEnumerable).IsAssignableFrom(prop.PropertyType) && prop.PropertyType != typeof(string))
                {
                    if (!(prop.PropertyType == typeof(JsonArray) ||
                          prop.PropertyType == typeof(JsonObject) ||
                          typeof(JsonElement).IsAssignableFrom(prop.PropertyType)))
                    {
                        _logger.LogTrace("Skipped processing collection property \"{PropertyName}\" of table \"{TableName}\".", prop.Name, tableName);
                        continue;
                    }
                }

                if (bindAttr != null)
                {
                    dataType = bindAttr.Type.GetPostgresColumnType();
                    isPrimaryKey = prop.GetCustomAttribute<PrimaryKeyAttribute>() != null;
                    isNullable = prop.GetCustomAttribute<RequiredAttribute>() == null;

                    switch (bindAttr.Type)
                    {
                        case FieldTypes.Hstore:
                            requiresHstore = true;
                            break;
                        case FieldTypes.Geography:
                            requiresPostgis = true;
                            break;
                        case FieldTypes.AutoNumber:
                            dataType = isPrimaryKey ? "SERIAL PRIMARY KEY" : "SERIAL";
                            break;
                    }
                }

                columns.Add(new ColumnInfo
                {
                    Name = columnName,
                    DataType = dataType,
                    IsPrimaryKey = isPrimaryKey,
                    IsNullable = isNullable
                });

                // Handle UniqueColumnAttribute
                if (prop.GetCustomAttribute<UniqueKeyAttribute>() != null)
                {
                    uniqueColumns.Add(columnName);
                }

                // Handle IndexColumnAttribute
                var indexAttr = prop.GetCustomAttribute<IndexColumnAttribute>();
                if (indexAttr != null)
                {
                    allIndexes.Add(new IndexInfo
                    {
                        TableName = tableName,
                        ColumnName = columnName,
                        Direction = indexAttr.Direction
                    });
                }
            }

            if (uniqueColumns.Any())
            {
                allUniqueIndexes.Add(new UniqueIndexInfo
                {
                    TableName = tableName,
                    Columns = [..uniqueColumns]
                });
            }

            tables.Add(new TableInfo
            {
                TableName = tableName,
                Columns = columns
            });
        }

        try
        {
            _logger.LogInformation("Opening database connection.");
            await _connectionHandler.OpenConnectionAsync();
            await _connectionHandler.BeginTransactionAsync();

            if (requiresHstore)
            {
                _logger.LogDebug("Ensuring HSTORE extension is present.");
                await EnsureHstoreExtensionAsync();
            }

            if (requiresPostgis)
            {
                _logger.LogDebug("Ensuring PostGIS extension is present.");
                await EnsurePostgisExtensionAsync();
            }

            foreach (var table in tables)
            {
                _logger.LogDebug("Processing table \"{TableName}\".", table.TableName);

                var tableExists = await TableExistsAsync(table.TableName);
                if (!tableExists)
                {
                    _logger.LogInformation("Creating table \"{TableName}\".", table.TableName);
                    var createTableCmd = GenerateCreateTableCommand(table.TableName, table.Columns);
                    await using var cmd = _connectionHandler.CreateCommand();
                    cmd.CommandText = createTableCmd;
                    await cmd.ExecuteNonQueryAsync();
                }
                else
                {
                    _logger.LogInformation("Updating table \"{TableName}\".", table.TableName);
                    var existingColumns = await GetExistingColumnsAsync(table.TableName);
                    var newColumns = table.Columns.Where(c => !existingColumns.Contains(c.Name.ToLower())).ToList();
                    foreach (var addColumnCmd in newColumns.Select(column => GenerateAddColumnCommand(table.TableName, column)))
                    {
                        _logger.LogDebug("Adding column to table \"{TableName}\".", table.TableName);
                        await using var cmd = _connectionHandler.CreateCommand();
                        cmd.CommandText = addColumnCmd;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }

            // Create Unique Indexes
            foreach (var createUniqueIndexCmd in from uniqueIndex in allUniqueIndexes let indexName = $"{uniqueIndex.TableName}_unique_{string.Join("_", uniqueIndex.Columns)}" let columnsList = string.Join(", ", uniqueIndex.Columns.Select(c => $"\"{c}\"")) select $"CREATE UNIQUE INDEX IF NOT EXISTS \"{indexName}\" ON \"{uniqueIndex.TableName}\" ({columnsList});")
            {
                _logger.LogDebug("Creating unique index with command: {Command}.", createUniqueIndexCmd);
                await using var uniqueCmd = _connectionHandler.CreateCommand();
                uniqueCmd.CommandText = createUniqueIndexCmd;
                await uniqueCmd.ExecuteNonQueryAsync();
            }

            // Create Indexes
            foreach (var createIndexCmd in from index in allIndexes let indexName = $"{index.TableName}_{index.ColumnName}_idx" select $"CREATE INDEX IF NOT EXISTS \"{indexName}\" ON \"{index.TableName}\" (\"{index.ColumnName}\" {index.Direction});")
            {
                _logger.LogDebug("Creating index with command: {Command}.", createIndexCmd);
                await using var idxCmd = _connectionHandler.CreateCommand();
                idxCmd.CommandText = createIndexCmd;
                await idxCmd.ExecuteNonQueryAsync();
            }

            // Create Foreign Keys
            foreach (var rel in allRelationalMappings)
            {
                var fkName = $"fk_{rel.FromColumn}_{rel.ToColumn}_{rel.FromTable}_{rel.ToTable}";
                if (await ConstraintsExistsAsync(fkName)) 
                {
                    _logger.LogTrace("Foreign key \"{FKName}\" already exists.", fkName);
                    continue;
                }

                var addFkCmd = $"ALTER TABLE \"{rel.ToTable}\" ADD CONSTRAINT \"{fkName}\" FOREIGN KEY (\"{rel.ToColumn}\") REFERENCES \"{rel.FromTable}\"(\"{rel.FromColumn}\");";
                _logger.LogDebug("Adding foreign key with command: {Command}.", addFkCmd);
                await using var fkCmd = _connectionHandler.CreateCommand();
                fkCmd.CommandText = addFkCmd;
                await fkCmd.ExecuteNonQueryAsync();
            }

            _logger.LogInformation("Committing database transaction.");
            await _connectionHandler.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in CreateOrUpdateTablesAsync. Rolling back transaction.");
            await _connectionHandler.RollbackTransactionAsync();
            throw;
        }
        finally
        {
            _logger.LogInformation("Closing database connection.");
            await _connectionHandler.CloseConnectionAsync();
        }

        _logger.LogInformation("Finished processing CreateOrUpdateTablesAsync.");
    }

    private async Task EnsureHstoreExtensionAsync()
    {
        const string cmdText = "CREATE EXTENSION IF NOT EXISTS hstore;";
        _logger.LogDebug("Executing query to ensure HSTORE extension: {CommandText}", cmdText);
        await using var cmd = _connectionHandler.CreateCommand();
        cmd.CommandText = cmdText;
        try
        {
            await cmd.ExecuteNonQueryAsync();
            _logger.LogInformation("HSTORE extension ensured successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to ensure HSTORE extension.");
            throw;
        }
    }

    private async Task EnsurePostgisExtensionAsync()
    {
        const string cmdText = "CREATE EXTENSION IF NOT EXISTS postgis;";
        _logger.LogDebug("Executing query to ensure PostGIS extension: {CommandText}", cmdText);
        await using var cmd = _connectionHandler.CreateCommand();
        cmd.CommandText = cmdText;
        try
        {
            await cmd.ExecuteNonQueryAsync();
            _logger.LogInformation("PostGIS extension ensured successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to ensure PostGIS extension.");
            throw;
        }
    }

    private async Task<bool> TableExistsAsync(string tableName)
    {
        _logger.LogDebug("Checking if table exists: {TableName}", tableName);
        await using var cmd = _connectionHandler.CreateCommand();
        cmd.CommandText = @"
            SELECT EXISTS (
                SELECT FROM information_schema.tables 
                WHERE table_schema = 'public' 
                AND table_name = @table
            );";
        cmd.Parameters.AddWithValue("table", tableName);
        try
        {
            var exists = (bool)(await cmd.ExecuteScalarAsync() ?? false);
            _logger.LogDebug("Table \"{TableName}\" exists: {Exists}", tableName, exists);
            return exists;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while checking if table \"{TableName}\" exists.", tableName);
            throw;
        }
    }

    private async Task<bool> ConstraintsExistsAsync(string fkName)
    {
        _logger.LogDebug("Checking if constraint exists: {FKName}", fkName);
        await using var cmd = _connectionHandler.CreateCommand();
        cmd.CommandText = @"
            SELECT EXISTS(
                SELECT 1 FROM pg_constraint WHERE conname = @fkName
            );";
        cmd.Parameters.AddWithValue("fkName", fkName);
        try
        {
            var exists = (bool)(await cmd.ExecuteScalarAsync() ?? false);
            _logger.LogDebug("Constraint \"{FKName}\" exists: {Exists}", fkName, exists);
            return exists;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while checking if constraint \"{FKName}\" exists.", fkName);
            throw;
        }
    }

    private async Task<HashSet<string>> GetExistingColumnsAsync(string tableName)
    {
        _logger.LogDebug("Retrieving existing columns for table \"{TableName}\".", tableName);
        var columns = new HashSet<string>();
        await using var cmd = _connectionHandler.CreateCommand();
        cmd.CommandText = @"
            SELECT column_name 
            FROM information_schema.columns 
            WHERE table_schema = 'public' 
            AND table_name = @table;";
        cmd.Parameters.AddWithValue("table", tableName);
        try
        {
            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                columns.Add(reader.GetString(0).ToLower());
            }
            _logger.LogDebug("Successfully retrieved {ColumnCount} columns for table \"{TableName}\".", columns.Count, tableName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving columns for table \"{TableName}\".", tableName);
            throw;
        }
        return columns;
    }

    private string GenerateCreateTableCommand(string tableName, List<ColumnInfo> columns)
    {
        _logger.LogDebug("Generating CREATE TABLE command for table \"{TableName}\".", tableName);
        var sb = new StringBuilder();
        sb.Append($"CREATE TABLE \"{tableName}\" (");
        var columnDefs = columns.Select(c =>
        {
            var def = $"\"{c.Name}\" {c.DataType}";
            if (!c.IsNullable && !c.DataType.Contains("PRIMARY KEY"))
                def += " NOT NULL";
            return def;
        });

        var primaryKeys = columns.Where(c => c.IsPrimaryKey).Select(c => $"\"{c.Name}\"").ToList();
        if (primaryKeys.Count > 0)
        {
            sb.Append(string.Join(", ", columnDefs));
            sb.Append($", PRIMARY KEY ({string.Join(", ", primaryKeys)})");
        }
        else
        {
            sb.Append(string.Join(", ", columnDefs));
        }

        sb.Append(");");
        var createTableCommand = sb.ToString();
        _logger.LogDebug("Generated CREATE TABLE command: {Command}", createTableCommand);
        return createTableCommand;
    }

    private string GenerateAddColumnCommand(string tableName, ColumnInfo column)
    {
        _logger.LogDebug("Generating ADD COLUMN command for column \"{ColumnName}\" in table \"{TableName}\".", column.Name, tableName);
        var sb = new StringBuilder();
        sb.Append($"ALTER TABLE \"{tableName}\" ADD COLUMN \"{column.Name}\" {column.DataType}");
        if (!column.IsNullable && !column.DataType.Contains("PRIMARY KEY"))
            sb.Append(" NOT NULL");
        sb.Append(";");
        var addColumnCommand = sb.ToString();
        _logger.LogDebug("Generated ADD COLUMN command: {Command}", addColumnCommand);
        return addColumnCommand;
    }

    private string GetQueueName(string tableName)
    {
        return Utility.QueueName(_config.Host, _config.Database, tableName);
    }

    private class ColumnInfo
    {
        public required string Name { get; init; }
        public required string DataType { get; init; }
        public bool IsPrimaryKey { get; init; }
        public bool IsNullable { get; init; }
    }

    private class TableInfo
    {
        public required string TableName { get; init; }
        public required List<ColumnInfo> Columns { get; init; }
    }

    private class RelationalMappingInfo
    {
        public required string FromTable { get; init; }
        public required string ToTable { get; init; }
        public required string FromColumn { get; init; }
        public required string ToColumn { get; init; }
    }

    private class IndexInfo
    {
        public required string TableName { get; init; }
        public required string ColumnName { get; init; }
        public required string Direction { get; init; }
    }

    private class UniqueIndexInfo
    {
        public required string TableName { get; init; }
        public required List<string> Columns { get; init; }
    }
}