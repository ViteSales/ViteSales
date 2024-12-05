namespace ViteSales.Data.Entities;

public partial class GstcapitalGoodsSchedule
{
    public long DocKey { get; set; }

    public long DtlKey { get; set; }

    public int Gstseq { get; set; }

    public int Interval { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public decimal? Retax { get; set; }

    public decimal? Irrate { get; set; }

    public decimal? RetaxClaim { get; set; }

    public long? JeadjDocKey { get; set; }

    public string? IrrateCalcContent { get; set; }

    public virtual GstcapitalGood DocKeyNavigation { get; set; } = null!;
}
