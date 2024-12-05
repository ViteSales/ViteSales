namespace ViteSales.Data.Entities;

public partial class AsmRprocessing
{
    public long DocKey { get; set; }

    public long AodocKey { get; set; }

    public long? AsmdocKey { get; set; }

    public string? AsmdocNo { get; set; }

    public string? ItemCode { get; set; }

    public string? Description { get; set; }

    public string? Location { get; set; }

    public string? BatchNo { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public decimal? RequestQty { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public int LastUpdate { get; set; }

    public string? Remark { get; set; }

    public virtual User CreatedUser { get; set; } = null!;

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual Item? ItemCodeNavigation { get; set; }

    public virtual User LastModifiedUser { get; set; } = null!;

    public virtual Location? LocationNavigation { get; set; }

    public virtual Project? ProjNoNavigation { get; set; }
}
