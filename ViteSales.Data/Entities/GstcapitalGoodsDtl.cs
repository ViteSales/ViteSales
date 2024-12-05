namespace ViteSales.Data.Entities;

public partial class GstcapitalGoodsDtl
{
    public long DtlKey { get; set; }

    public long DocKey { get; set; }

    public long? ParentDtlKey { get; set; }

    public int Seq { get; set; }

    public string? Description { get; set; }

    public string? FurtherDescription { get; set; }

    public DateTime IncurredDate { get; set; }

    public DateTime? DisposalDate { get; set; }

    public string IsLost { get; set; } = null!;

    public decimal? Amount { get; set; }

    public decimal? Tax { get; set; }

    public virtual GstcapitalGood DocKeyNavigation { get; set; } = null!;
}
