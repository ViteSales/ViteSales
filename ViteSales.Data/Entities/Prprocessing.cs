namespace ViteSales.Data.Entities;

public partial class Prprocessing
{
    public long DocKey { get; set; }

    public long FromDtlKey { get; set; }

    public long FromDocKey { get; set; }

    public string? ItemCode { get; set; }

    public string? Description { get; set; }

    public string? Sodescription { get; set; }

    public string? FurtherDescription { get; set; }

    public string? EstimatedDeliveryDate { get; set; }

    public string? Location { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public string? CreditorCode { get; set; }

    public decimal? RequestQty { get; set; }

    public string? RequestUom { get; set; }

    public string IsAllowOpedit { get; set; } = null!;

    public string CreatedUserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public int LastUpdate { get; set; }

    public string? FromDocType { get; set; }

    public string? Remark { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public virtual User CreatedUser { get; set; } = null!;

    public virtual Glmast? CreditorCodeNavigation { get; set; }

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual ItemUom? ItemUom { get; set; }

    public virtual User LastModifiedUser { get; set; } = null!;

    public virtual Location? LocationNavigation { get; set; }

    public virtual Project? ProjNoNavigation { get; set; }
}
