using System.Data;
using SqlKata;

namespace ViteSales.ERP.SDK.Interfaces;

public interface IDbContext
{
    Task SaveChangesAsync(Func<List<IOperation>> callback);
    Task<bool> IsTableExistAsync(string tableName);
    Task<bool> IsColumnExistAsync(string tableName, string columnName);
    Task<Query> GetAsync<T>() where T : class;
    Task<DataTable> GetRecordsAsync(IOperation operation);
}