namespace ViteSales.Data.Entities;

public partial class BankTran
{
    public long BankTransKey { get; set; }

    public string? SourceType { get; set; }

    public long? SourceKey { get; set; }

    public long? DtlKey { get; set; }

    public string DocNo { get; set; } = null!;

    public DateTime DocDate { get; set; }

    public string AccNo { get; set; } = null!;

    public string? ChequeNo { get; set; }

    public short? FloatDay { get; set; }

    public string? Description { get; set; }

    public decimal? PaymentAmt { get; set; }

    public DateTime? BankStatementDate { get; set; }

    public byte? BankReconStatus { get; set; }

    public long? BankSlipDocKey { get; set; }

    public virtual Glmast AccNoNavigation { get; set; } = null!;

    public virtual BankRecon? BankRecon { get; set; }
}
