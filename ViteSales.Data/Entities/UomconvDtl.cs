namespace ViteSales.Data.Entities;

public partial class UomconvDtl
{
    public long DtlKey { get; set; }

    public long InDtlKey { get; set; }

    public long DocKey { get; set; }

    public int Seq { get; set; }

    public string? ItemCode { get; set; }

    public string? Location { get; set; }

    public string? BatchNo { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public decimal? FromQty { get; set; }

    public string? FromUom { get; set; }

    public string? ToUom { get; set; }

    public decimal? ToQty { get; set; }

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual ItemBatch? ItemBatch { get; set; }

    public virtual ItemUom? ItemUom { get; set; }

    public virtual ItemUom? ItemUomNavigation { get; set; }

    public virtual Location? LocationNavigation { get; set; }

    public virtual Project? ProjNoNavigation { get; set; }
}
