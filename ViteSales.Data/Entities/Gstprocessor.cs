namespace ViteSales.Data.Entities;

public partial class Gstprocessor
{
    public long Gstkey { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public byte Duration { get; set; }

    public string Submitted { get; set; } = null!;

    public byte[]? TaxDataReport { get; set; }

    public long? JedocKey { get; set; }

    public string? CarryForwardRefundGst { get; set; }

    public string? AuthorizerName { get; set; }

    public string? NewIcno { get; set; }

    public string? OldIcno { get; set; }

    public string? PassportNo { get; set; }

    public string? Nationality { get; set; }

    public string Compressed { get; set; } = null!;

    public string? CimbstatusCode { get; set; }

    public string? CimbstatusDesc { get; set; }

    public string? CimbrefNo { get; set; }

    public string? CimbmediaNo { get; set; }

    public long? Cbkey { get; set; }

    public long? JegainLossDocKey { get; set; }

    public long? JefixedAssetAndStockValueDocKey { get; set; }

    public DateTime? DeclarationDate { get; set; }

    public DateTime? CreatedTimeStamp { get; set; }

    public string? CreatedUserId { get; set; }

    public long? ParentGstkey { get; set; }

    public int? Seq { get; set; }

    public string? IsPartialExemption { get; set; }

    public string? IsCapitalGoodsAdj { get; set; }

    public string? IsFinalGstreturn { get; set; }

    public string? ProductVersion { get; set; }

    public string? Gafversion { get; set; }

    public int Gstversion { get; set; }
}
