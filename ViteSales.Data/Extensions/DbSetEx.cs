using System.ComponentModel;
using System.Data;

namespace ViteSales.Data.Extensions;

public static class DbSetEx
{
    public static DataTable ToDataTable<T>(this IList<T> data)
    {
        var table = new DataTable();
        var properties =
            TypeDescriptor.GetProperties(typeof(T));
        foreach (PropertyDescriptor prop in properties)
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        foreach (var item in data)
        {
            var row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }

        return table;
    }
    /// <summary>
    /// Converts a list of objects of type <typeparamref name="T"/> to a DataTable.
    /// </summary>
    /// <typeparam name="T">The type of objects in the list.</typeparam>
    /// <param name="data">The list of objects to convert to a DataTable.</param>
    /// <param name="primaryKey">
    /// Optional. The name of the column to set as the primary key for the DataTable.
    /// If specified and the column exists, it will be set as the primary key.
    /// </param>
    /// <returns>A DataTable representing the list of objects.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the specified primary key column name does not match any column in the DataTable.
    /// </exception>
    public static DataTable ToDataTable<T>(this IList<T> data, string? primaryKey = null)
    {
        var properties =
            TypeDescriptor.GetProperties(typeof(T));
        var table = new DataTable();
        foreach (PropertyDescriptor prop in properties)
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        foreach (var item in data)
        {
            var row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }

        if (string.IsNullOrEmpty(primaryKey))
        {
            table.AcceptChanges();
            return table;
        }
        if (table.Columns.Contains(primaryKey))
        {
            table.PrimaryKey = new[] { table.Columns[primaryKey]! };
        }
        else
        {
            throw new ArgumentException($"Primary key '{primaryKey}' does not match any column name.");
        }
        table.AcceptChanges();
        return table;
    }
    
    /// <summary>
    /// Maps a DataRow to a new instance of the specified entity type.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to map to.</typeparam>
    /// <param name="row">The DataRow to map from.</param>
    /// <returns>A new instance of the entity type with mapped properties.</returns>
    public static TEntity MapToEntity<TEntity>(this DataRow row) where TEntity : new()
    {
        var entity = new TEntity();
        var properties = typeof(TEntity).GetProperties();

        foreach (var property in properties)
        {
            if (!row.Table.Columns.Contains(property.Name) || row[property.Name] == DBNull.Value) continue;
            
            var propertyType = property.PropertyType;
            if (typeof(System.Collections.IEnumerable).IsAssignableFrom(propertyType) &&
                propertyType != typeof(string))
            {
                continue;
            }

            var targetType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
            var value = Convert.ChangeType(row[property.Name], targetType);
            property.SetValue(entity, value);
        }
        return entity;
    }

    /// <summary>
    /// Updates an existing entity instance using the values from a DataRow.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity to update.</typeparam>
    /// <param name="entity">The existing entity instance to update.</param>
    /// <param name="row">The DataRow containing the new values.</param>
    public static void UpdateFromDataRow<TEntity>(this TEntity entity, DataRow row)
    {
        var properties = typeof(TEntity).GetProperties();
        foreach (var property in properties)
        {
            if (!row.Table.Columns.Contains(property.Name) || row[property.Name] == DBNull.Value) continue;
            
            var propertyType = property.PropertyType;
            if (typeof(System.Collections.IEnumerable).IsAssignableFrom(propertyType) &&
                propertyType != typeof(string))
            {
                continue;
            }

            var targetType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
            var value = Convert.ChangeType(row[property.Name], targetType);
            property.SetValue(entity, value);
        }
    }

    public static DataRow ToRow<TEntity>(this TEntity entity, DataTable table)
    {
        var properties = typeof(TEntity).GetProperties();
        foreach (var property in properties)
        {
            if (!table.Columns.Contains(property.Name))
            {
                var columnType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                table.Columns.Add(property.Name, columnType);
            }
        }

        var row = table.NewRow();
        foreach (var property in properties)
        {
            var value = property.GetValue(entity) ?? DBNull.Value;
            row[property.Name] = value;
        }
        return row;
    }
}