namespace ViteSales.Data.Entities;

public partial class TaxSetting
{
    public string TaxName { get; set; } = null!;

    public string? TaxValue { get; set; }
}
