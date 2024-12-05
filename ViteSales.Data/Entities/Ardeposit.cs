namespace ViteSales.Data.Entities;

public partial class Ardeposit
{
    public long DocKey { get; set; }

    public string DocNo { get; set; } = null!;

    public string? DebtorCode { get; set; }

    public string? DebtorName { get; set; }

    public string? InvAddr1 { get; set; }

    public string? InvAddr2 { get; set; }

    public string? InvAddr3 { get; set; }

    public string? InvAddr4 { get; set; }

    public string? Phone1 { get; set; }

    public string? Fax1 { get; set; }

    public string? Attention { get; set; }

    public DateTime DocDate { get; set; }

    public string? Description { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public string DepositPaymentMethod { get; set; } = null!;

    public string CurrencyCode { get; set; } = null!;

    public decimal ToDepositRate { get; set; }

    public decimal ToHomeRate { get; set; }

    public decimal? PaymentAmt { get; set; }

    public decimal? TransferedAmt { get; set; }

    public long? Cbkey { get; set; }

    public short PrintCount { get; set; }

    public string Cancelled { get; set; } = null!;

    public decimal? Outstanding { get; set; }

    public string? Note { get; set; }

    public string? ExternalLink { get; set; }

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public string? SourceType { get; set; }

    public long? SourceKey { get; set; }

    public long? GltrxId { get; set; }

    public long? GstjedocKey { get; set; }

    public long? SstjedocKey { get; set; }

    public string IsSecurityDeposit { get; set; } = null!;

    public virtual User CreatedUser { get; set; } = null!;

    public virtual Currency CurrencyCodeNavigation { get; set; } = null!;

    public virtual Glmast? DebtorCodeNavigation { get; set; }

    public virtual PaymentMethod DepositPaymentMethodNavigation { get; set; } = null!;

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual User LastModifiedUser { get; set; } = null!;

    public virtual Project? ProjNoNavigation { get; set; }
}
