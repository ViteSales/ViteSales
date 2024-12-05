namespace ViteSales.Data.Entities;

public partial class Cbdtl
{
    public long DtlKey { get; set; }

    public long DocKey { get; set; }

    public int Seq { get; set; }

    public string? AccNo { get; set; }

    public decimal ToAccountRate { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public string? TaxCode { get; set; }

    public string? Description { get; set; }

    public string? FurtherDescription { get; set; }

    public decimal? Amount { get; set; }

    public decimal? LocalAmount { get; set; }

    public decimal? Rchqamount { get; set; }

    public string? SalesAgent { get; set; }

    public decimal? TaxableAmt { get; set; }

    public decimal? Tax { get; set; }

    public decimal? LocalTax { get; set; }

    public string? TaxPermitNo { get; set; }

    public decimal? TaxAdjustment { get; set; }

    public string? TaxExportCountry { get; set; }

    public string? TaxRefNo { get; set; }

    public decimal? TaxRate { get; set; }

    public decimal? LocalTaxAdjustment { get; set; }

    public DateTime? TaxBillDate { get; set; }

    public decimal? LocalTaxableAmt { get; set; }

    public decimal? TaxCurrencyTax { get; set; }

    public decimal? TaxCurrencyTaxableAmt { get; set; }

    public string InclusiveTax { get; set; } = null!;

    public decimal? AmountExTax { get; set; }

    public decimal? LocalAmountExTax { get; set; }

    public decimal? AmountWithTax { get; set; }

    public decimal? LocalAmountWithTax { get; set; }

    public string? NegativeKopayment { get; set; }

    public string? TariffCode { get; set; }

    public string? Atc { get; set; }

    public int? TaxEntityId { get; set; }

    public virtual Glmast? AccNoNavigation { get; set; }

    public virtual SalesAtc? AtcNavigation { get; set; }

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual Project? ProjNoNavigation { get; set; }

    public virtual SalesAgent? SalesAgentNavigation { get; set; }

    public virtual Tariff? TariffCodeNavigation { get; set; }

    public virtual TaxCode? TaxCodeNavigation { get; set; }

    public virtual TaxEntity? TaxEntity { get; set; }
}
