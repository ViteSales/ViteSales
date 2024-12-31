using System.Data;
using SqlKata;

namespace ViteSales.ERP.SDK.Interfaces;

public interface IDbContext
{
    Task SaveChanges(Func<List<IOperation>> callback);
    Task<bool> IsTableExist(string tableName);
    Task<bool> IsColumnExist(string tableName, string columnName);
    Task<Query> Get<T>() where T : class;
    Task<DataTable> GetRecords(IOperation operation);
}