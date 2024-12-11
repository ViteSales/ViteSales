namespace ViteSales.Data.Entities;

public partial class TaxCodes
{
    public long AutoKey { get; set; }

    public string TaxCode { get; set; } = null!;

    public string? Description { get; set; }

    public decimal TaxRate { get; set; }

    public string Inclusive { get; set; } = null!;

    public string IsActive { get; set; } = null!;

    public int LastUpdate { get; set; }

    public string? GovtTaxCode { get; set; }

    public string SupplyPurchase { get; set; } = null!;

    public string IsDefault { get; set; } = null!;

    public string? TaxAccNo { get; set; }

    public string IsZeroRate { get; set; } = null!;

    public string UseTrxTaxAccNo { get; set; } = null!;

    public int AccountingBasis { get; set; }

    public string AddToCost { get; set; } = null!;

    public Guid Guid { get; set; }

    public string? TaxSystem { get; set; }

    public virtual ICollection<Advqt> AdvqtFooter1TaxCodeNavigations { get; set; } = new List<Advqt>();

    public virtual ICollection<Advqt> AdvqtFooter2TaxCodeNavigations { get; set; } = new List<Advqt>();

    public virtual ICollection<Advqt> AdvqtFooter3TaxCodeNavigations { get; set; } = new List<Advqt>();

    public virtual ICollection<Advqtdtl> Advqtdtls { get; set; } = new List<Advqtdtl>();

    public virtual ICollection<Apcndtl> Apcndtls { get; set; } = new List<Apcndtl>();

    public virtual ICollection<ApdepositPaymentDtl> ApdepositPaymentDtls { get; set; } = new List<ApdepositPaymentDtl>();

    public virtual ICollection<ApdepositRefundPaymentDtl> ApdepositRefundPaymentDtls { get; set; } = new List<ApdepositRefundPaymentDtl>();

    public virtual ICollection<Apdndtl> Apdndtls { get; set; } = new List<Apdndtl>();

    public virtual ICollection<ApinvoiceDtl> ApinvoiceDtls { get; set; } = new List<ApinvoiceDtl>();

    public virtual ICollection<AppaymentDtl> AppaymentDtls { get; set; } = new List<AppaymentDtl>();

    public virtual ICollection<AprefundDtl> AprefundDtls { get; set; } = new List<AprefundDtl>();

    public virtual ICollection<Arcndtl> Arcndtls { get; set; } = new List<Arcndtl>();

    public virtual ICollection<ArdepositPaymentDtl> ArdepositPaymentDtls { get; set; } = new List<ArdepositPaymentDtl>();

    public virtual ICollection<ArdepositRefundPaymentDtl> ArdepositRefundPaymentDtls { get; set; } = new List<ArdepositRefundPaymentDtl>();

    public virtual ICollection<Ardndtl> Ardndtls { get; set; } = new List<Ardndtl>();

    public virtual ICollection<ArinvoiceDtl> ArinvoiceDtls { get; set; } = new List<ArinvoiceDtl>();

    public virtual ICollection<ArpaymentDtl> ArpaymentDtls { get; set; } = new List<ArpaymentDtl>();

    public virtual ICollection<ArrefundDtl> ArrefundDtls { get; set; } = new List<ArrefundDtl>();

    public virtual ICollection<CashSales> CFooter1TaxCodeNavigations { get; set; } = new List<CashSales>();

    public virtual ICollection<CashSales> CFooter2TaxCodeNavigations { get; set; } = new List<CashSales>();

    public virtual ICollection<CashSales> CFooter3TaxCodeNavigations { get; set; } = new List<CashSales>();

    public virtual ICollection<Cbdtl> Cbdtls { get; set; } = new List<Cbdtl>();

    public virtual ICollection<Cbigdtl> Cbigdtls { get; set; } = new List<Cbigdtl>();

    public virtual ICollection<CbpaymentDtl> CbpaymentDtls { get; set; } = new List<CbpaymentDtl>();

    public virtual ICollection<Cn> CnFooter1TaxCodeNavigations { get; set; } = new List<Cn>();

    public virtual ICollection<Cn> CnFooter2TaxCodeNavigations { get; set; } = new List<Cn>();

    public virtual ICollection<Cn> CnFooter3TaxCodeNavigations { get; set; } = new List<Cn>();

    public virtual ICollection<Cndtl> Cndtls { get; set; } = new List<Cndtl>();

    public virtual ICollection<ConsignmentDtl> ConsignmentDtls { get; set; } = new List<ConsignmentDtl>();

