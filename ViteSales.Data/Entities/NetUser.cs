namespace ViteSales.Data.Entities;

public partial class NetUser
{
    public long SessionKey { get; set; }

    public DateTime LastCheckDateTime { get; set; }

    public DateTime LoginTime { get; set; }

    public string IsInternetUser { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string? ComputerName { get; set; }

    public string? UserName { get; set; }
}
