namespace ViteSales.Data.Entities;

public partial class Cb
{
    public long DocKey { get; set; }

    public string DocNo { get; set; } = null!;

    public DateTime DocDate { get; set; }

    public string DocType { get; set; } = null!;

    public string? DealWith { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public decimal CurrencyRate { get; set; }

    public decimal? TotalPayment { get; set; }

    public decimal? Total { get; set; }

    public decimal? LocalTotal { get; set; }

    public decimal? Tax { get; set; }

    public decimal? LocalTax { get; set; }

    public decimal? NetTotal { get; set; }

    public decimal? LocalNetTotal { get; set; }

    public short PrintCount { get; set; }

    public string Cancelled { get; set; } = null!;

    public string? Note { get; set; }

    public string? ExternalLink { get; set; }

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public string? SourceType { get; set; }

    public long? SourceKey { get; set; }

    public int LastUpdate { get; set; }

    public string? PostDetailDesc { get; set; }

    public DateTime? HandOverDate { get; set; }

    public decimal? ExTax { get; set; }

    public decimal? LocalExTax { get; set; }

    public string? DocNo2 { get; set; }

    public decimal ToTaxCurrencyRate { get; set; }

    public string? Description { get; set; }

    public string InclusiveTax { get; set; } = null!;

    public decimal? TotalExTax { get; set; }

    public decimal? LocalTotalExTax { get; set; }

    public decimal? TaxableAmt { get; set; }

    public long? GltrxId { get; set; }

    public DateTime? TaxDate { get; set; }

    public int RoundingMethod { get; set; }

    public decimal? LocalTaxableAmt { get; set; }

    public decimal? TaxCurrencyTax { get; set; }

    public decimal? TaxCurrencyTaxableAmt { get; set; }

    public byte[]? RchqtaxTransKeymap { get; set; }

    public int? TaxEntityId { get; set; }

    public decimal? WithholdingTax { get; set; }

    public decimal? LocalWithholdingTax { get; set; }

    public decimal? TaxCurrencyWithholdingTax { get; set; }

    public decimal? WithholdingVat { get; set; }

    public decimal? LocalWithholdingVat { get; set; }

    public decimal? TaxCurrencyWithholdingVat { get; set; }

    public int WithholdingTaxVersion { get; set; }

    public string DocStatus { get; set; } = null!;

    public DateTime? ExpiryTimeStamp { get; set; }

    public string? EinvoiceSelfBilledDocNo { get; set; }

    public string SubmitEinvoice { get; set; } = null!;

    public string? EinvoiceStatus { get; set; }

    public DateTime? EinvoiceAipsubmissionDateTime { get; set; }

    public string? EinvoiceUuid { get; set; }

    public DateTime? EinvoiceValidatedDateTime { get; set; }

    public string? EinvoiceValidationLink { get; set; }

    public string? EinvoiceError { get; set; }

    public DateTime? EinvoiceCancelDateTime { get; set; }

    public string? EinvoiceTraceId { get; set; }

    public string? EinvoiceCancelReason { get; set; }

    public string? ReferenceInvoiceNo { get; set; }

    public DateTime? EinvoiceIssueDateTime { get; set; }

    public string? EinvoiceSubmissionUuid { get; set; }

    public virtual User CreatedUser { get; set; } = null!;

    public virtual Currency CurrencyCodeNavigation { get; set; } = null!;

    public virtual User LastModifiedUser { get; set; } = null!;

    public virtual TaxEntity? TaxEntity { get; set; }
}
