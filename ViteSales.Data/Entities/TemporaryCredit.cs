namespace ViteSales.Data.Entities;

public partial class TemporaryCredit
{
    public long AutoKey { get; set; }

    public string AccNo { get; set; } = null!;

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public decimal? CreditLimit { get; set; }

    public decimal? OverdueLimit { get; set; }

    public string? Remark { get; set; }

    public virtual Glmast AccNoNavigation { get; set; } = null!;
}
