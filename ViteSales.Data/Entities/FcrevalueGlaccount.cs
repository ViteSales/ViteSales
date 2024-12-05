namespace ViteSales.Data.Entities;

public partial class FcrevalueGlaccount
{
    public long DtlKey { get; set; }

    public long FcrevalueKey { get; set; }

    public string AccNo { get; set; } = null!;

    public decimal RevalueRate { get; set; }

    public decimal Balance { get; set; }

    public decimal NewHomeBalance { get; set; }

    public decimal HomeBalance { get; set; }

    public decimal GainLoss { get; set; }

    public virtual Glmast AccNoNavigation { get; set; } = null!;
}
