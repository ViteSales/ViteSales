namespace ViteSales.Data.Entities;

public partial class TempDocument
{
    public long DocKey { get; set; }

    public string DocType { get; set; } = null!;

    public DateTime SaveTime { get; set; }

    public string? DocNo { get; set; }

    public string UserId { get; set; } = null!;

    public string? SaveReason { get; set; }

    public string? Description { get; set; }

    public string Compressed { get; set; } = null!;

    public byte[]? Data { get; set; }
}
