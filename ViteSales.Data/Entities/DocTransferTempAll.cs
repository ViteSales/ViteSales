namespace ViteSales.Data.Entities;

public partial class DocTransferTempAll
{
    public string DocType { get; set; } = null!;

    public string DocNo { get; set; } = null!;

    public DateTime DocDate { get; set; }

    public long DocKey { get; set; }

    public long DtlKey { get; set; }

    public string? ItemCode { get; set; }

    public decimal? Qty { get; set; }

    public decimal? Focqty { get; set; }

    public string? FromDocType { get; set; }

    public string? FromDocNo { get; set; }

    public long? FromDocDtlKey { get; set; }

    public string? FullTransferOption { get; set; }

    public string? FullTransferFromDocList { get; set; }

    public long TransferKey { get; set; }

    public string Success { get; set; } = null!;
}
