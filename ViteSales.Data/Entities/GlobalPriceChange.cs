namespace ViteSales.Data.Entities;

public partial class GlobalPriceChange
{
    public long DocKey { get; set; }

    public string? Name { get; set; }

    public DateTime? ChangeDateTime { get; set; }

    public string? Criteria { get; set; }

    public string? Data { get; set; }

    public string? Updated { get; set; }

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public string CreatedUserId { get; set; } = null!;
}
