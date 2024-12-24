using ViteSales.ERP.SDK.Const;
using ViteSales.ERP.SDK.Interfaces;

namespace ViteSales.ERP.SDK.Database.Operation;

public class Upsert<T>(T data, ConditionBuilder? whereClauses = null) : IOperation
    where T : class
{
    public DbOperationTypes Type { get; } = DbOperationTypes.Upsert;
    public T Data { get; } = data;

    object IOperation.Data()
    {
        return Data;
    }

    public ConditionBuilder? Where()
    {
        return whereClauses;
    }
}