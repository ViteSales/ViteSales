namespace ViteSales.Data.Entities;

public partial class Project
{
    public string ProjNo { get; set; } = null!;

    public string? ParentProjNo { get; set; }

    public string? Description { get; set; }

    public string? Desc2 { get; set; }

    public string IsActive { get; set; } = null!;

    public int LastUpdate { get; set; }

    public Guid Guid { get; set; }

    public virtual ICollection<Adjdtl> Adjdtls { get; set; } = new List<Adjdtl>();

    public virtual ICollection<Advqtdtl> Advqtdtls { get; set; } = new List<Advqtdtl>();

    public virtual ICollection<Aorprocessing> Aorprocessings { get; set; } = new List<Aorprocessing>();

    public virtual ICollection<Apcndtl> Apcndtls { get; set; } = new List<Apcndtl>();

    public virtual ICollection<ApcnknockOff> ApcnknockOffs { get; set; } = new List<ApcnknockOff>();

    public virtual ICollection<ApcontraKnockOff> ApcontraKnockOffs { get; set; } = new List<ApcontraKnockOff>();

    public virtual ICollection<ApdepositPaymentDtl> ApdepositPaymentDtls { get; set; } = new List<ApdepositPaymentDtl>();

    public virtual ICollection<ApdepositRefundPaymentDtl> ApdepositRefundPaymentDtls { get; set; } = new List<ApdepositRefundPaymentDtl>();

    public virtual ICollection<Apdeposit> Apdeposits { get; set; } = new List<Apdeposit>();

    public virtual ICollection<Apdndtl> Apdndtls { get; set; } = new List<Apdndtl>();

    public virtual ICollection<ApinvoiceDtl> ApinvoiceDtls { get; set; } = new List<ApinvoiceDtl>();

    public virtual ICollection<AppaymentDtl> AppaymentDtls { get; set; } = new List<AppaymentDtl>();

    public virtual ICollection<AppaymentKnockOff> AppaymentKnockOffs { get; set; } = new List<AppaymentKnockOff>();

    public virtual ICollection<Appayment> Appayments { get; set; } = new List<Appayment>();

    public virtual ICollection<AprefundDtl> AprefundDtls { get; set; } = new List<AprefundDtl>();

    public virtual ICollection<AprefundKnockOff> AprefundKnockOffs { get; set; } = new List<AprefundKnockOff>();

    public virtual ICollection<Aprefund> Aprefunds { get; set; } = new List<Aprefund>();

    public virtual ICollection<Arapcontra> Arapcontras { get; set; } = new List<Arapcontra>();

    public virtual ICollection<Arcndtl> Arcndtls { get; set; } = new List<Arcndtl>();

    public virtual ICollection<ArcnknockOff> ArcnknockOffs { get; set; } = new List<ArcnknockOff>();

    public virtual ICollection<ArcontraKnockOff> ArcontraKnockOffs { get; set; } = new List<ArcontraKnockOff>();

    public virtual ICollection<ArdepositPaymentDtl> ArdepositPaymentDtls { get; set; } = new List<ArdepositPaymentDtl>();

    public virtual ICollection<ArdepositRefundPaymentDtl> ArdepositRefundPaymentDtls { get; set; } = new List<ArdepositRefundPaymentDtl>();

    public virtual ICollection<Ardeposit> Ardeposits { get; set; } = new List<Ardeposit>();

    public virtual ICollection<Ardndtl> Ardndtls { get; set; } = new List<Ardndtl>();

    public virtual ICollection<ArinvoiceDtl> ArinvoiceDtls { get; set; } = new List<ArinvoiceDtl>();

    public virtual ICollection<ArpaymentDtl> ArpaymentDtls { get; set; } = new List<ArpaymentDtl>();

    public virtual ICollection<ArpaymentKnockOff> ArpaymentKnockOffs { get; set; } = new List<ArpaymentKnockOff>();

