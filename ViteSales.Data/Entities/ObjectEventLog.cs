namespace ViteSales.Data.Entities;

public partial class ObjectEventLog
{
    public long EventKey { get; set; }

    public DateTime OccurTime { get; set; }

    public string EventType { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string? ObjType { get; set; }

    public long? ObjKey { get; set; }

    public string? ObjCode { get; set; }
}
