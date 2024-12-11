namespace ViteSales.Data.Entities;

public partial class ArinvoiceDtl
{
    public long DtlKey { get; set; }

    public long? DocKey { get; set; }

    public int Seq { get; set; }

    public string? AccNo { get; set; }

    public decimal ToAccountRate { get; set; }

    public string? Description { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public string? TaxCode { get; set; }

    public decimal? Tax { get; set; }

    public decimal? LocalTax { get; set; }

    public decimal? Amount { get; set; }

    public decimal? NetAmount { get; set; }

    public decimal? LocalNetAmount { get; set; }

    public decimal? KnockOffAmount { get; set; }

    public decimal? TaxableAmt { get; set; }

    public decimal? SubTotal { get; set; }

    public decimal? LocalSubTotal { get; set; }

    public decimal? TaxAdjustment { get; set; }

    public string? TaxExportCountry { get; set; }

    public long? SourceDtlKey { get; set; }

    public decimal? TaxRate { get; set; }

    public decimal? LocalTaxAdjustment { get; set; }

    public string? SourceDtlType { get; set; }

    public decimal? LocalTaxableAmt { get; set; }

    public decimal? TaxCurrencyTax { get; set; }

    public decimal? TaxCurrencyTaxableAmt { get; set; }

    public string? TaxPermitNo { get; set; }

    public string? TariffCode { get; set; }

    public string? Atc { get; set; }

    public int? AccountingBasis { get; set; }

    public virtual Glmast? AccNoNavigation { get; set; }

    public virtual SalesAtc? AtcNavigation { get; set; }

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual Project? ProjNoNavigation { get; set; }

    public virtual Tariff? TariffCodeNavigation { get; set; }

    public virtual TaxCodes? TaxCodeNavigation { get; set; }
}
