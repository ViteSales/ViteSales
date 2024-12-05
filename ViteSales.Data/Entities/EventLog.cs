namespace ViteSales.Data.Entities;

public partial class EventLog
{
    public long EventKey { get; set; }

    public DateTime EventDateTime { get; set; }

    public string UserId { get; set; } = null!;

    public string? ComputerName { get; set; }

    public string? DocType { get; set; }

    public long? DocKey { get; set; }

    public byte EventType { get; set; }

    public string? Description { get; set; }

    public string? EventMessage { get; set; }

    public string? ThirdPartyAppName { get; set; }

    public virtual User User { get; set; } = null!;
}
