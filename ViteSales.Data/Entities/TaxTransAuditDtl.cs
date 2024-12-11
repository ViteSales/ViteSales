namespace ViteSales.Data.Entities;

public partial class TaxTransAuditDtl
{
    public long TaxTransAuditDtlKey { get; set; }

    public long TaxTransAuditKey { get; set; }

    public long TaxTransKey { get; set; }

    public string SourceType { get; set; } = null!;

    public long SourceKey { get; set; }

    public string DocNo { get; set; } = null!;

    public DateTime DocDate { get; set; }

    public string TaxCode { get; set; } = null!;

    public string SupplyPurchase { get; set; } = null!;

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public string? TaxableAccNo { get; set; }

    public string? TaxableName { get; set; }

    public decimal? TaxableAmt { get; set; }

    public decimal? LocalTaxableAmt { get; set; }

    public decimal? Tax { get; set; }

    public decimal? LocalTax { get; set; }

    public long? SourceDtlKey { get; set; }

    public int? Seq { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public decimal CurrencyRate { get; set; }

    public decimal TaxRate { get; set; }

    public string? TaxAccNo { get; set; }

    public DateTime TaxDate { get; set; }

    public string? TaxRefNo { get; set; }

    public string? TaxPermitNo { get; set; }

    public string? TaxExportCountry { get; set; }

    public string? Description { get; set; }

    public long? OriginalDtlKey { get; set; }

    public string? OriginalDtlType { get; set; }

    public byte Action { get; set; }

    public byte Severity { get; set; }

    public string? TariffCode { get; set; }

    public string? Atc { get; set; }

    public string? PaymentDocType { get; set; }

    public long? PaymentDocKey { get; set; }

    public int? TaxEntityId { get; set; }

    public virtual SalesAtc? AtcNavigation { get; set; }

    public virtual Currency CurrencyCodeNavigation { get; set; } = null!;

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual Project? ProjNoNavigation { get; set; }

    public virtual Tariff? TariffCodeNavigation { get; set; }

    public virtual Glmast? TaxAccNoNavigation { get; set; }

    public virtual TaxCodes TaxCodesNavigation { get; set; } = null!;

    public virtual TaxEntity? TaxEntity { get; set; }

    public virtual Glmast? TaxableAccNoNavigation { get; set; }
}
