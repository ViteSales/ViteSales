namespace ViteSales.Data.Entities;

public partial class Session
{
    public int SessionKey { get; set; }

    public string UserId { get; set; } = null!;

    public DateTime? TimeStart { get; set; }

    public DateTime? TimeEnd { get; set; }

    public string? ComputerName { get; set; }

    public string? UserName { get; set; }

    public int? PrivateKey { get; set; }
}
