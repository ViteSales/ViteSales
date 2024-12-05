namespace ViteSales.Data.Entities;

public partial class UpdateSalesDocumentDtl
{
    public long DtlKey { get; set; }

    public string DocType { get; set; } = null!;

    public DateTime DocDate { get; set; }

    public decimal LocalTotalCost { get; set; }

    public bool DetailTotalCostUpdated { get; set; }

    public decimal LocalFoctotalCost { get; set; }

    public bool LocalFoctotalCostUpdated { get; set; }
}
