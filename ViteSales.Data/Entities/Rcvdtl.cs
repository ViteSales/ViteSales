namespace ViteSales.Data.Entities;

public partial class Rcvdtl
{
    public long DtlKey { get; set; }

    public long DocKey { get; set; }

    public int Seq { get; set; }

    public string? Numbering { get; set; }

    public string? ItemCode { get; set; }

    public string? Location { get; set; }

    public string? BatchNo { get; set; }

    public string? Description { get; set; }

    public string? FurtherDescription { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public decimal? Qty { get; set; }

    public string? Uom { get; set; }

    public decimal? UnitCost { get; set; }

    public decimal? SubTotal { get; set; }

    public string PrintOut { get; set; } = null!;

    public string? SerialNoList { get; set; }

    public Guid Guid { get; set; }

    public string? Desc2 { get; set; }

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual ItemBatch? ItemBatch { get; set; }

    public virtual ItemUom? ItemUom { get; set; }

    public virtual Location? LocationNavigation { get; set; }

    public virtual Project? ProjNoNavigation { get; set; }
}
