namespace ViteSales.ERP.SDK.Interfaces;

public interface ITableSchemaManager
{
    Task DropTablesAsync(IEnumerable<Type> types);
    Task CreateOrUpdateTablesAsync(IEnumerable<Type> types);
}