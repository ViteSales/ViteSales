namespace ViteSales.Data.Entities;

public partial class PackageDtl
{
    public long DtlKey { get; set; }

    public long DocKey { get; set; }

    public int Seq { get; set; }

    public string? ItemCode { get; set; }

    public string? Description { get; set; }

    public string? FurtherDescription { get; set; }

    public string? Uom { get; set; }

    public decimal? Qty { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? SubTotal { get; set; }

    public string PrintOut { get; set; } = null!;

    public decimal? PurchasePrice { get; set; }

    public decimal? PurchaseSubTotal { get; set; }

    public Guid Guid { get; set; }

    public string? TaxCode { get; set; }

    public string? PurchaseTaxCode { get; set; }

    public virtual Package DocKeyNavigation { get; set; } = null!;

    public virtual ItemUom? ItemUom { get; set; }

    public virtual TaxCodes? PurchaseTaxCodeNavigation { get; set; }

    public virtual TaxCodes? TaxCodeNavigation { get; set; }
}
