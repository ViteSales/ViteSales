namespace ViteSales.Data.Entities;

public partial class SerialNoTran
{
    public long TransKey { get; set; }

    public string? DocType { get; set; }

    public long? DocKey { get; set; }

    public long? DtlKey { get; set; }

    public string? FromSerialNo { get; set; }

    public string? ToSerialNo { get; set; }

    public long? Count { get; set; }

    public string? ItemCode { get; set; }

    public string? TransferedSn { get; set; }

    public long? FromDocDtlKey { get; set; }

    public DateTime? ManufacturedDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public DateTime? LastSalesDate { get; set; }

    public string? Remarks { get; set; }

    public string? Cancelled { get; set; }

    public string? Note { get; set; }

    public DateTime? DocDate { get; set; }

    public Guid Guid { get; set; }

    public virtual Item? ItemCodeNavigation { get; set; }
}
