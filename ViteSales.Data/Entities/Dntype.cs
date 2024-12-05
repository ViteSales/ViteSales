namespace ViteSales.Data.Entities;

public partial class Dntype
{
    public long AutoKey { get; set; }

    public string DnTypeCode { get; set; } = null!;

    public string? Description { get; set; }

    public string? Desc2 { get; set; }

    public string IsActive { get; set; } = null!;

    public int LastUpdate { get; set; }

    public virtual ICollection<Apdn> Apdns { get; set; } = new List<Apdn>();

    public virtual ICollection<Ardn> Ardns { get; set; } = new List<Ardn>();

    public virtual ICollection<Dn> Dns { get; set; } = new List<Dn>();
}
