namespace ViteSales.Data.Entities;

public partial class DocTransferTempMatch
{
    public long TransferKey { get; set; }

    public string? FromDocType { get; set; }

    public long? FromDocKey { get; set; }

    public long? FromDocDtlKey { get; set; }

    public string? ToDocType { get; set; }

    public long? ToDocKey { get; set; }

    public long? ToDocDtlKey { get; set; }

    public decimal? Qty { get; set; }

    public decimal? Focqty { get; set; }

    public short? TransferOption { get; set; }

    public string? ActlFromDocNo { get; set; }

    public long? ActlFromDocKey { get; set; }

    public long? ActlFromDocDtlKey { get; set; }

    public decimal? ActlFromQty { get; set; }

    public decimal? ActlFromFocqty { get; set; }
}
