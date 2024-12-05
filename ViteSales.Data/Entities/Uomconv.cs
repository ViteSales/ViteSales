namespace ViteSales.Data.Entities;

public partial class Uomconv
{
    public long DocKey { get; set; }

    public string DocNo { get; set; } = null!;

    public DateTime DocDate { get; set; }

    public string? Description { get; set; }

    public string? Note { get; set; }

    public short PrintCount { get; set; }

    public string Cancelled { get; set; } = null!;

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public string? FromDocType { get; set; }

    public long? FromDocKey { get; set; }

    public string? FromDocNo { get; set; }

    public int LastUpdate { get; set; }

    public string CanSync { get; set; } = null!;

    public virtual User CreatedUser { get; set; } = null!;

    public virtual User LastModifiedUser { get; set; } = null!;
}
