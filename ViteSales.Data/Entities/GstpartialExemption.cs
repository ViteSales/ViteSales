namespace ViteSales.Data.Entities;

public partial class GstpartialExemption
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int Gstseq { get; set; }

    public string IsRemainPeriod { get; set; } = null!;

    public decimal? TtaxableAmt { get; set; }

    public decimal? EtaxableAmt { get; set; }

    public decimal? O1taxableAmt { get; set; }

    public decimal? O2taxableAmt { get; set; }

    public decimal? R1O1taxableAmt { get; set; }

    public decimal? R1O2taxableAmt { get; set; }

    public decimal? R2taxableAmt { get; set; }

    public decimal? R3taxableAmt { get; set; }

    public decimal? R4taxableAmt { get; set; }

    public decimal? R5taxableAmt { get; set; }

    public decimal? Estax { get; set; }

    public decimal? Retax { get; set; }

    public string? IsDeMinimis { get; set; }

    public decimal? Irrate { get; set; }

    public decimal? EstaxClaim { get; set; }

    public decimal? RetaxClaim { get; set; }

    public decimal? EstaxClaimPeriodAdj { get; set; }

    public decimal? RetaxClaimPeriodAdj { get; set; }

    public long? JeperiodDocKey { get; set; }

    public long? JeperiodAdjDocKey { get; set; }

    public long? JeannualAdjDocKey { get; set; }

    public long? JeannualAdjDocKey2 { get; set; }

    public string? IsDeMinimisCalcContent { get; set; }

    public string? IrrateCalcContent { get; set; }

    public string? ProcessJeperiodAdj { get; set; }
}
