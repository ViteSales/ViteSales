namespace ViteSales.Data.Entities;

public partial class Term
{
    public long AutoKey { get; set; }

    public string DisplayTerm { get; set; } = null!;

    public string? Terms { get; set; }

    public int LastUpdate { get; set; }

    public string? TermType { get; set; }

    public int? TermDays { get; set; }

    public int? DiscountDays { get; set; }

    public decimal? DiscountPercent { get; set; }

    public virtual ICollection<Advqt> Advqts { get; set; } = new List<Advqt>();

    public virtual ICollection<Apdn> Apdns { get; set; } = new List<Apdn>();

    public virtual ICollection<Apinvoice> Apinvoices { get; set; } = new List<Apinvoice>();

    public virtual ICollection<Ardn> Ardns { get; set; } = new List<Ardn>();

    public virtual ICollection<Arinvoice> Arinvoices { get; set; } = new List<Arinvoice>();

    public virtual ICollection<Cn> Cns { get; set; } = new List<Cn>();

    public virtual ICollection<ConsignmentReturn> ConsignmentReturns { get; set; } = new List<ConsignmentReturn>();

    public virtual ICollection<Consignment> Consignments { get; set; } = new List<Consignment>();

    public virtual ICollection<Cp> Cps { get; set; } = new List<Cp>();

    public virtual ICollection<Creditor> Creditors { get; set; } = new List<Creditor>();

    public virtual ICollection<CashSales> Cs { get; set; } = new List<CashSales>();

    public virtual ICollection<Debtor> Debtors { get; set; } = new List<Debtor>();

    public virtual ICollection<Dn> Dns { get; set; } = new List<Dn>();

    public virtual ICollection<Do> Dos { get; set; } = new List<Do>();

    public virtual ICollection<Dr> Drs { get; set; } = new List<Dr>();

    public virtual ICollection<Gr> Grs { get; set; } = new List<Gr>();

    public virtual ICollection<Gt> Gts { get; set; } = new List<Gt>();

    public virtual ICollection<Iv> Ivs { get; set; } = new List<Iv>();

    public virtual ICollection<Pi> Pis { get; set; } = new List<Pi>();

    public virtual ICollection<Po> Pos { get; set; } = new List<Po>();

    public virtual ICollection<PriceBookRule> PriceBookRules { get; set; } = new List<PriceBookRule>();

    public virtual ICollection<Pr> Prs { get; set; } = new List<Pr>();

    public virtual ICollection<PurchaseConsignmentReturn> PurchaseConsignmentReturns { get; set; } = new List<PurchaseConsignmentReturn>();

    public virtual ICollection<PurchaseConsignment> PurchaseConsignments { get; set; } = new List<PurchaseConsignment>();

    public virtual ICollection<Qt> Qts { get; set; } = new List<Qt>();

    public virtual ICollection<Rq> Rqs { get; set; } = new List<Rq>();

    public virtual ICollection<So> Sos { get; set; } = new List<So>();

    public virtual ICollection<Xs> Xes { get; set; } = new List<Xs>();

    public virtual ICollection<Xp> Xps { get; set; } = new List<Xp>();
}
