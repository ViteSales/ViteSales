namespace ViteSales.Data.Entities;

public partial class AccessRight
{
    public long AutoKey { get; set; }

    public string CmdId { get; set; } = null!;

    public string UserId { get; set; } = null!;
}
