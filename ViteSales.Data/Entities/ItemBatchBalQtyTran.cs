namespace ViteSales.Data.Entities;

public partial class ItemBatchBalQtyTran
{
    public long TransKey { get; set; }

    public string ItemCode { get; set; } = null!;

    public string Uom { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string BatchNo { get; set; } = null!;

    public decimal Qty { get; set; }
}