    public virtual ICollection<ConsignmentReturnDtl> ConsignmentReturnDtls { get; set; } = new List<ConsignmentReturnDtl>();

    public virtual ICollection<Cp> CpFooter1TaxCodeNavigations { get; set; } = new List<Cp>();

    public virtual ICollection<Cp> CpFooter2TaxCodeNavigations { get; set; } = new List<Cp>();

    public virtual ICollection<Cp> CpFooter3TaxCodeNavigations { get; set; } = new List<Cp>();

    public virtual ICollection<Cpdtl> Cpdtls { get; set; } = new List<Cpdtl>();

    public virtual ICollection<Creditor> Creditors { get; set; } = new List<Creditor>();

    public virtual ICollection<CashSalesdtl> Csdtls { get; set; } = new List<CashSalesdtl>();

    public virtual ICollection<Debtor> Debtors { get; set; } = new List<Debtor>();

    public virtual ICollection<Dn> DnFooter1TaxCodeNavigations { get; set; } = new List<Dn>();

    public virtual ICollection<Dn> DnFooter2TaxCodeNavigations { get; set; } = new List<Dn>();

    public virtual ICollection<Dn> DnFooter3TaxCodeNavigations { get; set; } = new List<Dn>();

    public virtual ICollection<Dndtl> Dndtls { get; set; } = new List<Dndtl>();

    public virtual ICollection<Do> DoFooter1TaxCodeNavigations { get; set; } = new List<Do>();

    public virtual ICollection<Do> DoFooter2TaxCodeNavigations { get; set; } = new List<Do>();

    public virtual ICollection<Do> DoFooter3TaxCodeNavigations { get; set; } = new List<Do>();

    public virtual ICollection<Dodtl> Dodtls { get; set; } = new List<Dodtl>();

    public virtual ICollection<Dr> DrFooter1TaxCodeNavigations { get; set; } = new List<Dr>();

    public virtual ICollection<Dr> DrFooter2TaxCodeNavigations { get; set; } = new List<Dr>();

    public virtual ICollection<Dr> DrFooter3TaxCodeNavigations { get; set; } = new List<Dr>();

    public virtual ICollection<Drdtl> Drdtls { get; set; } = new List<Drdtl>();

    public virtual ICollection<Drprocessing> Drprocessings { get; set; } = new List<Drprocessing>();

    public virtual ICollection<Footer> Footers { get; set; } = new List<Footer>();

    public virtual ICollection<Gldtl> Gldtls { get; set; } = new List<Gldtl>();

    public virtual ICollection<Glmast> GlmastInputTaxCodeNavigations { get; set; } = new List<Glmast>();

    public virtual ICollection<Glmast> GlmastOutputTaxCodeNavigations { get; set; } = new List<Glmast>();

    public virtual ICollection<Gr> GrFooter1TaxCodeNavigations { get; set; } = new List<Gr>();

    public virtual ICollection<Gr> GrFooter2TaxCodeNavigations { get; set; } = new List<Gr>();

    public virtual ICollection<Gr> GrFooter3TaxCodeNavigations { get; set; } = new List<Gr>();

    public virtual ICollection<Grdtl> Grdtls { get; set; } = new List<Grdtl>();

    public virtual ICollection<Gt> GtFooter1TaxCodeNavigations { get; set; } = new List<Gt>();

    public virtual ICollection<Gt> GtFooter2TaxCodeNavigations { get; set; } = new List<Gt>();

    public virtual ICollection<Gt> GtFooter3TaxCodeNavigations { get; set; } = new List<Gt>();

    public virtual ICollection<Gtdtl> Gtdtls { get; set; } = new List<Gtdtl>();

    public virtual ICollection<Item> ItemPurchaseTaxCodeNavigations { get; set; } = new List<Item>();

    public virtual ICollection<Item> ItemTaxCodeNavigations { get; set; } = new List<Item>();

    public virtual ICollection<Iv> IvFooter1TaxCodeNavigations { get; set; } = new List<Iv>();

    public virtual ICollection<Iv> IvFooter2TaxCodeNavigations { get; set; } = new List<Iv>();

    public virtual ICollection<Iv> IvFooter3TaxCodeNavigations { get; set; } = new List<Iv>();

    public virtual ICollection<Ivdtl> Ivdtls { get; set; } = new List<Ivdtl>();

    public virtual ICollection<Jedtl> Jedtls { get; set; } = new List<Jedtl>();

    public virtual ICollection<PackageDtl> PackageDtlPurchaseTaxCodeNavigations { get; set; } = new List<PackageDtl>();

    public virtual ICollection<PackageDtl> PackageDtlTaxCodeNavigations { get; set; } = new List<PackageDtl>();

