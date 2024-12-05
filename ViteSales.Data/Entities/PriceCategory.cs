namespace ViteSales.Data.Entities;

public partial class PriceCategory
{
    public long AutoKey { get; set; }

    public string PriceCategory1 { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? DiscountPercent { get; set; }

    public string? DetailDiscount { get; set; }

    public int LastUpdate { get; set; }

    public decimal? MarkupRatio { get; set; }

    public virtual ICollection<Creditor> Creditors { get; set; } = new List<Creditor>();

    public virtual ICollection<Debtor> Debtors { get; set; } = new List<Debtor>();

    public virtual ICollection<ItemPrice> ItemPrices { get; set; } = new List<ItemPrice>();

    public virtual ICollection<PriceBookRule> PriceBookRuleFromPriceCategoryNavigations { get; set; } = new List<PriceBookRule>();

    public virtual ICollection<PriceBookRule> PriceBookRuleToPriceCategoryNavigations { get; set; } = new List<PriceBookRule>();
}
