namespace ViteSales.Data.Entities;

public partial class TaxDocNoDtl
{
    public long TaxDocNoDtlkey { get; set; }

    public int TaxDocNoKey { get; set; }

    public string Number { get; set; } = null!;

    public string? UseInDocType { get; set; }

    public long? UseInDocKey { get; set; }

    public string? UseInDocNo { get; set; }

    public string Void { get; set; } = null!;
}
