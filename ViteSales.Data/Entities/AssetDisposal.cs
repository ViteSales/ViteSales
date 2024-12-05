namespace ViteSales.Data.Entities;

public partial class AssetDisposal
{
    public long DisposalKey { get; set; }

    public string FixedAssetAccNo { get; set; } = null!;

    public DateTime TransDate { get; set; }

    public string? Description { get; set; }

    public decimal DisposalValue { get; set; }

    public decimal CurrencyRate { get; set; }

    public decimal LocalDisposalValue { get; set; }

    public virtual Glmast FixedAssetAccNoNavigation { get; set; } = null!;
}
