namespace ViteSales.Data.Entities;

public partial class ItemIngredient
{
    public long AutoKey { get; set; }

    public string ItemCode { get; set; } = null!;

    public string IngredientItemCode { get; set; } = null!;

    public string? Uom { get; set; }

    public decimal? Qty { get; set; }

    public Guid Guid { get; set; }

    public virtual Item IngredientItemCodeNavigation { get; set; } = null!;

    public virtual Item ItemCodeNavigation { get; set; } = null!;
}
