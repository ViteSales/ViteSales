namespace ViteSales.Data.Entities;

public partial class GltrxIdtrash
{
    public long GltrxId { get; set; }

    public string UserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public string? DocType { get; set; }

    public long? DocKey { get; set; }

    public byte[]? SourceData { get; set; }

    public string? Compressed { get; set; }

    public virtual User User { get; set; } = null!;
}
