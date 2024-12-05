namespace ViteSales.Data.Entities;

public partial class WithholdingTaxPaymentDtl
{
    public long TaxPaymentDtlKey { get; set; }

    public string DocType { get; set; } = null!;

    public long DocKey { get; set; }

    public string? WithholdingValueType { get; set; }

    public string? WithholdingTaxCode { get; set; }

    public decimal? WithholdingTaxRate { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public decimal? Amount { get; set; }

    public decimal? LocalAmount { get; set; }

    public decimal? TaxCurrencyAmount { get; set; }

    public decimal? WithholdingTax { get; set; }

    public decimal? LocalWithholdingTax { get; set; }

    public decimal? TaxCurrencyWithholdingTax { get; set; }

    public long DtlKey { get; set; }

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual Project? ProjNoNavigation { get; set; }

    public virtual WithholdingTax? WithholdingTaxCodeNavigation { get; set; }
}
