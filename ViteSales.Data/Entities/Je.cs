namespace ViteSales.Data.Entities;

public partial class Je
{
    public long DocKey { get; set; }

    public string DocNo { get; set; } = null!;

    public DateTime DocDate { get; set; }

    public string? Description { get; set; }

    public string JournalType { get; set; } = null!;

    public string CurrencyCode { get; set; } = null!;

    public decimal CurrencyRate { get; set; }

    public decimal? TotalDr { get; set; }

    public decimal? TotalCr { get; set; }

    public string? Note { get; set; }

    public string? ExternalLink { get; set; }

    public string Cancelled { get; set; } = null!;

    public short PrintCount { get; set; }

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public string? SourceType { get; set; }

    public long? SourceKey { get; set; }

    public int LastUpdate { get; set; }

    public string? PostDetailDesc { get; set; }

    public string? DocNo2 { get; set; }

    public decimal? TaxableDr { get; set; }

    public decimal? TaxableCr { get; set; }

    public decimal? TaxDr { get; set; }

    public decimal? TaxCr { get; set; }

    public decimal? ExTaxDr { get; set; }

    public decimal? ExTaxCr { get; set; }

    public decimal? NetTotalDr { get; set; }

    public decimal? NetTotalCr { get; set; }

    public decimal? LocalTotalDr { get; set; }

    public decimal? LocalTotalCr { get; set; }

    public decimal? LocalTaxableDr { get; set; }

    public decimal? LocalTaxableCr { get; set; }

    public decimal? LocalTaxDr { get; set; }

    public decimal? LocalTaxCr { get; set; }

    public decimal? LocalExTaxDr { get; set; }

    public decimal? LocalExTaxCr { get; set; }

    public decimal? LocalNetTotalDr { get; set; }

    public decimal? LocalNetTotalCr { get; set; }

    public decimal ToTaxCurrencyRate { get; set; }

    public string InclusiveTax { get; set; } = null!;

    public decimal? TotalDrexTax { get; set; }

    public decimal? TotalCrexTax { get; set; }

    public decimal? LocalTotalDrexTax { get; set; }

    public decimal? LocalTotalCrexTax { get; set; }

    public long? GltrxId { get; set; }

    public DateTime? TaxDate { get; set; }

    public int RoundingMethod { get; set; }

    public decimal? TaxCurrencyTaxDr { get; set; }

    public decimal? TaxCurrencyTaxCr { get; set; }

    public decimal? TaxCurrencyTaxableDr { get; set; }

    public decimal? TaxCurrencyTaxableCr { get; set; }

    public string DocStatus { get; set; } = null!;

    public DateTime? ExpiryTimeStamp { get; set; }

    public virtual User CreatedUser { get; set; } = null!;

    public virtual Currency CurrencyCodeNavigation { get; set; } = null!;

    public virtual Journal JournalTypeNavigation { get; set; } = null!;

    public virtual User LastModifiedUser { get; set; } = null!;
}
