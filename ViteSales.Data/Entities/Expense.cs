namespace ViteSales.Data.Entities;

public partial class Expense
{
    public long DocKey { get; set; }

    public string AccNo { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public string Frequency { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Glmast AccNoNavigation { get; set; } = null!;
}
