namespace ViteSales.Data.Entities;

public partial class Race
{
    public long AutoKey { get; set; }

    public string Race1 { get; set; } = null!;

    public string? Description { get; set; }

    public Guid Guid { get; set; }

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();
}
