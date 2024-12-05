namespace ViteSales.Data.Entities;

public partial class Jedtl
{
    public long DtlKey { get; set; }

    public long DocKey { get; set; }

    public int Seq { get; set; }

    public string? AccNo { get; set; }

    public decimal ToAccountRate { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public string? Description { get; set; }

    public string? FurtherDescription { get; set; }

    public decimal? Dr { get; set; }

    public decimal? Cr { get; set; }

    public string? TaxCode { get; set; }

    public string SupplyPurchase { get; set; } = null!;

    public decimal? TaxDr { get; set; }

    public decimal? TaxCr { get; set; }

    public decimal? TotalDr { get; set; }

    public decimal? TotalCr { get; set; }

    public string? SalesAgent { get; set; }

    public decimal? TaxableDr { get; set; }

    public decimal? TaxableCr { get; set; }

    public string? RefNo2 { get; set; }

    public string? TaxPermitNo { get; set; }

    public decimal? TaxAdjustment { get; set; }

    public string? TaxExportCountry { get; set; }

    public string? TaxRefNo { get; set; }

    public decimal? TaxRate { get; set; }

    public decimal? LocalTaxAdjustment { get; set; }

    public DateTime? TaxBillDate { get; set; }

    public decimal? LocalTaxableDr { get; set; }

    public decimal? LocalTaxableCr { get; set; }

    public decimal? LocalTaxDr { get; set; }

    public decimal? LocalTaxCr { get; set; }

    public decimal? TaxCurrencyTaxDr { get; set; }

    public decimal? TaxCurrencyTaxCr { get; set; }

    public decimal? TaxCurrencyTaxableDr { get; set; }

    public decimal? TaxCurrencyTaxableCr { get; set; }

    public string InclusiveTax { get; set; } = null!;

    public string? TariffCode { get; set; }

    public DateTime? PostingDate { get; set; }

    public DateTime? TaxDate { get; set; }

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
