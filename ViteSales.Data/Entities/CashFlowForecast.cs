namespace ViteSales.Data.Entities;

public partial class CashFlowForecast
{
    public long DocKey { get; set; }

    public string Section { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public string Frequency { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime? EndDate { get; set; }
}
