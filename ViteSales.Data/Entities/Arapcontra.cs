namespace ViteSales.Data.Entities;

public partial class Arapcontra
{
    public long DocKey { get; set; }

    public string DocNo { get; set; } = null!;

    public string DebtorCode { get; set; } = null!;

    public string CreditorCode { get; set; } = null!;

    public string TempAccNo { get; set; } = null!;

    public string JournalType { get; set; } = null!;

    public string? Ref { get; set; }

    public DateTime DocDate { get; set; }

    public string? Description { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public decimal CurrencyRate { get; set; }

    public decimal? NetTotal { get; set; }

    public decimal? ArlocalNetTotal { get; set; }

    public decimal? AplocalNetTotal { get; set; }

    public decimal? ArknockOffAmt { get; set; }

    public decimal? ApknockOffAmt { get; set; }

    public string Cancelled { get; set; } = null!;

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

    public string? RefNo2 { get; set; }

    public string? DebtorBranchCode { get; set; }

    public string? CreditorBranchCode { get; set; }

    public long? GltrxId { get; set; }

    public string DocStatus { get; set; } = null!;

    public DateTime? ExpiryTimeStamp { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual Branch? BranchNavigation { get; set; }

    public virtual User CreatedUser { get; set; } = null!;

    public virtual Glmast CreditorCodeNavigation { get; set; } = null!;

    public virtual Currency CurrencyCodeNavigation { get; set; } = null!;

    public virtual Glmast DebtorCodeNavigation { get; set; } = null!;

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual Journal JournalTypeNavigation { get; set; } = null!;

    public virtual User LastModifiedUser { get; set; } = null!;

    public virtual Project? ProjNoNavigation { get; set; }

    public virtual Glmast TempAccNoNavigation { get; set; } = null!;
}
