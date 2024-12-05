namespace ViteSales.Data.Entities;

public partial class Fifocost
{
    public long FifocostKey { get; set; }

    public long StockDtlkey { get; set; }

    public short Seq { get; set; }

    public decimal Qty { get; set; }

    public decimal Cost { get; set; }
}
