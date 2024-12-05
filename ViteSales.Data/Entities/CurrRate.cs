namespace ViteSales.Data.Entities;

public partial class CurrRate
{
    public long AutoKey { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public decimal BankBuyRate { get; set; }

    public decimal BankSellRate { get; set; }

    public Guid Guid { get; set; }

    public virtual Currency CurrencyCodeNavigation { get; set; } = null!;
}
