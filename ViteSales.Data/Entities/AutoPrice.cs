namespace ViteSales.Data.Entities;

public partial class AutoPrice
{
    public int AutoPriceKey { get; set; }

    public int Seq { get; set; }

    public string AutoPriceType { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string ForSale { get; set; } = null!;

    public string Enable { get; set; } = null!;
}
