namespace ViteSales.Data.Entities;

public partial class StockDtlchangeQ
{
    public long ChangeQkey { get; set; }

    public string ItemCode { get; set; } = null!;

    public long StockDtlkey { get; set; }

    public string Action { get; set; } = null!;

    public virtual Item ItemCodeNavigation { get; set; } = null!;
}
