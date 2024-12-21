using ViteSales.ERP.SDK.Const;
using ViteSales.ERP.SDK.Interfaces;

namespace ViteSales.ERP.SDK.Database.Operation;

public class Delete<T>(List<WhereClause>? whereClauses = null) : IOperation
    where T : class
{
    public DbOperationTypes Type { get; } = DbOperationTypes.Delete;
    private T Data { get; } = Activator.CreateInstance<T>();

    object IOperation.Data()
    {
        return Data;
    }

    public List<WhereClause>? Where()
    {
        if (whereClauses is null)
        {
            throw new ArgumentNullException(nameof(whereClauses));
        }
        return whereClauses;
    }
}