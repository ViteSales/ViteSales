namespace ViteSales.Data.Entities;

public partial class SalesAgent
{
    public long AutoKey { get; set; }

    public string SalesAgent1 { get; set; } = null!;

    public string? Description { get; set; }

    public string? Desc2 { get; set; }

    public string IsActive { get; set; } = null!;

    public int LastUpdate { get; set; }

    public byte[]? Signature { get; set; }

    public Guid Guid { get; set; }

    public string? EmailAddress { get; set; }

    public string? ApproverEmailAddress { get; set; }

    public decimal? UdfCommRate { get; set; }

    public virtual ICollection<Advqt> Advqts { get; set; } = new List<Advqt>();

    public virtual ICollection<Ardn> Ardns { get; set; } = new List<Ardn>();

    public virtual ICollection<Arinvoice> Arinvoices { get; set; } = new List<Arinvoice>();

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual ICollection<Cbdtl> Cbdtls { get; set; } = new List<Cbdtl>();

    public virtual ICollection<Cn> Cns { get; set; } = new List<Cn>();

    public virtual ICollection<Commission> Commissions { get; set; } = new List<Commission>();

    public virtual ICollection<ConsignmentReturn> ConsignmentReturns { get; set; } = new List<ConsignmentReturn>();

    public virtual ICollection<Consignment> Consignments { get; set; } = new List<Consignment>();

    public virtual ICollection<CashSales> Cs { get; set; } = new List<CashSales>();

    public virtual ICollection<Csgnxfer> Csgnxfers { get; set; } = new List<Csgnxfer>();

    public virtual ICollection<Debtor> Debtors { get; set; } = new List<Debtor>();

    public virtual ICollection<Dn> Dns { get; set; } = new List<Dn>();

    public virtual ICollection<Do> Dos { get; set; } = new List<Do>();

    public virtual ICollection<Dr> Drs { get; set; } = new List<Dr>();

    public virtual ICollection<Iv> Ivs { get; set; } = new List<Iv>();

    public virtual ICollection<Jedtl> Jedtls { get; set; } = new List<Jedtl>();

    public virtual ICollection<PriceBookRule> PriceBookRuleFromSalesAgentNavigations { get; set; } = new List<PriceBookRule>();

    public virtual ICollection<PriceBookRule> PriceBookRuleToSalesAgentNavigations { get; set; } = new List<PriceBookRule>();

    public virtual ICollection<Qt> Qts { get; set; } = new List<Qt>();

    public virtual ICollection<So> Sos { get; set; } = new List<So>();

    public virtual ICollection<Xs> Xes { get; set; } = new List<Xs>();
}
