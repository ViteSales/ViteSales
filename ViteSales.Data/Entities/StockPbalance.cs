namespace ViteSales.Data.Entities;

public partial class StockPbalance
{
    public long AutoKey { get; set; }

    public int StockSetKey { get; set; }

    public int PeriodNo { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public decimal? Balance { get; set; }

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual Project? ProjNoNavigation { get; set; }
}
