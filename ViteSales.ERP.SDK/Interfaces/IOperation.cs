using ViteSales.ERP.SDK.Const;
using ViteSales.ERP.SDK.Database.Operation;

namespace ViteSales.ERP.SDK.Interfaces;

public interface IOperation
{
    public DbOperationTypes Type { get; }
    public object Data();
    public ConditionBuilder? Where();
}
