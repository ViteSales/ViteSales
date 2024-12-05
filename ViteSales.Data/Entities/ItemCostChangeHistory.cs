namespace ViteSales.Data.Entities;

public partial class ItemCostChangeHistory
{
    public long Hkey { get; set; }

    public string? ItemCode { get; set; }

    public string? Uom { get; set; }

    public DateTime ChangeTime { get; set; }

    public decimal? Cost { get; set; }

    public virtual Item? ItemCodeNavigation { get; set; }

    public virtual ItemUom? ItemUom { get; set; }
}
