namespace ViteSales.Data.Entities;

public partial class Area
{
    public long AutoKey { get; set; }

    public string AreaCode { get; set; } = null!;

    public string? Description { get; set; }

    public string? Desc2 { get; set; }

    public int LastUpdate { get; set; }

    public Guid Guid { get; set; }

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual ICollection<Creditor> Creditors { get; set; } = new List<Creditor>();

    public virtual ICollection<Debtor> Debtors { get; set; } = new List<Debtor>();

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    public virtual ICollection<PriceBookRule> PriceBookRuleFromAreaCodeNavigations { get; set; } = new List<PriceBookRule>();

    public virtual ICollection<PriceBookRule> PriceBookRuleToAreaCodeNavigations { get; set; } = new List<PriceBookRule>();
}
