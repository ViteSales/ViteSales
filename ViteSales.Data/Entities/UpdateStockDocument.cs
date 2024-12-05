namespace ViteSales.Data.Entities;

public partial class UpdateStockDocument
{
    public long DocKey { get; set; }

    public long DtlKey { get; set; }

    public string DocType { get; set; } = null!;

    public DateTime DocDate { get; set; }

    public decimal LocalTotalCost { get; set; }

    public bool PostFromUpdateStockCost { get; set; }
}
