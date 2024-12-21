using ViteSales.ERP.SDK.Const;
using ViteSales.ERP.SDK.Interfaces;

namespace ViteSales.ERP.SDK.Database.Operation;

public class Update<T>(T data, List<WhereClause>? whereClauses = null) : IOperation
    where T : class
{
    public DbOperationTypes Type { get; } = DbOperationTypes.Update;
    public T Data { get; } = data;

    object IOperation.Data()
    {
        return Data;
    }

    public List<WhereClause>? Where()
    {
        return whereClauses;
    }
}