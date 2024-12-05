namespace ViteSales.Data.Entities;

public partial class ChangeLog
{
    public long Id { get; set; }

    public string TableName { get; set; } = null!;

    public Guid Guid { get; set; }

    public Guid? NewGuid { get; set; }

    public string? Content { get; set; }

    public string? ContentType { get; set; }

    public string ChangeType { get; set; } = null!;

    public DateTime ChangeTime { get; set; }

    public long? TransactionId { get; set; }
}
