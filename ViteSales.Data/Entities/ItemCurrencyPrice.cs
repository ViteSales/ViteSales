namespace ViteSales.Data.Entities;

public partial class ItemCurrencyPrice
{
    public long AutoKey { get; set; }

    public string ItemCode { get; set; } = null!;

    public string Uom { get; set; } = null!;

    public string CurrencyCode { get; set; } = null!;

    public decimal? Price { get; set; }

    public int LastUpdate { get; set; }

    public virtual Currency CurrencyCodeNavigation { get; set; } = null!;

    public virtual ItemUom ItemUom { get; set; } = null!;
}
