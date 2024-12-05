namespace ViteSales.Data.Entities;

public partial class ItemReplacement
{
    public long AutoKey { get; set; }

    public string ItemCode { get; set; } = null!;

    public string ReplacementItemCode { get; set; } = null!;

    public decimal ReplacementDegree { get; set; }

    public string? Note { get; set; }

    public virtual Item ItemCodeNavigation { get; set; } = null!;

    public virtual Item ReplacementItemCodeNavigation { get; set; } = null!;
}
