namespace ViteSales.Data.Entities;

public partial class BomoptionalLink
{
    public long BomoptionalLinkKey { get; set; }

    public long BomoptionalKey { get; set; }

    public string BomitemCode { get; set; } = null!;

    public decimal Qty { get; set; }

    public virtual Item BomitemCodeNavigation { get; set; } = null!;
}
