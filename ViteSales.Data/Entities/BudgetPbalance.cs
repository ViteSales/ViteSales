namespace ViteSales.Data.Entities;

public partial class BudgetPbalance
{
    public long AutoKey { get; set; }

    public long BudgetKey { get; set; }

    public int PeriodNo { get; set; }

    public string AccNo { get; set; } = null!;

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public decimal? Amount { get; set; }

    public virtual Glmast AccNoNavigation { get; set; } = null!;

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual Project? ProjNoNavigation { get; set; }
}
