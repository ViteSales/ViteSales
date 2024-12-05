namespace ViteSales.Data.Entities;

public partial class UserFormula
{
    public string FormulaName { get; set; } = null!;

    public string ColumnName { get; set; } = null!;

    public string FormulaType { get; set; } = null!;

    public string? Formula { get; set; }

    public long? LastUpdate { get; set; }
}
