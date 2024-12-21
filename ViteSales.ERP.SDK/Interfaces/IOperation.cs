using ViteSales.ERP.SDK.Const;

namespace ViteSales.ERP.SDK.Interfaces;

public interface IOperation
{
    public DbOperationTypes Type { get; }
    public object Data();
    public List<WhereClause>? Where();
}

public class WhereClause
{
    public string Field { get; set; }
    public string Operator { get; set; }
    public object Value { get; set; }
}