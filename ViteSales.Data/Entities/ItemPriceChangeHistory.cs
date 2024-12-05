namespace ViteSales.Data.Entities;

public partial class ItemPriceChangeHistory
{
    public long Hkey { get; set; }

    public string? ItemCode { get; set; }

    public string? Uom { get; set; }

    public DateTime ChangeTime { get; set; }

    public decimal? Price { get; set; }

    public virtual Item? ItemCodeNavigation { get; set; }

    public virtual ItemUom? ItemUom { get; set; }
}
