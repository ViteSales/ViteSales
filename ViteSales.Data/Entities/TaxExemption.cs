namespace ViteSales.Data.Entities;

public partial class TaxExemption
{
    public long AutoKey { get; set; }

    public string AccNo { get; set; } = null!;

    public string ItemCode { get; set; } = null!;

    public string SalesExemptionNo { get; set; } = null!;

    public string TaxCode { get; set; } = null!;

    public int LastUpdate { get; set; }

    public virtual TaxCodes TaxCodesNavigation { get; set; } = null!;
}