    public virtual ICollection<PaymentMethods> PaymentMethods { get; set; } = new List<PaymentMethods>();

    public virtual ICollection<Pi> PiFooter1TaxCodeNavigations { get; set; } = new List<Pi>();

    public virtual ICollection<Pi> PiFooter2TaxCodeNavigations { get; set; } = new List<Pi>();

    public virtual ICollection<Pi> PiFooter3TaxCodeNavigations { get; set; } = new List<Pi>();

    public virtual ICollection<Pidtl> Pidtls { get; set; } = new List<Pidtl>();

    public virtual ICollection<Po> PoFooter1TaxCodeNavigations { get; set; } = new List<Po>();

    public virtual ICollection<Po> PoFooter2TaxCodeNavigations { get; set; } = new List<Po>();

    public virtual ICollection<Po> PoFooter3TaxCodeNavigations { get; set; } = new List<Po>();

    public virtual ICollection<Podtl> Podtls { get; set; } = new List<Podtl>();

    public virtual ICollection<Pr> PrFooter1TaxCodeNavigations { get; set; } = new List<Pr>();

    public virtual ICollection<Pr> PrFooter2TaxCodeNavigations { get; set; } = new List<Pr>();

    public virtual ICollection<Pr> PrFooter3TaxCodeNavigations { get; set; } = new List<Pr>();

    public virtual ICollection<Prdtl> Prdtls { get; set; } = new List<Prdtl>();

    public virtual ICollection<PurchaseConsignmentDtl> PurchaseConsignmentDtls { get; set; } = new List<PurchaseConsignmentDtl>();

    public virtual ICollection<PurchaseConsignmentReturnDtl> PurchaseConsignmentReturnDtls { get; set; } = new List<PurchaseConsignmentReturnDtl>();

    public virtual ICollection<Qt> QtFooter1TaxCodeNavigations { get; set; } = new List<Qt>();

    public virtual ICollection<Qt> QtFooter2TaxCodeNavigations { get; set; } = new List<Qt>();

    public virtual ICollection<Qt> QtFooter3TaxCodeNavigations { get; set; } = new List<Qt>();

    public virtual ICollection<Qtdtl> Qtdtls { get; set; } = new List<Qtdtl>();

    public virtual ICollection<Rq> RqFooter1TaxCodeNavigations { get; set; } = new List<Rq>();

    public virtual ICollection<Rq> RqFooter2TaxCodeNavigations { get; set; } = new List<Rq>();

    public virtual ICollection<Rq> RqFooter3TaxCodeNavigations { get; set; } = new List<Rq>();

    public virtual ICollection<Rqdtl> Rqdtls { get; set; } = new List<Rqdtl>();

    public virtual ICollection<So> SoFooter1TaxCodeNavigations { get; set; } = new List<So>();

    public virtual ICollection<So> SoFooter2TaxCodeNavigations { get; set; } = new List<So>();

    public virtual ICollection<So> SoFooter3TaxCodeNavigations { get; set; } = new List<So>();

    public virtual ICollection<Sodtl> Sodtls { get; set; } = new List<Sodtl>();

    public virtual Glmast? TaxAccNoNavigation { get; set; }

    public virtual ICollection<TaxExemption> TaxExemptions { get; set; } = new List<TaxExemption>();

    public virtual ICollection<TaxTran> TaxTrans { get; set; } = new List<TaxTran>();

    public virtual ICollection<TaxTransAuditDtl> TaxTransAuditDtls { get; set; } = new List<TaxTransAuditDtl>();

    public virtual ICollection<TaxTransCancelled> TaxTransCancelleds { get; set; } = new List<TaxTransCancelled>();

    public virtual ICollection<Xs> XFooter1TaxCodeNavigations { get; set; } = new List<Xs>();

    public virtual ICollection<Xs> XFooter2TaxCodeNavigations { get; set; } = new List<Xs>();

    public virtual ICollection<Xs> XFooter3TaxCodeNavigations { get; set; } = new List<Xs>();

    public virtual ICollection<Xp> XpFooter1TaxCodeNavigations { get; set; } = new List<Xp>();

    public virtual ICollection<Xp> XpFooter2TaxCodeNavigations { get; set; } = new List<Xp>();

    public virtual ICollection<Xp> XpFooter3TaxCodeNavigations { get; set; } = new List<Xp>();

    public virtual ICollection<Xpdtl> Xpdtls { get; set; } = new List<Xpdtl>();

    public virtual ICollection<Xsdtl> Xsdtls { get; set; } = new List<Xsdtl>();
}
