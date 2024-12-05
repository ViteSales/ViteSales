namespace ViteSales.Data.Entities;

public partial class AssetLink
{
    public long AutoKey { get; set; }

    public string AssetAccNo { get; set; } = null!;

    public string AssetDeprnAccNo { get; set; } = null!;

    public virtual Glmast AssetAccNoNavigation { get; set; } = null!;

    public virtual Glmast AssetDeprnAccNoNavigation { get; set; } = null!;
}
