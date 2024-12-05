namespace ViteSales.Data.Entities;

public partial class Activity
{
    public long ActivityKey { get; set; }

    public DateTime ActivityDateTime { get; set; }

    public long? EventKey { get; set; }

    public string? UserId { get; set; }

    public string? ComputerName { get; set; }

    public string? DocType { get; set; }

    public long? DocKey { get; set; }

    public string? Description { get; set; }

    public string? Detail { get; set; }

    public string? ThirdPartyAppName { get; set; }

    public virtual User? User { get; set; }
}
