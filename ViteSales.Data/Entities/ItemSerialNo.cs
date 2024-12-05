namespace ViteSales.Data.Entities;

public partial class ItemSerialNo
{
    public long AutoKey { get; set; }

    public string ItemCode { get; set; } = null!;

    public string SerialNumber { get; set; } = null!;

    public string? BatchNo { get; set; }

    public DateTime? ManufacturedDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public DateTime? LastSalesDate { get; set; }

    public string? Remarks { get; set; }

    public int? Qty { get; set; }

    public int? Csgnqty { get; set; }

    public string? Note { get; set; }

    public DateTime? DocDate { get; set; }

    public Guid Guid { get; set; }

    public virtual ItemBatch? ItemBatch { get; set; }

    public virtual Item ItemCodeNavigation { get; set; } = null!;
}
