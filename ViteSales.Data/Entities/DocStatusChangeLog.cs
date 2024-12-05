namespace ViteSales.Data.Entities;

public partial class DocStatusChangeLog
{
    public long Id { get; set; }

    public string Action { get; set; } = null!;

    public DateTime ActionTimeStamp { get; set; }

    public string DocStatus { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string DocType { get; set; } = null!;

    public long DocKey { get; set; }

    public string? Reason { get; set; }
}
