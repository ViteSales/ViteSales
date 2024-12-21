using ViteSales.ERP.SDK.Const;
using ViteSales.ERP.SDK.Interfaces;

namespace ViteSales.ERP.SDK.Database.Operation;

public class Insert<T>(T data) : IOperation
    where T : class
{
    public DbOperationTypes Type { get; } = DbOperationTypes.Insert;
    public T Data { get; } = data;

    object IOperation.Data()
    {
        return Data;
    }

    public List<WhereClause>? Where()
    {
        return null;
    }
}