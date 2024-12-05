namespace ViteSales.Data.Entities;

public partial class Iphist
{
    public long Ipkey { get; set; }

    public long? DtlKey { get; set; }

    public long? DocKey { get; set; }

    public string? DocType { get; set; }

    public string? AccNo { get; set; }

    public string? BranchCode { get; set; }

    public string? ItemCode { get; set; }

    public string DocNo { get; set; } = null!;

    public DateTime DocDate { get; set; }

    public string? Location { get; set; }

    public string? Agent { get; set; }

    public string? Description { get; set; }

    public string? FurtherDescription { get; set; }

    public string? BatchNo { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public string? Uom { get; set; }

    public decimal? Qty { get; set; }

    public decimal? Rate { get; set; }

    public decimal? SmallestQty { get; set; }

    public decimal? Focqty { get; set; }

    public decimal? UnitPrice { get; set; }

    public string? Discount { get; set; }

    public string? TaxCode { get; set; }

    public decimal? SubTotal { get; set; }

    public int LastUpdate { get; set; }

    public decimal? CurrencyRate { get; set; }

    public string Cancelled { get; set; } = null!;

    public string? Transfered { get; set; }

    public long? PackageDocKey { get; set; }

    public long? ParentDtlKey { get; set; }

    public string? MemberNo { get; set; }

    public Guid Guid { get; set; }

    public decimal? TaxRate { get; set; }

    public virtual Glmast? AccNoNavigation { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual ItemBatch? ItemBatch { get; set; }

    public virtual ItemUom? ItemUom { get; set; }

    public virtual Location? LocationNavigation { get; set; }

    public virtual Project? ProjNoNavigation { get; set; }
}
