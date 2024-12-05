namespace ViteSales.Data.Entities;

public partial class Layout
{
    public long AutoKey { get; set; }

    public string Title { get; set; } = null!;

    public string? FormName { get; set; }

    public string? ComponentName { get; set; }

    public string? Template { get; set; }

    public string IsDefault { get; set; } = null!;

    public long? LastUpdate { get; set; }
}
