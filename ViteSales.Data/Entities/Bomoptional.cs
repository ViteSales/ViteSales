namespace ViteSales.Data.Entities;

public partial class Bomoptional
{
    public long BomoptionalKey { get; set; }

    public string BomoptionalCode { get; set; } = null!;

    public string? Description { get; set; }

    public string? Desc2 { get; set; }

    public string IsActive { get; set; } = null!;

    public int LastUpdate { get; set; }
}
