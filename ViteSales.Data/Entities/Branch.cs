namespace ViteSales.Data.Entities;

public partial class Branch
{
    public long AutoKey { get; set; }

    public string AccNo { get; set; } = null!;

    public string BranchCode { get; set; } = null!;

    public string? BranchName { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? Address3 { get; set; }

    public string? Address4 { get; set; }

    public string? PostCode { get; set; }

    public string? Contact { get; set; }

    public string? Phone1 { get; set; }

    public string? Phone2 { get; set; }

    public string? Fax1 { get; set; }

    public string? Fax2 { get; set; }

    public int LastUpdate { get; set; }

    public string? AreaCode { get; set; }

    public string? SalesAgent { get; set; }

    public string? PurchaseAgent { get; set; }

    public string? EmailAddress { get; set; }

    public string IsActive { get; set; } = null!;

    public string? Mobile { get; set; }

    public decimal? UdfWeightCharges { get; set; }

    public decimal? UdfHandleCharges { get; set; }

    public int? TaxEntityId { get; set; }

    public virtual Glmast AccNoNavigation { get; set; } = null!;

    public virtual ICollection<Advqt> Advqts { get; set; } = new List<Advqt>();

    public virtual ICollection<Apcn> Apcns { get; set; } = new List<Apcn>();

    public virtual ICollection<Apdn> Apdns { get; set; } = new List<Apdn>();

    public virtual ICollection<Apinvoice> Apinvoices { get; set; } = new List<Apinvoice>();

    public virtual ICollection<Appayment> Appayments { get; set; } = new List<Appayment>();

    public virtual ICollection<Aprefund> Aprefunds { get; set; } = new List<Aprefund>();

    public virtual ICollection<Arapcontra> ArapcontraBranchNavigations { get; set; } = new List<Arapcontra>();

    public virtual ICollection<Arapcontra> ArapcontraBranches { get; set; } = new List<Arapcontra>();

    public virtual ICollection<Arcn> Arcns { get; set; } = new List<Arcn>();

    public virtual ICollection<Ardn> Ardns { get; set; } = new List<Ardn>();

    public virtual Area? AreaCodeNavigation { get; set; }

    public virtual ICollection<Arinvoice> Arinvoices { get; set; } = new List<Arinvoice>();

    public virtual ICollection<Arpayment> Arpayments { get; set; } = new List<Arpayment>();

    public virtual ICollection<Arrefund> Arrefunds { get; set; } = new List<Arrefund>();

    public virtual ICollection<Cn> Cns { get; set; } = new List<Cn>();

    public virtual ICollection<ConsignmentReturn> ConsignmentReturns { get; set; } = new List<ConsignmentReturn>();

    public virtual ICollection<Consignment> Consignments { get; set; } = new List<Consignment>();

    public virtual ICollection<Cp> Cps { get; set; } = new List<Cp>();

    public virtual ICollection<CashSales> Cs { get; set; } = new List<CashSales>();

    public virtual ICollection<CsgnitemBalQty> CsgnitemBalQties { get; set; } = new List<CsgnitemBalQty>();

    public virtual ICollection<Csgnxfer> CsgnxferBranchNavigations { get; set; } = new List<Csgnxfer>();

    public virtual ICollection<Csgnxfer> CsgnxferBranches { get; set; } = new List<Csgnxfer>();

    public virtual ICollection<Dn> Dns { get; set; } = new List<Dn>();

    public virtual ICollection<Do> Dos { get; set; } = new List<Do>();

    public virtual ICollection<Dr> Drs { get; set; } = new List<Dr>();

    public virtual ICollection<Gr> Grs { get; set; } = new List<Gr>();

    public virtual ICollection<Gt> Gts { get; set; } = new List<Gt>();

    public virtual ICollection<Iphist> Iphists { get; set; } = new List<Iphist>();

    public virtual ICollection<Iv> Ivs { get; set; } = new List<Iv>();

    public virtual ICollection<Pi> Pis { get; set; } = new List<Pi>();

    public virtual ICollection<Po> Pos { get; set; } = new List<Po>();

    public virtual ICollection<Pr> Prs { get; set; } = new List<Pr>();

    public virtual PurchaseAgent? PurchaseAgentNavigation { get; set; }

    public virtual ICollection<PurchaseConsignmentReturn> PurchaseConsignmentReturns { get; set; } = new List<PurchaseConsignmentReturn>();

    public virtual ICollection<PurchaseConsignment> PurchaseConsignments { get; set; } = new List<PurchaseConsignment>();

    public virtual ICollection<Qt> Qts { get; set; } = new List<Qt>();

    public virtual ICollection<Rq> Rqs { get; set; } = new List<Rq>();

    public virtual SalesAgent? SalesAgentNavigation { get; set; }

    public virtual ICollection<So> Sos { get; set; } = new List<So>();

    public virtual TaxEntity? TaxEntity { get; set; }

    public virtual ICollection<Xs> Xes { get; set; } = new List<Xs>();

    public virtual ICollection<Xp> Xps { get; set; } = new List<Xp>();
}
