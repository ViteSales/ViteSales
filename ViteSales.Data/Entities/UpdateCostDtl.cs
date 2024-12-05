namespace ViteSales.Data.Entities;

public partial class UpdateCostDtl
{
    public long DtlKey { get; set; }

    public long DocKey { get; set; }

    public int Seq { get; set; }

    public string? ItemCode { get; set; }

    public string? Description { get; set; }

    public string? Uom { get; set; }

    public decimal? OldCost { get; set; }

    public decimal? NewCost { get; set; }

    public string? Desc2 { get; set; }

    public virtual ItemUom? ItemUom { get; set; }
}
