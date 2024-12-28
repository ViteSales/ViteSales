using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Const;
using ViteSales.ERP.SDK.MessageQueue;
using ViteSales.ERP.SDK.Models;
using ViteSales.ERP.SDK.Utils;

namespace ViteSales.ERP.SDK.Database;

public class TableSchemaManager(ConnectionConfig config)
{
    private readonly Connection _connectionHandler = new(config);
    private readonly PubSub _pubSub = new();
    public async Task DropTablesAsync(IEnumerable<Type> types)
    {
        try
        {
            await _connectionHandler.OpenConnectionAsync();
            await _connectionHandler.BeginTransactionAsync();
            foreach (var type in types)
            {
                var tableName = type.Name;
                await using var cmd = _connectionHandler.CreateCommand();
                cmd.CommandText = $"DROP TABLE IF EXISTS \"{tableName}\" CASCADE;";
                await cmd.ExecuteNonQueryAsync();
                await _pubSub.Drop(GetQueueName(tableName));
            }

            await _connectionHandler.CommitTransactionAsync();
        }
        catch
        {
            await _connectionHandler.RollbackTransactionAsync();
            throw;
        }
        finally
        {
            await _connectionHandler.CloseConnectionAsync();
        }
    }
    
    public async Task CreateOrUpdateTablesAsync(IEnumerable<Type> types)
    {
        var tables = new List<TableInfo>();
        var requiresHstore = false;
        var requiresPostgis = false;
        var allUniqueIndexes = new List<UniqueIndexInfo>();
        var allIndexes = new List<IndexInfo>();
        var allRelationalMappings = new List<RelationalMappingInfo>();

        foreach (var type in types)
        {
            var tableName = type.Name;
            var properties = type.GetProperties();

            var columns = new List<ColumnInfo>();
            var uniqueColumns = new List<string>();
            
            var streamAttr = type.GetCustomAttribute<MqStreamAttribute>();
            if (streamAttr != null)
            {
                await _pubSub.InitTopicAsync(GetQueueName(tableName));
            }

            foreach (var prop in properties)
            {
                bool isPrimaryKey = false, isNullable = false;
                var dataType = FieldTypes.Text.GetPostgresColumnType();
                var columnName = prop.Name;
                var bindAttr = prop.GetCustomAttribute<BindDataTypeAttribute>();
                
                // Handle RelationalMappingAttribute
                var relationalAttrs = prop.GetCustomAttributes<RelationalMappingAttribute>();
                allRelationalMappings.AddRange(relationalAttrs.Select(relAttr => new RelationalMappingInfo { ToTable = relAttr.ToTable, FromColumn = relAttr.FromColumn, ToColumn = relAttr.ToColumn, FromTable = tableName }));
                
                if (typeof(System.Collections.IEnumerable).IsAssignableFrom(prop.PropertyType) && prop.PropertyType != typeof(string))
                {
                    if (!(prop.PropertyType == typeof(JsonArray) ||
                          prop.PropertyType == typeof(JsonObject) ||
                          typeof(JsonElement).IsAssignableFrom(prop.PropertyType)))
                    {
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
            await _connectionHandler.OpenConnectionAsync();
            await _connectionHandler.BeginTransactionAsync();

            if (requiresHstore)
            {
                await EnsureHstoreExtensionAsync();
            }

            if (requiresPostgis)
            {
                await EnsurePostgisExtensionAsync();
            }

            foreach (var table in tables)
            {
                var tableExists = await TableExistsAsync(table.TableName);
                if (!tableExists)
                {
                    var createTableCmd = GenerateCreateTableCommand(table.TableName, table.Columns);
                    await using var cmd = _connectionHandler.CreateCommand();
                    cmd.CommandText = createTableCmd;
                    await cmd.ExecuteNonQueryAsync();
                }
                else
                {
                    var existingColumns = await GetExistingColumnsAsync(table.TableName);
                    var newColumns = table.Columns.Where(c => !existingColumns.Contains(c.Name.ToLower())).ToList();
                    foreach (var addColumnCmd in newColumns.Select(column => GenerateAddColumnCommand(table.TableName, column)))
                    {
                        await using var cmd = _connectionHandler.CreateCommand();
                        cmd.CommandText = addColumnCmd;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }

            // Create Unique Indexes
            foreach (var createUniqueIndexCmd in from uniqueIndex in allUniqueIndexes let indexName = $"{uniqueIndex.TableName}_unique_{string.Join("_", uniqueIndex.Columns)}" let columnsList = string.Join(", ", uniqueIndex.Columns.Select(c => $"\"{c}\"")) select $"CREATE UNIQUE INDEX IF NOT EXISTS \"{indexName}\" ON \"{uniqueIndex.TableName}\" ({columnsList});")
            {
                await using var uniqueCmd = _connectionHandler.CreateCommand();
                uniqueCmd.CommandText = createUniqueIndexCmd;
                await uniqueCmd.ExecuteNonQueryAsync();
            }

            // Create Indexes
            foreach (var createIndexCmd in from index in allIndexes let indexName = $"{index.TableName}_{index.ColumnName}_idx" select $"CREATE INDEX IF NOT EXISTS \"{indexName}\" ON \"{index.TableName}\" (\"{index.ColumnName}\" {index.Direction});")
            {
                await using var idxCmd = _connectionHandler.CreateCommand();
                idxCmd.CommandText = createIndexCmd;
                await idxCmd.ExecuteNonQueryAsync();
            }

            // Create Foreign Keys
            foreach (var rel in allRelationalMappings)
            {
                var fkName = $"fk_{rel.FromColumn}_{rel.ToColumn}_{rel.FromTable}_{rel.ToTable}";
                if (await ConstraintsExistsAsync(fkName)) continue;
                
                var addFkCmd = $"ALTER TABLE \"{rel.ToTable}\" ADD CONSTRAINT \"{fkName}\" FOREIGN KEY (\"{rel.ToColumn}\") REFERENCES \"{rel.FromTable}\"(\"{rel.FromColumn}\");";
                await using var fkCmd = _connectionHandler.CreateCommand();
                fkCmd.CommandText = addFkCmd;
                await fkCmd.ExecuteNonQueryAsync();
            }
            await _connectionHandler.CommitTransactionAsync();
        }
        catch
        {
            await _connectionHandler.RollbackTransactionAsync();
            throw;
        }
        finally
        {
            await _connectionHandler.CloseConnectionAsync();
        }
    }

    private async Task EnsureHstoreExtensionAsync()
    {
        const string cmdText = "CREATE EXTENSION IF NOT EXISTS hstore;";
        await using var cmd = _connectionHandler.CreateCommand();
        cmd.CommandText = cmdText;
        await cmd.ExecuteNonQueryAsync();
    }

    private async Task EnsurePostgisExtensionAsync()
    {
        const string cmdText = "CREATE EXTENSION IF NOT EXISTS postgis;";
        await using var cmd = _connectionHandler.CreateCommand();
        cmd.CommandText = cmdText;
        await cmd.ExecuteNonQueryAsync();
    }

    private async Task<bool> TableExistsAsync(string tableName)
    {
        await using var cmd = _connectionHandler.CreateCommand();
        cmd.CommandText = @"
            SELECT EXISTS (
                SELECT FROM information_schema.tables 
                WHERE table_schema = 'public' 
                AND table_name = @table
            );";
        cmd.Parameters.AddWithValue("table", tableName);
        var exists = (bool)(await cmd.ExecuteScalarAsync() ?? false);
        return exists;
    }

    private async Task<bool> ConstraintsExistsAsync(string fkName)
    {
        await using var cmd = _connectionHandler.CreateCommand();
        cmd.CommandText = @"
            SELECT EXISTS(
                SELECT 1 FROM pg_constraint WHERE conname = @fkName
            );";
        cmd.Parameters.AddWithValue("fkName", fkName);
        var exists = (bool)(await cmd.ExecuteScalarAsync() ?? false);
        return exists;
    }

    private async Task<HashSet<string>> GetExistingColumnsAsync(string tableName)
    {
        var columns = new HashSet<string>();
        await using var cmd = _connectionHandler.CreateCommand();
        cmd.CommandText = @"
            SELECT column_name 
            FROM information_schema.columns 
            WHERE table_schema = 'public' 
            AND table_name = @table;";
        cmd.Parameters.AddWithValue("table", tableName);
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            columns.Add(reader.GetString(0).ToLower());
        }
        return columns;
    }

    private string GenerateCreateTableCommand(string tableName, List<ColumnInfo> columns)
    {
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
        return sb.ToString();
    }

    private string GenerateAddColumnCommand(string tableName, ColumnInfo column)
    {
        var sb = new StringBuilder();
        sb.Append($"ALTER TABLE \"{tableName}\" ADD COLUMN \"{column.Name}\" {column.DataType}");
        if (!column.IsNullable && !column.DataType.Contains("PRIMARY KEY"))
            sb.Append(" NOT NULL");
        sb.Append(";");
        return sb.ToString();
    }

    private string GetQueueName(string tableName)
    {
        return Utility.QueueName(config.Host, config.Database, tableName);
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