namespace ViteSales.Data.Entities;

public partial class MemberType
{
    public long AutoKey { get; set; }

    public string MemberTypeCode { get; set; } = null!;

    public string? Description { get; set; }

    public byte Level { get; set; }

    public Guid Guid { get; set; }

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();
}
