namespace ViteSales.Data.Entities;

public partial class ArapbadDebt
{
    public string DocType { get; set; } = null!;

    public long DocKey { get; set; }

    public long? JedocKey { get; set; }

    public DateTime? JedocDate { get; set; }

    public long? BdcndocKey { get; set; }

    public DateTime? BdcndocDate { get; set; }

    public long? BdwithBdrcndocKey { get; set; }

    public DateTime? BdwithBdrcndocDate { get; set; }

    public int? BadDebtType { get; set; }

    public virtual ICollection<ArapbadDebtRecovery> ArapbadDebtRecoveries { get; set; } = new List<ArapbadDebtRecovery>();
}
