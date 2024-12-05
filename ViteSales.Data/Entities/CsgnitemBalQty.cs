namespace ViteSales.Data.Entities;

public partial class CsgnitemBalQty
{
    public long AutoKey { get; set; }

    public string AccNo { get; set; } = null!;

    public string ItemCode { get; set; } = null!;

    public string Uom { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string? BranchCode { get; set; }

    public string? BatchNo { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public decimal? BalQty { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual ItemBatch? ItemBatch { get; set; }

    public virtual ItemUom ItemUom { get; set; } = null!;

    public virtual Location LocationNavigation { get; set; } = null!;

    public virtual Project? ProjNoNavigation { get; set; }
}
