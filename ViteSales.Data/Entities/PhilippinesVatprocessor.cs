namespace ViteSales.Data.Entities;

public partial class PhilippinesVatprocessor
{
    public long Vatkey { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public byte Duration { get; set; }

    public string Submitted { get; set; } = null!;

    public byte[]? TaxDataReport { get; set; }

    public byte[]? TaxSummaryDataReport { get; set; }

    public decimal TotalAmountPayable { get; set; }

    public long? JedocKey { get; set; }

    public string? Period { get; set; }

    public int Quarter { get; set; }

    public string? TaxRelief { get; set; }

    public string ShortPeriodReturn { get; set; } = null!;

    public DateTime? CreatedTimeStamp { get; set; }

    public string? CreatedUserId { get; set; }

    public long? ParentVatkey { get; set; }

    public int? Seq { get; set; }

    public string? ProductVersion { get; set; }

    public int Vatversion { get; set; }
}
