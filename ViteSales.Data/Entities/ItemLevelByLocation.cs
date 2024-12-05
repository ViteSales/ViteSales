namespace ViteSales.Data.Entities;

public partial class ItemLevelByLocation
{
    public long AutoKey { get; set; }

    public string ItemCode { get; set; } = null!;

    public string Uom { get; set; } = null!;

    public string Location { get; set; } = null!;

    public decimal? MinQty { get; set; }

    public decimal? MaxQty { get; set; }

    public decimal? NormalLevel { get; set; }

    public decimal? ReOlevel { get; set; }

    public decimal? ReOqty { get; set; }

    public Guid Guid { get; set; }
}
