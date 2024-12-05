namespace ViteSales.Data.Entities;

public partial class StockTransQueue
{
    public long StockTransQueueKey { get; set; }

    public string? ItemCode { get; set; }

    public long? StockDtlkey { get; set; }

    public long? DocKey { get; set; }

    public long? DtlKey { get; set; }

    public long? ReferTo { get; set; }

    public string? Uom { get; set; }

    public string? Location { get; set; }

    public string? BatchNo { get; set; }

    public string? DocType { get; set; }

    public DateTime? DocDate { get; set; }

    public decimal? Qty { get; set; }

    public int RecordState { get; set; }

    public int? UnlockedPeriod { get; set; }

    public bool IsRecalculate { get; set; }

    public bool PostFromUpdateStockCost { get; set; }

    public int? CostingMethod { get; set; }

    public DateTime? AdjustedDate { get; set; }

    public DateTime CreatedTimeStamp { get; set; }

    public int RetryCount { get; set; }

    public bool CheckTrialPlanLimit { get; set; }
}
