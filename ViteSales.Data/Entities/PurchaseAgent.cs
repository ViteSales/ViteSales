namespace ViteSales.Data.Entities;

public partial class PurchaseAgent
{
    public long AutoKey { get; set; }

    public string PurchaseAgent1 { get; set; } = null!;

    public string? Description { get; set; }

    public string? Desc2 { get; set; }

    public string IsActive { get; set; } = null!;

    public int LastUpdate { get; set; }

    public byte[]? Signature { get; set; }

    public virtual ICollection<Apdn> Apdns { get; set; } = new List<Apdn>();

    public virtual ICollection<Apinvoice> Apinvoices { get; set; } = new List<Apinvoice>();

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual ICollection<Cp> Cps { get; set; } = new List<Cp>();

    public virtual ICollection<Creditor> Creditors { get; set; } = new List<Creditor>();

    public virtual ICollection<Gr> Grs { get; set; } = new List<Gr>();

    public virtual ICollection<Gt> Gts { get; set; } = new List<Gt>();

    public virtual ICollection<Pi> Pis { get; set; } = new List<Pi>();

    public virtual ICollection<Po> Pos { get; set; } = new List<Po>();

    public virtual ICollection<Pr> Prs { get; set; } = new List<Pr>();

    public virtual ICollection<PurchaseConsignmentReturn> PurchaseConsignmentReturns { get; set; } = new List<PurchaseConsignmentReturn>();

    public virtual ICollection<PurchaseConsignment> PurchaseConsignments { get; set; } = new List<PurchaseConsignment>();

    public virtual ICollection<Rq> Rqs { get; set; } = new List<Rq>();

    public virtual ICollection<Xp> Xps { get; set; } = new List<Xp>();
}
