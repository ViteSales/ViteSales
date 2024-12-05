namespace ViteSales.Data.Entities;

public partial class Drprocessing
{
    public long DocKey { get; set; }

    public long SodtlKey { get; set; }

    public long SodocKey { get; set; }

    public string? ItemCode { get; set; }

    public string? Description { get; set; }

    public string? FurtherDescription { get; set; }

    public string? EstimatedDeliveryDate { get; set; }

    public string? Location { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public string? DebtorCode { get; set; }

    public decimal? DeliveryQty { get; set; }

    public string? DeliveryUom { get; set; }

    public decimal? DeliveryRate { get; set; }

    public decimal? UnitPrice { get; set; }

    public string? Discount { get; set; }

    public string? TaxCode { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public int LastUpdate { get; set; }

    public string? Remark { get; set; }

    public string? IsAllowOpedit { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public decimal? TaxRate { get; set; }

    public virtual User CreatedUser { get; set; } = null!;

    public virtual Glmast? DebtorCodeNavigation { get; set; }

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual ItemUom? ItemUom { get; set; }

    public virtual User LastModifiedUser { get; set; } = null!;

    public virtual Location? LocationNavigation { get; set; }

    public virtual Project? ProjNoNavigation { get; set; }

    public virtual TaxCode? TaxCodeNavigation { get; set; }
}
