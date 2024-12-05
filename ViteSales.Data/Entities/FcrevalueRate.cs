namespace ViteSales.Data.Entities;

public partial class FcrevalueRate
{
    public long DtlKey { get; set; }

    public long FcrevalueKey { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public decimal BankBuyRate { get; set; }

    public string? UnrealizedGainAccount { get; set; }

    public string? UnrealizedLossAccount { get; set; }

    public decimal BankSellRate { get; set; }

    public virtual Currency CurrencyCodeNavigation { get; set; } = null!;
}
