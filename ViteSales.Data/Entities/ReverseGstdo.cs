namespace ViteSales.Data.Entities;

public partial class ReverseGstdo
{
    public long DocKey { get; set; }

    public string DocType { get; set; } = null!;

    public long JedocKey { get; set; }
}
