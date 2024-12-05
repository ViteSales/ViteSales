namespace ViteSales.Data.Entities;

public partial class GstcapitalGood
{
    public long DocKey { get; set; }

    public string CapitalCode { get; set; } = null!;

    public string? Description { get; set; }

    public string? FurtherDescription { get; set; }

    public DateTime CommenceDate { get; set; }

    public DateTime? DisposalDate { get; set; }

    public string IsLost { get; set; } = null!;

    public string? DocumentLink { get; set; }

    public int NumOfInterval { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? TotalTax { get; set; }

    public virtual ICollection<GstcapitalGoodsDtl> GstcapitalGoodsDtls { get; set; } = new List<GstcapitalGoodsDtl>();

    public virtual ICollection<GstcapitalGoodsScheduleAdj> GstcapitalGoodsScheduleAdjs { get; set; } = new List<GstcapitalGoodsScheduleAdj>();

    public virtual ICollection<GstcapitalGoodsSchedule> GstcapitalGoodsSchedules { get; set; } = new List<GstcapitalGoodsSchedule>();
}
