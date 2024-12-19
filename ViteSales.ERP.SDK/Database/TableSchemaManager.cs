using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Const;

namespace ViteSales.ERP.SDK.Database;

public class TableSchemaManager(Connection connectionHandler)
{
    public async Task DropTablesAsync(IEnumerable<Type> types)
    {
        try
        {
            await connectionHandler.OpenConnectionAsync();
            await connectionHandler.BeginTransactionAsync();
            foreach (var type in types)
            {
                var tableName = type.Name;
                await using var cmd = connectionHandler.CreateCommand();
                cmd.CommandText = $"DROP TABLE IF EXISTS \"{tableName}\" CASCADE;";
                await cmd.ExecuteNonQueryAsync();
            }

            await connectionHandler.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            await connectionHandler.CloseConnectionAsync();
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

            foreach (var prop in properties)
            {
                bool isPrimaryKey = false, isNullable = false;
                var dataType = FieldTypes.Text.GetPostgresType();
                var columnName = prop.Name;
                var bindAttr = prop.GetCustomAttribute<BindDataTypeAttribute>();
                
                // Handle RelationalMappingAttribute
                var relationalAttrs = prop.GetCustomAttributes<RelationalMappingAttribute>();
                allRelationalMappings.AddRange(relationalAttrs.Select(relAttr => new RelationalMappingInfo { ToTable = relAttr.ToTable, FromColumn = relAttr.FromColumn, ToColumn = relAttr.ToColumn, FromTable = tableName }));
                
                if (typeof(System.Collections.IEnumerable).IsAssignableFrom(prop.PropertyType) && prop.PropertyType != typeof(string))
                {
                    continue;
                }
                if (bindAttr != null)
                {
                    dataType = bindAttr.Type.GetPostgresType();
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
            await connectionHandler.OpenConnectionAsync();
            await connectionHandler.BeginTransactionAsync();

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
                    await using var cmd = connectionHandler.CreateCommand();
                    cmd.CommandText = createTableCmd;
                    await cmd.ExecuteNonQueryAsync();
                }
                else
                {
                    var existingColumns = await GetExistingColumnsAsync(table.TableName);
                    var newColumns = table.Columns.Where(c => !existingColumns.Contains(c.Name.ToLower())).ToList();
                    foreach (var column in newColumns)
                    {
                        var addColumnCmd = GenerateAddColumnCommand(table.TableName, column);
                        await using var cmd = connectionHandler.CreateCommand();
                        cmd.CommandText = addColumnCmd;
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }

            // Create Unique Indexes
            foreach (var uniqueIndex in allUniqueIndexes)
            {
                var indexName = $"{uniqueIndex.TableName}_unique_{string.Join("_", uniqueIndex.Columns)}";
                var columnsList = string.Join(", ", uniqueIndex.Columns.Select(c => $"\"{c}\""));
                var createUniqueIndexCmd = $"CREATE UNIQUE INDEX IF NOT EXISTS \"{indexName}\" ON \"{uniqueIndex.TableName}\" ({columnsList});";
                await using var uniqueCmd = connectionHandler.CreateCommand();
                uniqueCmd.CommandText = createUniqueIndexCmd;
                await uniqueCmd.ExecuteNonQueryAsync();
            }

            // Create Indexes
            foreach (var index in allIndexes)
            {
                var indexName = $"{index.TableName}_{index.ColumnName}_idx";
                var createIndexCmd = $"CREATE INDEX IF NOT EXISTS \"{indexName}\" ON \"{index.TableName}\" (\"{index.ColumnName}\" {index.Direction});";
                await using var idxCmd = connectionHandler.CreateCommand();
                idxCmd.CommandText = createIndexCmd;
                await idxCmd.ExecuteNonQueryAsync();
            }

            // Create Foreign Keys
            foreach (var rel in allRelationalMappings)
            {
                var fkName = $"fk_{rel.FromColumn}_{rel.ToColumn}_{rel.FromTable}_{rel.ToTable}";
                var addFkCmd = $"ALTER TABLE \"{rel.ToTable}\" ADD CONSTRAINT \"{fkName}\" FOREIGN KEY (\"{rel.ToColumn}\") REFERENCES \"{rel.FromTable}\"(\"{rel.FromColumn}\");";
                Console.WriteLine(addFkCmd);
                await using var fkCmd = connectionHandler.CreateCommand();
                fkCmd.CommandText = addFkCmd;
                await fkCmd.ExecuteNonQueryAsync();
            }

            await connectionHandler.CommitTransactionAsync();
        }
        catch(Exception e)
        {
            await connectionHandler.RollbackTransactionAsync();
            throw e;
        }
        finally
        {
            await connectionHandler.CloseConnectionAsync();
        }
    }

    private async Task EnsureHstoreExtensionAsync()
    {
        var cmdText = "CREATE EXTENSION IF NOT EXISTS hstore;";
        await using var cmd = connectionHandler.CreateCommand();
        cmd.CommandText = cmdText;
        await cmd.ExecuteNonQueryAsync();
    }

    private async Task EnsurePostgisExtensionAsync()
    {
        var cmdText = "CREATE EXTENSION IF NOT EXISTS postgis;";
        await using var cmd = connectionHandler.CreateCommand();
        cmd.CommandText = cmdText;
        await cmd.ExecuteNonQueryAsync();
    }

    private async Task<bool> TableExistsAsync(string tableName)
    {
        await using var cmd = connectionHandler.CreateCommand();
        cmd.CommandText = @"
            SELECT EXISTS (
                SELECT FROM information_schema.tables 
                WHERE table_schema = 'public' 
                AND table_name = @table
            );";
        cmd.Parameters.AddWithValue("table", tableName.ToLower());
        var exists = (bool)(await cmd.ExecuteScalarAsync() ?? false);
        return exists;
    }

    private async Task<HashSet<string>> GetExistingColumnsAsync(string tableName)
    {
        var columns = new HashSet<string>();
        await using var cmd = connectionHandler.CreateCommand();
        cmd.CommandText = @"
            SELECT column_name 
            FROM information_schema.columns 
            WHERE table_schema = 'public' 
            AND table_name = @table;";
        cmd.Parameters.AddWithValue("table", tableName.ToLower());
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

    private class ColumnInfo
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsNullable { get; set; }
    }

    private class TableInfo
    {
        public string TableName { get; set; }
        public List<ColumnInfo> Columns { get; set; }
    }

    private class RelationalMappingInfo
    {
        public string FromTable { get; set; }
        public string ToTable { get; set; }
        public string FromColumn { get; set; }
        public string ToColumn { get; set; }
    }

    private class IndexInfo
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string Direction { get; set; }
    }

    private class UniqueIndexInfo
    {
        public string TableName { get; set; }
        public List<string> Columns { get; set; }
    }
}