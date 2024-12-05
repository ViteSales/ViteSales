namespace ViteSales.Data.Entities;

public partial class Notification
{
    public long NotificationKey { get; set; }

    public string? Header { get; set; }

    public string? Detail { get; set; }

    public string? UserName { get; set; }

    public DateTime CreatedTimeStamp { get; set; }
}
