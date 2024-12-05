namespace ViteSales.Data.Entities;

public partial class Bsformat
{
    public int AutoKey { get; set; }

    public int Seq { get; set; }

    public string RowType { get; set; } = null!;

    public string? AccType { get; set; }

    public string? Description { get; set; }

    public string CreditAsPositive { get; set; } = null!;

    public virtual AccType? AccTypeNavigation { get; set; }
}
