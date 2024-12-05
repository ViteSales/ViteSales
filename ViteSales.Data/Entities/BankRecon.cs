namespace ViteSales.Data.Entities;

public partial class BankRecon
{
    public long AutoKey { get; set; }

    public string AccNo { get; set; } = null!;

    public DateTime BankStatementDate { get; set; }

    public decimal? ActualBankStatementBalance { get; set; }

    public virtual Glmast AccNoNavigation { get; set; } = null!;

    public virtual ICollection<BankTran> BankTrans { get; set; } = new List<BankTran>();
}
