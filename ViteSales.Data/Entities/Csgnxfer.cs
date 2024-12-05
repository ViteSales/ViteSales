namespace ViteSales.Data.Entities;

public partial class Csgnxfer
{
    public long DocKey { get; set; }

    public string DocNo { get; set; } = null!;

    public DateTime DocDate { get; set; }

    public string FromDebtorCode { get; set; } = null!;

    public string? FromDebtorName { get; set; }

    public string? FromBranchCode { get; set; }

    public string ToDebtorCode { get; set; } = null!;

    public string? ToDebtorName { get; set; }

    public string? ToBranchCode { get; set; }

    public string? Ref { get; set; }

    public string? Description { get; set; }

    public string? SalesAgent { get; set; }

    public string? ToInvAddr1 { get; set; }

    public string? ToInvAddr2 { get; set; }

    public string? ToInvAddr3 { get; set; }

    public string? ToInvAddr4 { get; set; }

    public string? ToPhone1 { get; set; }

    public string? ToFax1 { get; set; }

    public string? ToAttention { get; set; }

    public string? ToDeliverAddr1 { get; set; }

    public string? ToDeliverAddr2 { get; set; }

    public string? ToDeliverAddr3 { get; set; }

    public string? ToDeliverAddr4 { get; set; }

    public string? ToDeliverPhone1 { get; set; }

    public string? ToDeliverFax1 { get; set; }

    public string? ToDeliverContact { get; set; }

    public string? Note { get; set; }

    public string? Remark1 { get; set; }

    public string? Remark2 { get; set; }

    public string? Remark3 { get; set; }

    public string? Remark4 { get; set; }

    public short PrintCount { get; set; }

    public string Cancelled { get; set; } = null!;

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public string? ExternalLink { get; set; }

    public string? RefDocNo { get; set; }

    public int LastUpdate { get; set; }

    public string CanSync { get; set; } = null!;

    public virtual Branch? Branch { get; set; }

    public virtual Branch? BranchNavigation { get; set; }

    public virtual User CreatedUser { get; set; } = null!;

    public virtual Glmast FromDebtorCodeNavigation { get; set; } = null!;

    public virtual User LastModifiedUser { get; set; } = null!;

    public virtual SalesAgent? SalesAgentNavigation { get; set; }

    public virtual Glmast ToDebtorCodeNavigation { get; set; } = null!;
}
