namespace ViteSales.Data.Entities;

public partial class AppaymentKnockOffDetail
{
    public long AutoKey { get; set; }

    public long KnockOffKey { get; set; }

    public long KnockOffDtlKey { get; set; }

    public decimal? Amount { get; set; }

    public decimal? LocalPaymentAmt { get; set; }

    public decimal? LocalInvoiceAmt { get; set; }
}
