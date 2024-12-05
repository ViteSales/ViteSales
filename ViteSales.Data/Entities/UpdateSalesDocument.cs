namespace ViteSales.Data.Entities;

public partial class UpdateSalesDocument
{
    public long DocKey { get; set; }

    public string DocType { get; set; } = null!;

    public DateTime DocDate { get; set; }

    public bool IsAssigned { get; set; }
}
