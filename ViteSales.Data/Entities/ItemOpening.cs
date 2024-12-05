namespace ViteSales.Data.Entities;

public partial class ItemOpening
{
    public long ItemOpeningKey { get; set; }

    public string ItemCode { get; set; } = null!;

    public string Uom { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string? BatchNo { get; set; }

    public int Seq { get; set; }

    public decimal Qty { get; set; }

    public decimal Cost { get; set; }

    public int LastUpdate { get; set; }

    public Guid Guid { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public DateTime DocDate { get; set; }

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual ItemBatch? ItemBatch { get; set; }

    public virtual ItemUom ItemUom { get; set; } = null!;

    public virtual Location LocationNavigation { get; set; } = null!;

    public virtual Project? ProjNoNavigation { get; set; }
}
