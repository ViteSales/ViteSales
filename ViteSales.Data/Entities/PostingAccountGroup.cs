namespace ViteSales.Data.Entities;

public partial class PostingAccountGroup
{
    public long AutoKey { get; set; }

    public string AccountGroup { get; set; } = null!;

    public string? Description { get; set; }

    public string? Desc2 { get; set; }

    public string? SalesCode { get; set; }

    public string? CashSalesCode { get; set; }

    public string? SalesReturnCode { get; set; }

    public string? SalesDiscountCode { get; set; }

    public string? PurchaseDiscountCode { get; set; }

    public string? PurchaseCode { get; set; }

    public string? PurchaseReturnCode { get; set; }

    public int LastUpdate { get; set; }

    public string? CashPurchaseCode { get; set; }

    public virtual Glmast? CashPurchaseCodeNavigation { get; set; }

    public virtual Glmast? CashSalesCodeNavigation { get; set; }

    public virtual Glmast? PurchaseCodeNavigation { get; set; }

    public virtual Glmast? PurchaseDiscountCodeNavigation { get; set; }

    public virtual Glmast? PurchaseReturnCodeNavigation { get; set; }

    public virtual Glmast? SalesCodeNavigation { get; set; }

    public virtual Glmast? SalesDiscountCodeNavigation { get; set; }

    public virtual Glmast? SalesReturnCodeNavigation { get; set; }
}
