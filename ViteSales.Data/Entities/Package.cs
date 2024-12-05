namespace ViteSales.Data.Entities;

public partial class Package
{
    public long DocKey { get; set; }

    public string PackageCode { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public decimal? OpeningQty { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? LimitedQty { get; set; }

    public decimal? SoldQty { get; set; }

    public decimal? NetTotal { get; set; }

    public string? IsActive { get; set; }

    public string? FurtherDescription { get; set; }

    public string? UserUom { get; set; }

    public decimal? PurchaseNetTotal { get; set; }

    public string? BarCode { get; set; }

    public decimal? PurchasedQty { get; set; }

    public Guid Guid { get; set; }

    public virtual ICollection<PackageDtl> PackageDtls { get; set; } = new List<PackageDtl>();
}
