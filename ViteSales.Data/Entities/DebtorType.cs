namespace ViteSales.Data.Entities;

public partial class DebtorType
{
    public long AutoKey { get; set; }

    public string DebtorTypeCode { get; set; } = null!;

    public string? Description { get; set; }

    public string? Desc2 { get; set; }

    public string IsActive { get; set; } = null!;

    public int LastUpdate { get; set; }

    public Guid Guid { get; set; }

    public virtual ICollection<Debtor> Debtors { get; set; } = new List<Debtor>();

    public virtual ICollection<PriceBookRule> PriceBookRuleFromDebtorTypeNavigations { get; set; } = new List<PriceBookRule>();

    public virtual ICollection<PriceBookRule> PriceBookRuleToDebtorTypeNavigations { get; set; } = new List<PriceBookRule>();
}
