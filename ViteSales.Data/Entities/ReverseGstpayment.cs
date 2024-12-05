namespace ViteSales.Data.Entities;

public partial class ReverseGstpayment
{
    public long DocKey { get; set; }

    public string DocType { get; set; } = null!;

    public long AppliedDocKey { get; set; }

    public string AppliedDocType { get; set; } = null!;

    public long JedocKey { get; set; }
}
