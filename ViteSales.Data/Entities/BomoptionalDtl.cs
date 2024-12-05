namespace ViteSales.Data.Entities;

public partial class BomoptionalDtl
{
    public long DtlKey { get; set; }

    public long BomoptionalKey { get; set; }

    public string SubItemCode { get; set; } = null!;

    public decimal Qty { get; set; }

    public decimal OverheadCost { get; set; }

    public virtual Item SubItemCodeNavigation { get; set; } = null!;
}
