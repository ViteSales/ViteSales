namespace ViteSales.Data.Entities;

public partial class DocNoFormatAccNo
{
    public long AutoKey { get; set; }

    public string Name { get; set; } = null!;

    public string AccNo { get; set; } = null!;

    public virtual Glmast AccNoNavigation { get; set; } = null!;

    public virtual DocNoFormat NameNavigation { get; set; } = null!;
}
