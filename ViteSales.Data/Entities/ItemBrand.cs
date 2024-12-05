namespace ViteSales.Data.Entities;

public partial class ItemBrand
{
    public long AutoKey { get; set; }

    public string ItemBrandCode { get; set; } = null!;

    public string? Description { get; set; }

    public string? Desc2 { get; set; }

    public string? Note { get; set; }

    public int LastUpdate { get; set; }

    public string? ShortCode { get; set; }

    public decimal? MarkupRatio { get; set; }

    public Guid Guid { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<PriceBookRule> PriceBookRuleFromItemBrandNavigations { get; set; } = new List<PriceBookRule>();

    public virtual ICollection<PriceBookRule> PriceBookRuleToItemBrandNavigations { get; set; } = new List<PriceBookRule>();
}
