namespace ViteSales.Data.Entities;

public partial class SyncProfile
{
    public long ProfileKey { get; set; }

    public string ProfileName { get; set; } = null!;

    public string Uri { get; set; } = null!;

    public int PortNumber { get; set; }

    public string EncryptionKey { get; set; } = null!;

    public string? ServerPassword { get; set; }

    public long SyncCriteriaProfileKey { get; set; }
}
