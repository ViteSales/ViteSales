namespace ViteSales.Data.Entities;

public partial class PriceBookRule
{
    public long RuleNo { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string? FromItemType { get; set; }

    public string? ToItemType { get; set; }

    public string? FromItemGroup { get; set; }

    public string? ToItemGroup { get; set; }

    public string? FromItemBrand { get; set; }

    public string? ToItemBrand { get; set; }

    public string? FromItemClass { get; set; }

    public string? ToItemClass { get; set; }

    public string? FromItemCategory { get; set; }

    public string? ToItemCategory { get; set; }

    public string? FromItemCode { get; set; }

    public string? ToItemCode { get; set; }

    public string? Uom { get; set; }

    public string? FromLocation { get; set; }

    public string? ToLocation { get; set; }

    public string? FromProjNo { get; set; }

    public string? ToProjNo { get; set; }

    public string? FromDeptNo { get; set; }

    public string? ToDeptNo { get; set; }

    public string? FromSalesAgent { get; set; }

    public string? ToSalesAgent { get; set; }

    public string? FromPriceCategory { get; set; }

    public string? ToPriceCategory { get; set; }

    public string? FromDebtorType { get; set; }

    public string? ToDebtorType { get; set; }

    public string? FromAreaCode { get; set; }

    public string? ToAreaCode { get; set; }

    public string? FromDebtorCode { get; set; }

    public string? ToDebtorCode { get; set; }

    public string? CurrencyCode { get; set; }

    public string? DisplayTerm { get; set; }

    public decimal? Geqty { get; set; }

    public decimal? UnitPrice { get; set; }

    public string? Discount { get; set; }

    public string? IsActive { get; set; }

    public string? BatchNo { get; set; }

    public long? MatrixKey { get; set; }

    public short? Priority { get; set; }

    public virtual Currency? CurrencyCodeNavigation { get; set; }

    public virtual Term? DisplayTermNavigation { get; set; }

    public virtual Area? FromAreaCodeNavigation { get; set; }

    public virtual Debtor? FromDebtorCodeNavigation { get; set; }

    public virtual DebtorType? FromDebtorTypeNavigation { get; set; }

    public virtual Dept? FromDeptNoNavigation { get; set; }

    public virtual ItemBrand? FromItemBrandNavigation { get; set; }

    public virtual ItemCategory? FromItemCategoryNavigation { get; set; }

    public virtual ItemClass? FromItemClassNavigation { get; set; }

    public virtual Item? FromItemCodeNavigation { get; set; }

    public virtual ItemGroup? FromItemGroupNavigation { get; set; }

    public virtual ItemType? FromItemTypeNavigation { get; set; }

    public virtual Location? FromLocationNavigation { get; set; }

    public virtual PriceCategory? FromPriceCategoryNavigation { get; set; }

    public virtual Project? FromProjNoNavigation { get; set; }

    public virtual SalesAgent? FromSalesAgentNavigation { get; set; }

    public virtual PriceBookMatrix? MatrixKeyNavigation { get; set; }

    public virtual Area? ToAreaCodeNavigation { get; set; }

    public virtual Debtor? ToDebtorCodeNavigation { get; set; }

    public virtual DebtorType? ToDebtorTypeNavigation { get; set; }

    public virtual Dept? ToDeptNoNavigation { get; set; }

    public virtual ItemBrand? ToItemBrandNavigation { get; set; }

    public virtual ItemCategory? ToItemCategoryNavigation { get; set; }

    public virtual ItemClass? ToItemClassNavigation { get; set; }

    public virtual Item? ToItemCodeNavigation { get; set; }

    public virtual ItemGroup? ToItemGroupNavigation { get; set; }

    public virtual ItemType? ToItemTypeNavigation { get; set; }

    public virtual Location? ToLocationNavigation { get; set; }

    public virtual PriceCategory? ToPriceCategoryNavigation { get; set; }

    public virtual Project? ToProjNoNavigation { get; set; }

    public virtual SalesAgent? ToSalesAgentNavigation { get; set; }
}
