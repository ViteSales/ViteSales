namespace ViteSales.Data.Entities;

public partial class UnrealizedGainLossRate
{
    public long DtlKey { get; set; }

    public long UnrealizedGainLossKey { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public decimal BankBuyRate { get; set; }

    public string? UnrealizedGainAccount { get; set; }

    public string? UnrealizedLossAccount { get; set; }

    public string? GlgainAccount { get; set; }

    public string? GllossAccount { get; set; }

    public decimal BankSellRate { get; set; }

    public virtual Currency CurrencyCodeNavigation { get; set; } = null!;

    public virtual Glmast? GlgainAccountNavigation { get; set; }

    public virtual Glmast? GllossAccountNavigation { get; set; }
}
