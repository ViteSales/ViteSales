namespace ViteSales.Data.Entities;

public partial class Aprefund
{
    public long DocKey { get; set; }

    public string DocNo { get; set; } = null!;

    public string CreditorCode { get; set; } = null!;

    public DateTime DocDate { get; set; }

    public string? Description { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public decimal ToCreditorRate { get; set; }

    public decimal ToHomeRate { get; set; }

    public decimal? PaymentAmt { get; set; }

    public decimal? LocalPaymentAmt { get; set; }

    public decimal? KnockOffAmt { get; set; }

    public long? Cbkey { get; set; }

    public string Cancelled { get; set; } = null!;

    public string? Note { get; set; }

    public string? ExternalLink { get; set; }

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public string? SourceType { get; set; }

    public long? SourceKey { get; set; }

    public decimal? RevalueRate { get; set; }

    public DateTime? HandOverDate { get; set; }

    public string? BranchCode { get; set; }

    public string? DocNo2 { get; set; }

    public long? GltrxId { get; set; }

    public decimal? WithholdingTax { get; set; }

    public decimal? LocalWithholdingTax { get; set; }

    public decimal? TaxCurrencyWithholdingTax { get; set; }

    public int WithholdingTaxVersion { get; set; }

    public string DocStatus { get; set; } = null!;

    public DateTime? ExpiryTimeStamp { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual User CreatedUser { get; set; } = null!;

    public virtual Glmast CreditorCodeNavigation { get; set; } = null!;

    public virtual Currency CurrencyCodeNavigation { get; set; } = null!;

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual User LastModifiedUser { get; set; } = null!;

    public virtual Project? ProjNoNavigation { get; set; }
}
