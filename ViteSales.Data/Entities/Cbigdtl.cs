namespace ViteSales.Data.Entities;

public partial class Cbigdtl
{
    public long DtlKey { get; set; }

    public long DocKey { get; set; }

    public long CbdtlKey { get; set; }

    public decimal? TaxableAmt { get; set; }

    public string? TaxCode { get; set; }

    public decimal? Tax { get; set; }

    public decimal? LocalTax { get; set; }

    public string? TaxPermitNo { get; set; }

    public int Seq { get; set; }

    public decimal? TaxRate { get; set; }

    public decimal CurrencyRate { get; set; }

    public decimal? TaxAdjustment { get; set; }

    public decimal? LocalTaxAdjustment { get; set; }

    public decimal? LocalTaxableAmt { get; set; }

    public decimal? TaxCurrencyTax { get; set; }

    public decimal? TaxCurrencyTaxableAmt { get; set; }

    public long SourceDtlKey { get; set; }

    public string? SourceType { get; set; }

    public string? SourceDtlType { get; set; }

    public virtual TaxCode? TaxCodeNavigation { get; set; }
}
