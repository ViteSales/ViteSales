namespace ViteSales.Data.Entities;

public partial class TaxDocNo
{
    public int TaxDocNoKey { get; set; }

    public long? NextNumber { get; set; }

    public long FromNumber { get; set; }

    public long ToNumber { get; set; }

    public int Count { get; set; }

    public int UseCount { get; set; }

    public int VoidCount { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string Format { get; set; } = null!;

    public string IsDefault { get; set; } = null!;
}
