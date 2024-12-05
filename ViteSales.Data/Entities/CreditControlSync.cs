namespace ViteSales.Data.Entities;

public partial class CreditControlSync
{
    public long DocKey { get; set; }

    public string DocNo { get; set; } = null!;

    public DateTime DocDate { get; set; }

    public string AccNo { get; set; } = null!;

    public string? CompanyName { get; set; }

    public decimal? CreditLimit { get; set; }

    public decimal? OverdueLimit { get; set; }

    public decimal? CurrentCredit { get; set; }

    public decimal? CurrentOverdue { get; set; }

    public string? OverdueDetail { get; set; }

    public string? Action { get; set; }

    public string? ComputerName { get; set; }

    public DateTime? RequestDateTime { get; set; }

    public string? CreatedUserId { get; set; }

    public virtual Glmast AccNoNavigation { get; set; } = null!;

    public virtual User? CreatedUser { get; set; }
}
