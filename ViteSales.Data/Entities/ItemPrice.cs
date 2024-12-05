namespace ViteSales.Data.Entities;

public partial class ItemPrice
{
    public long ItemPriceKey { get; set; }

    public string ItemCode { get; set; } = null!;

    public string Uom { get; set; } = null!;

    public string? PriceCategory { get; set; }

    public string? AccNo { get; set; }

    public string? SuppCustItemCode { get; set; }

    public string? Ref { get; set; }

    public string UseFixedPrice { get; set; } = null!;

    public decimal? FixedPrice { get; set; }

    public string? FixedDetailDiscount { get; set; }

    public decimal? Qty1 { get; set; }

    public decimal? Price1 { get; set; }

    public string? DetailDiscount1 { get; set; }

    public decimal? Qty2 { get; set; }

    public decimal? Price2 { get; set; }

    public string? DetailDiscount2 { get; set; }

    public decimal? Qty3 { get; set; }

    public decimal? Price3 { get; set; }

    public string? DetailDiscount3 { get; set; }

    public decimal? Qty4 { get; set; }

    public decimal? Price4 { get; set; }

    public string? DetailDiscount4 { get; set; }

    public decimal? Foclevel { get; set; }

    public decimal? Focqty { get; set; }

    public decimal? BonusPointQty { get; set; }

    public decimal? BonusPoint { get; set; }

    public int LastUpdate { get; set; }

    public Guid Guid { get; set; }

    public virtual Glmast? AccNoNavigation { get; set; }

    public virtual ItemUom ItemUom { get; set; } = null!;

    public virtual PriceCategory? PriceCategoryNavigation { get; set; }
}
