namespace ViteSales.Data.Entities;

public partial class WithholdingTaxTran
{
    public long TaxTransKey { get; set; }

    public string SourceType { get; set; } = null!;

    public long SourceKey { get; set; }

    public long SourceDtlKey { get; set; }

    public int Seq { get; set; }

    public string DocNo { get; set; } = null!;

    public DateTime DocDate { get; set; }

    public string? TaxableAccNo { get; set; }

    public decimal? TaxableAmt { get; set; }

    public decimal? LocalTaxableAmt { get; set; }

    public decimal? Tax { get; set; }

    public decimal? LocalTax { get; set; }

    public string ReceiptPayment { get; set; } = null!;

    public string? TaxableName { get; set; }

    public string WithholdingTaxCode { get; set; } = null!;

    public decimal WithholdingTaxRate { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public decimal CurrencyRate { get; set; }

    public DateTime TaxDate { get; set; }

    public string? WithholdingValueType { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public string? PaymentDocType { get; set; }

    public long? PaymentDocKey { get; set; }

    public int TaxEntityId { get; set; }

    public virtual TaxEntity TaxEntity { get; set; } = null!;

    public virtual Glmast? TaxableAccNoNavigation { get; set; }

    public virtual WithholdingTax WithholdingTaxCodeNavigation { get; set; } = null!;
}
