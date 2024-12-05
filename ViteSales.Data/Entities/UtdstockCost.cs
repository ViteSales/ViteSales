namespace ViteSales.Data.Entities;

public partial class UtdstockCost
{
    public long UtdstockCostKey { get; set; }

    public string ItemCode { get; set; } = null!;

    public string Uom { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string? BatchNo { get; set; }

    public decimal Utdqty { get; set; }

    public decimal Utdcost { get; set; }

    public decimal AdjustedCost { get; set; }

    public decimal? AverageCost { get; set; }

    public virtual ItemBatch? ItemBatch { get; set; }

    public virtual ItemUom ItemUom { get; set; } = null!;

    public virtual Location LocationNavigation { get; set; } = null!;
}
