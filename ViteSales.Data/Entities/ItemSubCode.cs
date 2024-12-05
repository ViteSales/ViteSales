namespace ViteSales.Data.Entities;

public partial class ItemSubCode
{
    public long AutoKey { get; set; }

    public string ItemCode { get; set; } = null!;

    public string SubCode { get; set; } = null!;

    public Guid Guid { get; set; }

    public string? Uom { get; set; }

    public virtual Item ItemCodeNavigation { get; set; } = null!;
}
