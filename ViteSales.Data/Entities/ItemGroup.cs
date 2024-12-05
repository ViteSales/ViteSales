namespace ViteSales.Data.Entities;

public partial class ItemGroup
{
    public string ItemGroup1 { get; set; } = null!;

    public string? Description { get; set; }

    public string? Desc2 { get; set; }

    public string? Note { get; set; }

    public string? SalesCode { get; set; }

    public string? CashSalesCode { get; set; }

    public string? SalesReturnCode { get; set; }

    public string? SalesDiscountCode { get; set; }

    public string? PurchaseDiscountCode { get; set; }

    public string? PurchaseCode { get; set; }

    public string? PurchaseReturnCode { get; set; }

    public int LastUpdate { get; set; }

    public string? ShortCode { get; set; }

    public decimal? MarkupRatio { get; set; }

    public string? BalanceStockCode { get; set; }

    public Guid Guid { get; set; }

    public string? RoundingMethod { get; set; }

    public string? RoundingAmount { get; set; }

    public string? CashPurchaseCode { get; set; }

    public virtual Glmast? BalanceStockCodeNavigation { get; set; }

    public virtual Glmast? CashPurchaseCodeNavigation { get; set; }

    public virtual Glmast? CashSalesCodeNavigation { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<PriceBookRule> PriceBookRuleFromItemGroupNavigations { get; set; } = new List<PriceBookRule>();

    public virtual ICollection<PriceBookRule> PriceBookRuleToItemGroupNavigations { get; set; } = new List<PriceBookRule>();

    public virtual Glmast? PurchaseCodeNavigation { get; set; }

    public virtual Glmast? PurchaseDiscountCodeNavigation { get; set; }

    public virtual Glmast? PurchaseReturnCodeNavigation { get; set; }

    public virtual Glmast? SalesCodeNavigation { get; set; }

    public virtual Glmast? SalesDiscountCodeNavigation { get; set; }

    public virtual Glmast? SalesReturnCodeNavigation { get; set; }
}
