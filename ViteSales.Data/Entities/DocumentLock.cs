namespace ViteSales.Data.Entities;

public partial class DocumentLock
{
    public long DocLockKey { get; set; }

    public string DocType { get; set; } = null!;

    public long DocKey { get; set; }

    public string DocNo { get; set; } = null!;

    public string ComputerName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime LastUpdate { get; set; }
}
