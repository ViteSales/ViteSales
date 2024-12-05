namespace ViteSales.Data.Entities;

public partial class ItemBom
{
    public long ItemBomkey { get; set; }

    public string ItemCode { get; set; } = null!;

    public string SubItemCode { get; set; } = null!;

    public decimal Qty { get; set; }

    public decimal OverheadCost { get; set; }

    public int Seq { get; set; }

    public decimal? CostFraction { get; set; }

    public virtual Item ItemCodeNavigation { get; set; } = null!;

    public virtual Item SubItemCodeNavigation { get; set; } = null!;
}
