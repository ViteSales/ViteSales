namespace ViteSales.Data.Entities;

public partial class BonusPointRedemption
{
    public long DocKey { get; set; }

    public string? MemberNo { get; set; }

    public string? DebtorCode { get; set; }

    public decimal? TotalPointRedeem { get; set; }

    public string? DocNo { get; set; }

    public DateTime? DocDate { get; set; }

    public string? Description { get; set; }

    public string? Note { get; set; }

    public string PostToStock { get; set; } = null!;

    public long? ReferDocKey { get; set; }

    public short PrintCount { get; set; }

    public string Cancelled { get; set; } = null!;

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public string CanSync { get; set; } = null!;

    public int LastUpdate { get; set; }

    public Guid Guid { get; set; }

    public virtual ICollection<BonusPointRedemptionDtl> BonusPointRedemptionDtls { get; set; } = new List<BonusPointRedemptionDtl>();

    public virtual User CreatedUser { get; set; } = null!;

    public virtual Glmast? DebtorCodeNavigation { get; set; }

    public virtual User LastModifiedUser { get; set; } = null!;

    public virtual Member? MemberNoNavigation { get; set; }
}
