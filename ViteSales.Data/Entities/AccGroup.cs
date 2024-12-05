namespace ViteSales.Data.Entities;

public partial class AccGroup
{
    public long AutoKey { get; set; }

    public int Seq { get; set; }

    public string AccType { get; set; } = null!;

    public string NormalBalance { get; set; } = null!;

    public string Group { get; set; } = null!;

    public int Level { get; set; }

    public virtual AccType AccTypeNavigation { get; set; } = null!;
}
