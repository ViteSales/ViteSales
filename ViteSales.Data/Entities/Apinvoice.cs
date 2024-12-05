namespace ViteSales.Data.Entities;

public partial class Apinvoice
{
    public long DocKey { get; set; }

    public string DocNo { get; set; } = null!;

    public string CreditorCode { get; set; } = null!;

    public string JournalType { get; set; } = null!;

    public DateTime DocDate { get; set; }

    public string DisplayTerm { get; set; } = null!;

    public DateTime DueDate { get; set; }

    public string? Description { get; set; }

    public string? PurchaseAgent { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public decimal CurrencyRate { get; set; }

    public decimal? Total { get; set; }

    public decimal? LocalTotal { get; set; }

    public decimal? Tax { get; set; }

    public decimal? LocalTax { get; set; }

    public decimal? NetTotal { get; set; }

    public decimal? LocalNetTotal { get; set; }

    public decimal? PaymentAmt { get; set; }

    public decimal? LocalPaymentAmt { get; set; }

    public decimal? Outstanding { get; set; }

    public string Cancelled { get; set; } = null!;

    public string? Note { get; set; }

    public string? ExternalLink { get; set; }

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public string? SourceType { get; set; }

    public long? SourceKey { get; set; }

    public DateTime? ForecastDueDate { get; set; }

    public decimal? RevalueRate { get; set; }

    public decimal? TotalRevalueGainLoss { get; set; }

    public string? RefNo2 { get; set; }

    public DateTime? AgingDate { get; set; }

    public decimal? ExTax { get; set; }

    public decimal? LocalExTax { get; set; }

    public decimal? Total2 { get; set; }

    public decimal? LocalTotal2 { get; set; }

    public string? BranchCode { get; set; }

    public string? SupplierInvoiceNo { get; set; }

    public decimal ToTaxCurrencyRate { get; set; }

    public string? TaxDocNo { get; set; }

    public string InclusiveTax { get; set; } = null!;

    public decimal? TaxableAmt { get; set; }

    public long? GltrxId { get; set; }

    public long? ReferAtmsjedocKey { get; set; }

    public string? ReferAtmsjedocNo { get; set; }

    public string? ReferAtmsjeisNonTaxableSupply { get; set; }

    public DateTime? TaxDate { get; set; }

    public int RoundingMethod { get; set; }

    public decimal? LocalTaxableAmt { get; set; }

    public decimal? TaxCurrencyTax { get; set; }

    public decimal? TaxCurrencyTaxableAmt { get; set; }

    public DateTime? DocDate2 { get; set; }

    public string? ReferIsjeisPayment { get; set; }

    public long? ReferIsjedocKey { get; set; }

    public string? ReferIsjedocNo { get; set; }

    public string? ReferIsjeisNonTaxableSupply { get; set; }

    public string? ReferCajeisPayment { get; set; }

    public long? ReferCajedocKey { get; set; }

    public string? ReferCajedocNo { get; set; }

    public long? ReferSstisjedocKey { get; set; }

    public string? ReferSstisjedocNo { get; set; }

    public long? ReferSstsdjedocKey { get; set; }

    public string? ReferSstsdjedocNo { get; set; }

    public decimal? WithholdingTax { get; set; }

    public decimal? LocalWithholdingTax { get; set; }

    public decimal? TaxCurrencyWithholdingTax { get; set; }

    public decimal? WithholdingVat { get; set; }

    public decimal? LocalWithholdingVat { get; set; }

    public decimal? TaxCurrencyWithholdingVat { get; set; }

    public DateTime? WhtpostingDate { get; set; }

    public int? TaxEntityId { get; set; }

    public string DocStatus { get; set; } = null!;

    public DateTime? ExpiryTimeStamp { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual User CreatedUser { get; set; } = null!;

    public virtual Glmast CreditorCodeNavigation { get; set; } = null!;

    public virtual Currency CurrencyCodeNavigation { get; set; } = null!;

    public virtual Term DisplayTermNavigation { get; set; } = null!;

    public virtual Journal JournalTypeNavigation { get; set; } = null!;

    public virtual User LastModifiedUser { get; set; } = null!;

    public virtual PurchaseAgent? PurchaseAgentNavigation { get; set; }

    public virtual TaxEntity? TaxEntity { get; set; }
}
