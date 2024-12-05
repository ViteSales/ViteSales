namespace ViteSales.Data.Entities;

public partial class UtdstockCostDtl
{
    public long UtdstockCostDtlkey { get; set; }

    public long UtdstockCostKey { get; set; }

    public short Seq { get; set; }

    public decimal Qty { get; set; }

    public decimal Cost { get; set; }
}
