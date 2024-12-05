namespace ViteSales.Data.Entities;

public partial class ItemCostHistory
{
    public int Hkey { get; set; }

    public string? ItemCode { get; set; }

    public string? Uom { get; set; }

    public DateTime DocDate { get; set; }

    public decimal? Cost { get; set; }

    public long DtlKey { get; set; }

    public long DocKey { get; set; }

    public string DocType { get; set; } = null!;

    public string DocNo { get; set; } = null!;

    public DateTime? CreatedTimeStamp { get; set; }

    public string? CreatedUserId { get; set; }

    public virtual ItemUom? ItemUom { get; set; }
}
