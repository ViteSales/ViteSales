namespace ViteSales.Data.Entities;

public partial class Cntype
{
    public long AutoKey { get; set; }

    public string CnTypeCode { get; set; } = null!;

    public string? Description { get; set; }

    public string? Desc2 { get; set; }

    public string IsActive { get; set; } = null!;

    public int LastUpdate { get; set; }

    public virtual ICollection<Apcn> Apcns { get; set; } = new List<Apcn>();

    public virtual ICollection<Arcn> Arcns { get; set; } = new List<Arcn>();

    public virtual ICollection<Cn> Cns { get; set; } = new List<Cn>();
}
