namespace ViteSales.Data.Entities;

public partial class Asmorder
{
    public long DocKey { get; set; }

    public string DocNo { get; set; } = null!;

    public DateTime DocDate { get; set; }

    public string? Description { get; set; }

    public string ItemCode { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string? BatchNo { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public decimal? Qty { get; set; }

    public decimal? TransferedQty { get; set; }

    public decimal? Total { get; set; }

    public decimal? AssemblyCost { get; set; }

    public decimal? NetTotal { get; set; }

    public string? FromDocType { get; set; }

    public long? FromDocDtlKey { get; set; }

    public string? Note { get; set; }

    public string? Remark1 { get; set; }

    public string? Remark2 { get; set; }

    public string? Remark3 { get; set; }

    public string? Remark4 { get; set; }

    public short? PrintCount { get; set; }

    public string Cancelled { get; set; } = null!;

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public string? ExternalLink { get; set; }

    public string? RefDocNo { get; set; }

    public int LastUpdate { get; set; }

    public string? CanSync { get; set; }

    public decimal? TotalAssemblyRequestQty { get; set; }

    public short? AssemblyStatus { get; set; }

    public DateTime? LastAopmodified { get; set; }

    public string? LastAopmodifiedUserId { get; set; }

    public string? ExpectedCompletedDate { get; set; }

    public string? IsMultilevel { get; set; }

    public Guid Guid { get; set; }

    public virtual User CreatedUser { get; set; } = null!;

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual ItemBatch? ItemBatch { get; set; }

    public virtual Item ItemCodeNavigation { get; set; } = null!;

    public virtual User? LastAopmodifiedUser { get; set; }

    public virtual User LastModifiedUser { get; set; } = null!;

    public virtual Location LocationNavigation { get; set; } = null!;

    public virtual Project? ProjNoNavigation { get; set; }
}
