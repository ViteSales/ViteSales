namespace ViteSales.Data.Entities;

public partial class GiftRule
{
    public long DocKey { get; set; }

    public string DocNo { get; set; } = null!;

    public DateTime DocDate { get; set; }

    public string Name { get; set; } = null!;

    public string? Icno { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? Address3 { get; set; }

    public string? Address4 { get; set; }

    public string? Phone { get; set; }

    public string? EmailAddress { get; set; }

    public string? GiftDescription { get; set; }

    public decimal? GiftTotalAmount { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public long? JedocKey { get; set; }

    public decimal? TaxCurrencyGiftTotalAmount { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public decimal ToTaxCurrencyRate { get; set; }

    public decimal CurrencyRate { get; set; }
}
