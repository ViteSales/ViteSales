namespace ViteSales.Data.Entities;

public partial class StockSet
{
    public int StockSetKey { get; set; }

    public string OpenStock { get; set; } = null!;

    public string CloseStock { get; set; } = null!;

    public string BalanceStock { get; set; } = null!;

    public virtual Glmast BalanceStockNavigation { get; set; } = null!;

    public virtual Glmast CloseStockNavigation { get; set; } = null!;

    public virtual Glmast OpenStockNavigation { get; set; } = null!;
}
