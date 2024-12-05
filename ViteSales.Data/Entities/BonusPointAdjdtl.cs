namespace ViteSales.Data.Entities;

public partial class BonusPointAdjdtl
{
    public long DtlKey { get; set; }

    public long DocKey { get; set; }

    public int Seq { get; set; }

    public string? MemberNo { get; set; }

    public decimal? Point { get; set; }

    public string PrintOut { get; set; } = null!;

    public Guid Guid { get; set; }

    public virtual Member? MemberNoNavigation { get; set; }
}