    public virtual ICollection<Arpayment> Arpayments { get; set; } = new List<Arpayment>();

    public virtual ICollection<ArrefundDtl> ArrefundDtls { get; set; } = new List<ArrefundDtl>();

    public virtual ICollection<ArrefundKnockOff> ArrefundKnockOffs { get; set; } = new List<ArrefundKnockOff>();

    public virtual ICollection<Arrefund> Arrefunds { get; set; } = new List<Arrefund>();

    public virtual ICollection<AsmRprocessing> AsmRprocessings { get; set; } = new List<AsmRprocessing>();

    public virtual ICollection<Asmdtl> Asmdtls { get; set; } = new List<Asmdtl>();

    public virtual ICollection<AsmorderDtl> AsmorderDtls { get; set; } = new List<AsmorderDtl>();

    public virtual ICollection<Asmorder> Asmorders { get; set; } = new List<Asmorder>();

    public virtual ICollection<Asm> Asms { get; set; } = new List<Asm>();

    public virtual ICollection<BonusPointRedemptionDtl> BonusPointRedemptionDtls { get; set; } = new List<BonusPointRedemptionDtl>();

    public virtual ICollection<BudgetPbalance> BudgetPbalances { get; set; } = new List<BudgetPbalance>();

    public virtual ICollection<Cbdtl> Cbdtls { get; set; } = new List<Cbdtl>();

    public virtual ICollection<Cndtl> Cndtls { get; set; } = new List<Cndtl>();

    public virtual ICollection<ConsignmentDtl> ConsignmentDtls { get; set; } = new List<ConsignmentDtl>();

    public virtual ICollection<ConsignmentReturnDtl> ConsignmentReturnDtls { get; set; } = new List<ConsignmentReturnDtl>();

    public virtual ICollection<Cpdtl> Cpdtls { get; set; } = new List<Cpdtl>();

    public virtual ICollection<CashSales> Cs { get; set; } = new List<CashSales>();

    public virtual ICollection<CashSalesdtl> Csdtls { get; set; } = new List<CashSalesdtl>();

    public virtual ICollection<CsgnitemBalQty> CsgnitemBalQties { get; set; } = new List<CsgnitemBalQty>();

    public virtual ICollection<Csgnxferdtl> Csgnxferdtls { get; set; } = new List<Csgnxferdtl>();

    public virtual ICollection<Dndtl> Dndtls { get; set; } = new List<Dndtl>();

    public virtual ICollection<Dn> Dns { get; set; } = new List<Dn>();

    public virtual ICollection<Dodtl> Dodtls { get; set; } = new List<Dodtl>();

    public virtual ICollection<Drdtl> Drdtls { get; set; } = new List<Drdtl>();

    public virtual ICollection<Drprocessing> Drprocessings { get; set; } = new List<Drprocessing>();

    public virtual ICollection<FcrevalueDocument> FcrevalueDocuments { get; set; } = new List<FcrevalueDocument>();

    public virtual ICollection<Gldtl> Gldtls { get; set; } = new List<Gldtl>();

    public virtual ICollection<Grdtl> Grdtls { get; set; } = new List<Grdtl>();

    public virtual ICollection<Gtdtl> Gtdtls { get; set; } = new List<Gtdtl>();

    public virtual ICollection<Project> InverseParentProjNoNavigation { get; set; } = new List<Project>();

    public virtual ICollection<Iphist> Iphists { get; set; } = new List<Iphist>();

    public virtual ICollection<Issdtl> Issdtls { get; set; } = new List<Issdtl>();

    public virtual ICollection<Iss> Isses { get; set; } = new List<Iss>();

    public virtual ICollection<ItemOpening> ItemOpenings { get; set; } = new List<ItemOpening>();

    public virtual ICollection<Ivdtl> Ivdtls { get; set; } = new List<Ivdtl>();

    public virtual ICollection<Iv> Ivs { get; set; } = new List<Iv>();

