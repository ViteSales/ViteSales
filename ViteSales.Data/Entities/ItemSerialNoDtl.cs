namespace ViteSales.Data.Entities;

public partial class ItemSerialNoDtl
{
    public long AutoKey { get; set; }

    public string ItemCode { get; set; } = null!;

    public string SerialNumber { get; set; } = null!;

    public string Location { get; set; } = null!;

    public int? Qty { get; set; }

    public int? Csgnqty { get; set; }

    public decimal? Cost { get; set; }

    public Guid Guid { get; set; }

    public virtual Item ItemCodeNavigation { get; set; } = null!;

    public virtual Location LocationNavigation { get; set; } = null!;
}
