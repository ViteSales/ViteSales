namespace ViteSales.Data.Entities;

public partial class GstcapitalGoodsScheduleAdj
{
    public long DocKey { get; set; }

    public long DtlKey { get; set; }

    public int Gstseq { get; set; }

    public int Interval { get; set; }

    public int IntervalAdj { get; set; }

    public decimal? Cgarate { get; set; }

    public decimal? Cgaamount { get; set; }

    public string? CgarateCalcContent { get; set; }

    public string? CgaamountCalcContent { get; set; }

    public virtual GstcapitalGood DocKeyNavigation { get; set; } = null!;
}
