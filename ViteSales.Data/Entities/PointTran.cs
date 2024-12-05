namespace ViteSales.Data.Entities;

public partial class PointTran
{
    public long PointTransKey { get; set; }

    public string SourceType { get; set; } = null!;

    public string MemberNo { get; set; } = null!;

    public string DocNo { get; set; } = null!;

    public DateTime DocDate { get; set; }

    public decimal Points { get; set; }

    public Guid SourceGuid { get; set; }

    public Guid Guid { get; set; }

    public string? PointType { get; set; }

    public virtual Member MemberNoNavigation { get; set; } = null!;
}
