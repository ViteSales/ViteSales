namespace ViteSales.Data.Entities;

public partial class TaxEntity
{
    public int TaxEntityId { get; set; }

    public string Name { get; set; } = null!;

    public string? IdentityNo { get; set; }

    public string FullTin { get; set; } = null!;

    public string Tin { get; set; } = null!;

    public string TaxBranchId { get; set; } = null!;

    public string? Address { get; set; }

    public string? PostCode { get; set; }

    public string? Phone { get; set; }

    public string? EmailAddress { get; set; }

    public int TaxCategory { get; set; }

    public int TaxClassification { get; set; }

    public string? GstregisterNo { get; set; }

    public string? SstregisterNo { get; set; }

    public string? TradeName { get; set; }

    public string? TourismTaxRegisterNo { get; set; }

    public string? BusinessActivityDesc { get; set; }

    public string? Msiccode { get; set; }

    public string? City { get; set; }

    public string? StateCode { get; set; }

    public string? CountryCode { get; set; }

    public string? IdentityType { get; set; }

    public Guid Guid { get; set; }

    public virtual ICollection<Advqt> Advqts { get; set; } = new List<Advqt>();

    public virtual ICollection<Apcn> Apcns { get; set; } = new List<Apcn>();

    public virtual ICollection<Apdn> Apdns { get; set; } = new List<Apdn>();

    public virtual ICollection<Apinvoice> Apinvoices { get; set; } = new List<Apinvoice>();

    public virtual ICollection<Arcn> Arcns { get; set; } = new List<Arcn>();

    public virtual ICollection<Ardn> Ardns { get; set; } = new List<Ardn>();

    public virtual ICollection<Arinvoice> Arinvoices { get; set; } = new List<Arinvoice>();

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual ICollection<Cbdtl> Cbdtls { get; set; } = new List<Cbdtl>();

    public virtual ICollection<Cb> Cbs { get; set; } = new List<Cb>();

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

    public virtual ICollection<Jedtl> Jedtls { get; set; } = new List<Jedtl>();

    public virtual ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();

    public virtual ICollection<Pi> Pis { get; set; } = new List<Pi>();

    public virtual ICollection<Po> Pos { get; set; } = new List<Po>();

    public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();

    public virtual ICollection<Pr> Prs { get; set; } = new List<Pr>();

    public virtual ICollection<PurchaseConsignmentReturn> PurchaseConsignmentReturns { get; set; } = new List<PurchaseConsignmentReturn>();

    public virtual ICollection<PurchaseConsignment> PurchaseConsignments { get; set; } = new List<PurchaseConsignment>();

    public virtual ICollection<Qt> Qts { get; set; } = new List<Qt>();

    public virtual ICollection<Rq> Rqs { get; set; } = new List<Rq>();

    public virtual ICollection<So> Sos { get; set; } = new List<So>();

    public virtual ICollection<TaxTran> TaxTrans { get; set; } = new List<TaxTran>();

    public virtual ICollection<TaxTransAuditDtl> TaxTransAuditDtls { get; set; } = new List<TaxTransAuditDtl>();

    public virtual ICollection<TaxTransCancelled> TaxTransCancelleds { get; set; } = new List<TaxTransCancelled>();

    public virtual ICollection<WithholdingTaxTran> WithholdingTaxTrans { get; set; } = new List<WithholdingTaxTran>();

    public virtual ICollection<Xs> Xes { get; set; } = new List<Xs>();

    public virtual ICollection<Xp> Xps { get; set; } = new List<Xp>();
}
