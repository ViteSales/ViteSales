namespace ViteSales.Data.Entities;

public partial class StockDtl
{
    public long StockDtlkey { get; set; }

    public string ItemCode { get; set; } = null!;

    public string Uom { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string? BatchNo { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public DateTime DocDate { get; set; }

    public long Seq { get; set; }

    public string DocType { get; set; } = null!;

    public long DocKey { get; set; }

    public long DtlKey { get; set; }

    public decimal Qty { get; set; }

    public decimal Cost { get; set; }

    public decimal AdjustedCost { get; set; }

    public decimal TotalCost { get; set; }

    public byte CostType { get; set; }

    public DateTime LastModified { get; set; }

    public long ReferTo { get; set; }

    public decimal InputCost { get; set; }

    public string? DocInfo { get; set; }

    public long Seq2 { get; set; }

    public decimal CftotalQty { get; set; }

    public decimal CftotalCost { get; set; }

    public DateTime CreatedTimeStamp { get; set; }

    public DateTime AdjustedDate { get; set; }

    public long? StockTransQueueKey { get; set; }

    public decimal ReportingCost { get; set; }

    public decimal ReportingTotalCost { get; set; }

    public string? Fifocost { get; set; }

    public string? ReportingFifocost { get; set; }

    public string? BalanceFifo { get; set; }

    public string? ReportingBalanceFifo { get; set; }

    public decimal? CftotalAdjustedCost { get; set; }

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual ItemUom ItemUom { get; set; } = null!;

    public virtual Location LocationNavigation { get; set; } = null!;

    public virtual Project? ProjNoNavigation { get; set; }
}
