namespace ViteSales.Data.Entities;

public partial class ApcnknockOffDetail
{
    public long AutoKey { get; set; }

    public long KnockOffKey { get; set; }

    public long KnockOffDtlKey { get; set; }

    public decimal? Amount { get; set; }

    public decimal? LocalCnamt { get; set; }

    public decimal? LocalInvoiceAmt { get; set; }
}
