namespace ViteSales.Data.Entities;

public partial class Xfer
{
    public long DocKey { get; set; }

    public string DocNo { get; set; } = null!;

    public DateTime DocDate { get; set; }

    public string FromLocation { get; set; } = null!;

    public string ToLocation { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? Total { get; set; }

    public string? Reason { get; set; }

    public string? AuthorisedBy { get; set; }

    public string? Ref { get; set; }

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

    public Guid Guid { get; set; }

    public virtual User CreatedUser { get; set; } = null!;

    public virtual Location FromLocationNavigation { get; set; } = null!;

    public virtual User LastModifiedUser { get; set; } = null!;

    public virtual Location ToLocationNavigation { get; set; } = null!;
}
