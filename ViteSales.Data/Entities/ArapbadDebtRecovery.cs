namespace ViteSales.Data.Entities;

public partial class ArapbadDebtRecovery
{
    public string DocType { get; set; } = null!;

    public long DocKey { get; set; }

    public string PaymentDocType { get; set; } = null!;

    public long PaymentDocKey { get; set; }

    public long? JedocKey { get; set; }

    public long? JedrdtlKey { get; set; }

    public long? JecrdtlKey { get; set; }

    public DateTime? JedocDate { get; set; }

    public virtual ArapbadDebt ArapbadDebt { get; set; } = null!;
}
