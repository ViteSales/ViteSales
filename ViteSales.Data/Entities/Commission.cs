namespace ViteSales.Data.Entities;

public partial class Commission
{
    public long AutoKey { get; set; }

    public string SalesAgent { get; set; } = null!;

    public short WithinDay { get; set; }

    public decimal Percentage { get; set; }

    public virtual SalesAgent SalesAgentNavigation { get; set; } = null!;
}
