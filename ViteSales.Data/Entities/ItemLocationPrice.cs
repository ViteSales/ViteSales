namespace ViteSales.Data.Entities;

public partial class ItemLocationPrice
{
    public long AutoKey { get; set; }

    public string ItemCode { get; set; } = null!;

    public string Uom { get; set; } = null!;

    public string Location { get; set; } = null!;

    public decimal? Price { get; set; }

    public Guid Guid { get; set; }

    public virtual ItemUom ItemUom { get; set; } = null!;

    public virtual Location LocationNavigation { get; set; } = null!;
}