    public virtual ICollection<Jedtl> Jedtls { get; set; } = new List<Jedtl>();

    public virtual ICollection<Obalance> Obalances { get; set; } = new List<Obalance>();

    public virtual ICollection<Obdtl> Obdtls { get; set; } = new List<Obdtl>();

    public virtual Project? ParentProjNoNavigation { get; set; }

    public virtual ICollection<Pbalance> Pbalances { get; set; } = new List<Pbalance>();

    public virtual ICollection<Pidtl> Pidtls { get; set; } = new List<Pidtl>();

    public virtual ICollection<Podtl> Podtls { get; set; } = new List<Podtl>();

    public virtual ICollection<Pqdtl> Pqdtls { get; set; } = new List<Pqdtl>();

    public virtual ICollection<Prdtl> Prdtls { get; set; } = new List<Prdtl>();

    public virtual ICollection<PriceBookRule> PriceBookRuleFromProjNoNavigations { get; set; } = new List<PriceBookRule>();

    public virtual ICollection<PriceBookRule> PriceBookRuleToProjNoNavigations { get; set; } = new List<PriceBookRule>();

    public virtual ICollection<Prprocessing> Prprocessings { get; set; } = new List<Prprocessing>();

    public virtual ICollection<PurchaseConsignmentDtl> PurchaseConsignmentDtls { get; set; } = new List<PurchaseConsignmentDtl>();

    public virtual ICollection<PurchaseConsignmentReturnDtl> PurchaseConsignmentReturnDtls { get; set; } = new List<PurchaseConsignmentReturnDtl>();

    public virtual ICollection<Qtdtl> Qtdtls { get; set; } = new List<Qtdtl>();

    public virtual ICollection<Rcvdtl> Rcvdtls { get; set; } = new List<Rcvdtl>();

    public virtual ICollection<Rqdtl> Rqdtls { get; set; } = new List<Rqdtl>();

    public virtual ICollection<Sodtl> Sodtls { get; set; } = new List<Sodtl>();

    public virtual ICollection<StockDisassembly> StockDisassemblies { get; set; } = new List<StockDisassembly>();

    public virtual ICollection<StockDisassemblyDtl> StockDisassemblyDtls { get; set; } = new List<StockDisassemblyDtl>();

    public virtual ICollection<StockDtl> StockDtls { get; set; } = new List<StockDtl>();

    public virtual ICollection<StockPbalance> StockPbalances { get; set; } = new List<StockPbalance>();

    public virtual ICollection<TaxTrans> TaxTrans { get; set; } = new List<TaxTrans>();

    public virtual ICollection<TaxTransAuditDtl> TaxTransAuditDtls { get; set; } = new List<TaxTransAuditDtl>();

    public virtual ICollection<TaxTransCancelled> TaxTransCancelleds { get; set; } = new List<TaxTransCancelled>();

    public virtual ICollection<UnrealizedGainLossDocument> UnrealizedGainLossDocuments { get; set; } = new List<UnrealizedGainLossDocument>();

    public virtual ICollection<UomconvDtl> UomconvDtls { get; set; } = new List<UomconvDtl>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<WithholdingTaxEntryDtl> WithholdingTaxEntryDtls { get; set; } = new List<WithholdingTaxEntryDtl>();

    public virtual ICollection<WithholdingTaxPaymentDtl> WithholdingTaxPaymentDtls { get; set; } = new List<WithholdingTaxPaymentDtl>();

    public virtual ICollection<Woffdtl> Woffdtls { get; set; } = new List<Woffdtl>();

    public virtual ICollection<Xferdtl> Xferdtls { get; set; } = new List<Xferdtl>();

    public virtual ICollection<Xpdtl> Xpdtls { get; set; } = new List<Xpdtl>();

    public virtual ICollection<Xsdtl> Xsdtls { get; set; } = new List<Xsdtl>();
}
