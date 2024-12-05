namespace ViteSales.Data.Entities;

public partial class Xsdtl
{
    public long DtlKey { get; set; }

    public long DocKey { get; set; }

    public int Seq { get; set; }

    public byte? Indent { get; set; }

    public string? FontStyle { get; set; }

    public string MainItem { get; set; } = null!;

    public string? Numbering { get; set; }

    public string? ItemCode { get; set; }

    public string? Location { get; set; }

    public string? Description { get; set; }

    public string? FurtherDescription { get; set; }

    public string? YourPono { get; set; }

    public DateTime? YourPodate { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public string? Uom { get; set; }

    public string? UserUom { get; set; }

    public decimal? Qty { get; set; }

    public decimal? Rate { get; set; }

    public decimal? SmallestQty { get; set; }

    public decimal? Focqty { get; set; }

    public decimal? SmallestUnitPrice { get; set; }

    public decimal? UnitPrice { get; set; }

    public string? Discount { get; set; }

    public decimal? DiscountAmt { get; set; }

    public string? TaxCode { get; set; }

    public decimal? Tax { get; set; }

    public decimal? SubTotal { get; set; }

    public decimal? LocalSubTotal { get; set; }

    public string PrintOut { get; set; } = null!;

    public string? DtlType { get; set; }

    public decimal? CalcByPercent { get; set; }

    public string AddToSubTotal { get; set; } = null!;

    public string? FromDocType { get; set; }

    public string? FromDocNo { get; set; }

    public long? FromDocDtlKey { get; set; }

    public string? FullTransferOption { get; set; }

    public string? FullTransferFromDocList { get; set; }

    public string? EstimatedDeliveryDate { get; set; }

    public long? PackageDocKey { get; set; }

    public long? ParentDtlKey { get; set; }

    public decimal? SubQty { get; set; }

    public decimal? SubTotalExTax { get; set; }

    public decimal? LocalTax { get; set; }

    public Guid Guid { get; set; }

    public long? RuleNo { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public decimal? TaxableAmt { get; set; }

    public decimal? TaxAdjustment { get; set; }

    public decimal? LocalSubTotalExTax { get; set; }

    public decimal? ExtraDiscountAmt { get; set; }

    public decimal? TaxRate { get; set; }

    public decimal? LocalTaxAdjustment { get; set; }

    public decimal? LocalTaxableAmt { get; set; }

    public decimal? TaxCurrencyTax { get; set; }

    public decimal? TaxCurrencyTaxableAmt { get; set; }

    public string? SalesExemptionNo { get; set; }

    public string? Desc2 { get; set; }

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual FontStyle? FontStyleNavigation { get; set; }

    public virtual ItemUom? ItemUom { get; set; }

    public virtual Location? LocationNavigation { get; set; }

    public virtual Project? ProjNoNavigation { get; set; }

    public virtual TaxCode? TaxCodeNavigation { get; set; }
}
