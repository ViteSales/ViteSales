namespace ViteSales.Data.Entities;

public partial class Gldtl
{
    public long GldtlKey { get; set; }

    public string AccNo { get; set; } = null!;

    public string? DeaccNo { get; set; }

    public string JournalType { get; set; } = null!;

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public decimal CurrencyRate { get; set; }

    public decimal ToHomeRate { get; set; }

    public decimal? OrgDr { get; set; }

    public decimal? OrgCr { get; set; }

    public decimal? Dr { get; set; }

    public decimal? Cr { get; set; }

    public decimal? HomeDr { get; set; }

    public decimal? HomeCr { get; set; }

    public DateTime TransDate { get; set; }

    public string? Description { get; set; }

    public string? RefNo1 { get; set; }

    public string? RefNo2 { get; set; }

    public long? Dekey { get; set; }

    public string? UserId { get; set; }

    public string? SourceType { get; set; }

    public long? SourceKey { get; set; }

    public string? TaxCode { get; set; }

    public long? GltrxId { get; set; }

    public long? SourceDtlKey { get; set; }

    public string? PaymentDocType { get; set; }

    public long? PaymentDocKey { get; set; }

    public virtual Glmast AccNoNavigation { get; set; } = null!;

    public virtual Currency CurrencyCodeNavigation { get; set; } = null!;

    public virtual Glmast? DeaccNoNavigation { get; set; }

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual Journal JournalTypeNavigation { get; set; } = null!;

    public virtual Project? ProjNoNavigation { get; set; }

    public virtual TaxCode? TaxCodeNavigation { get; set; }
}
