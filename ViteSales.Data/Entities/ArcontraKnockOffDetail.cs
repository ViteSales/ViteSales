namespace ViteSales.Data.Entities;

public partial class ArcontraKnockOffDetail
{
    public long AutoKey { get; set; }

    public long KnockOffKey { get; set; }

    public long KnockOffDtlKey { get; set; }

    public decimal? Amount { get; set; }

    public decimal? LocalContraAmt { get; set; }

    public decimal? LocalInvoiceAmt { get; set; }
}
