namespace ViteSales.Data.Entities;

public partial class Apcn
{
    public long DocKey { get; set; }

    public string DocNo { get; set; } = null!;

    public string CreditorCode { get; set; } = null!;

    public string JournalType { get; set; } = null!;

    public string? Cntype { get; set; }

    public string? Ref { get; set; }

    public DateTime DocDate { get; set; }

    public string? Description { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public decimal CurrencyRate { get; set; }

    public decimal? Total { get; set; }

    public decimal? LocalTotal { get; set; }

    public decimal? Tax { get; set; }

    public decimal? LocalTax { get; set; }

    public decimal? NetTotal { get; set; }

    public decimal? LocalNetTotal { get; set; }

    public decimal? KnockOffAmt { get; set; }

    public decimal? RefundAmt { get; set; }

    public string Cancelled { get; set; } = null!;

    public string IsJournal { get; set; } = null!;

    public long? Jekey { get; set; }

    public string? Note { get; set; }

    public string? ExternalLink { get; set; }

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public string? SourceType { get; set; }

    public long? SourceKey { get; set; }

    public decimal? RevalueRate { get; set; }

    public decimal? TotalRevalueGainLoss { get; set; }

    public string? RefNo2 { get; set; }

    public decimal? ExTax { get; set; }

    public decimal? LocalExTax { get; set; }

    public decimal? Total2 { get; set; }

    public decimal? LocalTotal2 { get; set; }

    public string? BranchCode { get; set; }

    public string? SupplierCnno { get; set; }

    public decimal ToTaxCurrencyRate { get; set; }

    public string? TaxDocNo { get; set; }

    public string? SupplierInvoiceNo { get; set; }

    public string? Reason { get; set; }

    public string InclusiveTax { get; set; } = null!;

    public decimal? TaxableAmt { get; set; }

    public long? GltrxId { get; set; }

    public DateTime? TaxDate { get; set; }

    public int RoundingMethod { get; set; }

    public decimal? LocalTaxableAmt { get; set; }

    public decimal? TaxCurrencyTax { get; set; }

    public decimal? TaxCurrencyTaxableAmt { get; set; }

    public DateTime? DocDate2 { get; set; }

    public short PrintCount { get; set; }

    public long? ReferCajedocKey { get; set; }

    public string? ReferCajedocNo { get; set; }

    public decimal? WithholdingTax { get; set; }

    public decimal? LocalWithholdingTax { get; set; }

    public decimal? TaxCurrencyWithholdingTax { get; set; }

    public decimal? WithholdingVat { get; set; }

    public decimal? LocalWithholdingVat { get; set; }

    public decimal? TaxCurrencyWithholdingVat { get; set; }

    public int? TaxEntityId { get; set; }

    public string DocStatus { get; set; } = null!;

    public DateTime? ExpiryTimeStamp { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual Cntype? CntypeNavigation { get; set; }

    public virtual User CreatedUser { get; set; } = null!;

    public virtual Glmast CreditorCodeNavigation { get; set; } = null!;

    public virtual Currency CurrencyCodeNavigation { get; set; } = null!;

    public virtual Journal JournalTypeNavigation { get; set; } = null!;

    public virtual User LastModifiedUser { get; set; } = null!;

    public virtual TaxEntity? TaxEntity { get; set; }
}
