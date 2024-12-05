namespace ViteSales.Data.Entities;

public partial class Member
{
    public long AutoKey { get; set; }

    public string MemberNo { get; set; } = null!;

    public string MemberType { get; set; } = null!;

    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? Address3 { get; set; }

    public string? Address4 { get; set; }

    public string? PostCode { get; set; }

    public string? AreaCode { get; set; }

    public string Individual { get; set; } = null!;

    public string? Race { get; set; }

    public DateTime? Dob { get; set; }

    public string? DebtorCode { get; set; }

    public string? CompanyName { get; set; }

    public string? Department { get; set; }

    public string? Title { get; set; }

    public string? MobilePhone { get; set; }

    public string? DirectPhone { get; set; }

    public string? DirectFax { get; set; }

    public string? EmailAddress { get; set; }

    public string? Imaddress { get; set; }

    public string? Note { get; set; }

    public decimal OpeningPoints { get; set; }

    public string? Gender { get; set; }

    public DateTime? RegisterDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string IsActive { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public byte[]? Photo { get; set; }

    public Guid Guid { get; set; }

    public string? MultiPrice { get; set; }

    public virtual Area? AreaCodeNavigation { get; set; }

    public virtual ICollection<BonusPointAdjdtl> BonusPointAdjdtls { get; set; } = new List<BonusPointAdjdtl>();

    public virtual ICollection<BonusPointRedemption> BonusPointRedemptions { get; set; } = new List<BonusPointRedemption>();

    public virtual ICollection<Cn> Cns { get; set; } = new List<Cn>();

    public virtual ICollection<CashSales> Cs { get; set; } = new List<CashSales>();

    public virtual Glmast? DebtorCodeNavigation { get; set; }

    public virtual ICollection<Dn> Dns { get; set; } = new List<Dn>();

    public virtual ICollection<Iv> Ivs { get; set; } = new List<Iv>();

    public virtual MemberType MemberTypeNavigation { get; set; } = null!;

    public virtual ICollection<PointTran> PointTrans { get; set; } = new List<PointTran>();

    public virtual Race? RaceNavigation { get; set; }
}
