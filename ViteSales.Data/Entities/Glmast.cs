namespace ViteSales.Data.Entities;

public partial class Glmast
{
    public string AccNo { get; set; } = null!;

    public string? ParentAccNo { get; set; }

    public string? Description { get; set; }

    public string? Desc2 { get; set; }

    public string AccType { get; set; } = null!;

    public string? SpecialAccType { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public string? CashFlowCategory { get; set; }

    public string? Msiccode { get; set; }

    public string? InputTaxCode { get; set; }

    public string? OutputTaxCode { get; set; }

    public string? TariffCode { get; set; }

    public Guid Guid { get; set; }

    public string? SgeFilingDataId { get; set; }

    public virtual AccType AccTypeNavigation { get; set; } = null!;

    public virtual ICollection<Advqt> Advqts { get; set; } = new List<Advqt>();

    public virtual ICollection<Apcndtl> Apcndtls { get; set; } = new List<Apcndtl>();

    public virtual ICollection<Apcn> Apcns { get; set; } = new List<Apcn>();

    public virtual ICollection<ApdepositForfeit> ApdepositForfeits { get; set; } = new List<ApdepositForfeit>();

    public virtual ICollection<Apdeposit> Apdeposits { get; set; } = new List<Apdeposit>();

    public virtual ICollection<Apdndtl> Apdndtls { get; set; } = new List<Apdndtl>();

    public virtual ICollection<Apdn> Apdns { get; set; } = new List<Apdn>();

    public virtual ICollection<ApinvoiceDtl> ApinvoiceDtls { get; set; } = new List<ApinvoiceDtl>();

    public virtual ICollection<Apinvoice> Apinvoices { get; set; } = new List<Apinvoice>();

    public virtual ICollection<Appayment> Appayments { get; set; } = new List<Appayment>();

    public virtual ICollection<Aprefund> Aprefunds { get; set; } = new List<Aprefund>();

    public virtual ICollection<Arapcontra> ArapcontraCreditorCodeNavigations { get; set; } = new List<Arapcontra>();

    public virtual ICollection<Arapcontra> ArapcontraDebtorCodeNavigations { get; set; } = new List<Arapcontra>();

    public virtual ICollection<Arapcontra> ArapcontraTempAccNoNavigations { get; set; } = new List<Arapcontra>();

    public virtual ICollection<Arcndtl> Arcndtls { get; set; } = new List<Arcndtl>();

    public virtual ICollection<Arcn> Arcns { get; set; } = new List<Arcn>();

    public virtual ICollection<ArdepositForfeit> ArdepositForfeits { get; set; } = new List<ArdepositForfeit>();

    public virtual ICollection<Ardeposit> Ardeposits { get; set; } = new List<Ardeposit>();

    public virtual ICollection<Ardndtl> Ardndtls { get; set; } = new List<Ardndtl>();

    public virtual ICollection<Ardn> Ardns { get; set; } = new List<Ardn>();

    public virtual ICollection<ArinvoiceDtl> ArinvoiceDtls { get; set; } = new List<ArinvoiceDtl>();

    public virtual ICollection<Arinvoice> Arinvoices { get; set; } = new List<Arinvoice>();

    public virtual ICollection<Arpayment> Arpayments { get; set; } = new List<Arpayment>();

    public virtual ICollection<Arrefund> Arrefunds { get; set; } = new List<Arrefund>();

    public virtual ICollection<AssetDisposal> AssetDisposals { get; set; } = new List<AssetDisposal>();

    public virtual ICollection<AssetLink> AssetLinkAssetAccNoNavigations { get; set; } = new List<AssetLink>();

    public virtual ICollection<AssetLink> AssetLinkAssetDeprnAccNoNavigations { get; set; } = new List<AssetLink>();

    public virtual ICollection<BankRecon> BankRecons { get; set; } = new List<BankRecon>();

    public virtual ICollection<BankTran> BankTrans { get; set; } = new List<BankTran>();

    public virtual ICollection<BonusPointRedemption> BonusPointRedemptions { get; set; } = new List<BonusPointRedemption>();

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    public virtual ICollection<BudgetPbalance> BudgetPbalances { get; set; } = new List<BudgetPbalance>();

    public virtual ICollection<Cbdtl> Cbdtls { get; set; } = new List<Cbdtl>();

    public virtual ICollection<Cndtl> Cndtls { get; set; } = new List<Cndtl>();

    public virtual ICollection<Cn> Cns { get; set; } = new List<Cn>();

    public virtual ICollection<ConsignmentReturn> ConsignmentReturns { get; set; } = new List<ConsignmentReturn>();

    public virtual ICollection<Consignment> Consignments { get; set; } = new List<Consignment>();

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    public virtual ICollection<Cpdtl> Cpdtls { get; set; } = new List<Cpdtl>();

    public virtual ICollection<Cp> Cps { get; set; } = new List<Cp>();

    public virtual ICollection<CreditControlSync> CreditControlSyncs { get; set; } = new List<CreditControlSync>();

    public virtual Creditor? Creditor { get; set; }

    public virtual ICollection<CashSales> Cs { get; set; } = new List<CashSales>();

    public virtual ICollection<CashSalesdtl> Csdtls { get; set; } = new List<CashSalesdtl>();

    public virtual ICollection<Csgnxfer> CsgnxferFromDebtorCodeNavigations { get; set; } = new List<Csgnxfer>();

    public virtual ICollection<Csgnxfer> CsgnxferToDebtorCodeNavigations { get; set; } = new List<Csgnxfer>();

    public virtual Currency CurrencyCodeNavigation { get; set; } = null!;

    public virtual ICollection<Currency> CurrencyFcgainAccountNavigations { get; set; } = new List<Currency>();

    public virtual ICollection<Currency> CurrencyFclossAccountNavigations { get; set; } = new List<Currency>();

    public virtual Debtor? Debtor { get; set; }

    public virtual ICollection<Dndtl> Dndtls { get; set; } = new List<Dndtl>();

    public virtual ICollection<Dn> Dns { get; set; } = new List<Dn>();

    public virtual ICollection<DocNoFormatAccNo> DocNoFormatAccNos { get; set; } = new List<DocNoFormatAccNo>();

    public virtual ICollection<Dodtl> Dodtls { get; set; } = new List<Dodtl>();

    public virtual ICollection<Do> Dos { get; set; } = new List<Do>();

    public virtual ICollection<Drprocessing> Drprocessings { get; set; } = new List<Drprocessing>();

    public virtual ICollection<Dr> Drs { get; set; } = new List<Dr>();

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual ICollection<FcrevalueDocument> FcrevalueDocuments { get; set; } = new List<FcrevalueDocument>();

    public virtual ICollection<FcrevalueGlaccount> FcrevalueGlaccounts { get; set; } = new List<FcrevalueGlaccount>();

    public virtual ICollection<Fcrevalue> FcrevalueUnrealizedGainAccountNavigations { get; set; } = new List<Fcrevalue>();

    public virtual ICollection<Fcrevalue> FcrevalueUnrealizedLossAccountNavigations { get; set; } = new List<Fcrevalue>();

    public virtual ICollection<Footer> Footers { get; set; } = new List<Footer>();

    public virtual ICollection<Gldtl> GldtlAccNoNavigations { get; set; } = new List<Gldtl>();

    public virtual ICollection<Gldtl> GldtlDeaccNoNavigations { get; set; } = new List<Gldtl>();

    public virtual ICollection<Gr> Grs { get; set; } = new List<Gr>();

    public virtual ICollection<Gt> Gts { get; set; } = new List<Gt>();

    public virtual TaxCode? InputTaxCodeNavigation { get; set; }

    public virtual ICollection<Iphist> Iphists { get; set; } = new List<Iphist>();

    public virtual ICollection<ItemGroup> ItemGroupBalanceStockCodeNavigations { get; set; } = new List<ItemGroup>();

    public virtual ICollection<ItemGroup> ItemGroupCashPurchaseCodeNavigations { get; set; } = new List<ItemGroup>();

    public virtual ICollection<ItemGroup> ItemGroupCashSalesCodeNavigations { get; set; } = new List<ItemGroup>();

    public virtual ICollection<ItemGroup> ItemGroupPurchaseCodeNavigations { get; set; } = new List<ItemGroup>();

    public virtual ICollection<ItemGroup> ItemGroupPurchaseDiscountCodeNavigations { get; set; } = new List<ItemGroup>();

    public virtual ICollection<ItemGroup> ItemGroupPurchaseReturnCodeNavigations { get; set; } = new List<ItemGroup>();

    public virtual ICollection<ItemGroup> ItemGroupSalesCodeNavigations { get; set; } = new List<ItemGroup>();

    public virtual ICollection<ItemGroup> ItemGroupSalesDiscountCodeNavigations { get; set; } = new List<ItemGroup>();

    public virtual ICollection<ItemGroup> ItemGroupSalesReturnCodeNavigations { get; set; } = new List<ItemGroup>();

    public virtual ICollection<ItemPrice> ItemPrices { get; set; } = new List<ItemPrice>();

    public virtual ICollection<Ivdtl> Ivdtls { get; set; } = new List<Ivdtl>();

    public virtual ICollection<Iv> Ivs { get; set; } = new List<Iv>();

    public virtual ICollection<Jedtl> Jedtls { get; set; } = new List<Jedtl>();

    public virtual ICollection<Location> LocationCreditCardChargesAccNoNavigations { get; set; } = new List<Location>();

    public virtual ICollection<Location> LocationDepositAccNoNavigations { get; set; } = new List<Location>();

    public virtual ICollection<Location> LocationForfeitedAccNoNavigations { get; set; } = new List<Location>();

    public virtual ICollection<Location> LocationPointPaymentAccNoNavigations { get; set; } = new List<Location>();

    public virtual ICollection<Location> LocationRoundingAccNoNavigations { get; set; } = new List<Location>();

    public virtual ICollection<Location> LocationServiceChargeAccNoNavigations { get; set; } = new List<Location>();

    public virtual ICollection<Location> LocationTipAccNoNavigations { get; set; } = new List<Location>();

    public virtual ICollection<Location> LocationVoucherForfeitedAccNoNavigations { get; set; } = new List<Location>();

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    public virtual ICollection<Obalance> Obalances { get; set; } = new List<Obalance>();

    public virtual ICollection<Obdtl> Obdtls { get; set; } = new List<Obdtl>();

    public virtual TaxCode? OutputTaxCodeNavigation { get; set; }

    public virtual ICollection<PaymentMethods> PaymentMethodBankAccountNavigations { get; set; } = new List<PaymentMethods>();

    public virtual ICollection<PaymentMethods> PaymentMethodBankChargeAccountNavigations { get; set; } = new List<PaymentMethods>();

    public virtual ICollection<Pbalance> Pbalances { get; set; } = new List<Pbalance>();

    public virtual ICollection<Pidtl> Pidtls { get; set; } = new List<Pidtl>();

    public virtual ICollection<Pi> Pis { get; set; } = new List<Pi>();

    public virtual ICollection<Po> Pos { get; set; } = new List<Po>();

    public virtual ICollection<PostingAccountGroup> PostingAccountGroupCashPurchaseCodeNavigations { get; set; } = new List<PostingAccountGroup>();

    public virtual ICollection<PostingAccountGroup> PostingAccountGroupCashSalesCodeNavigations { get; set; } = new List<PostingAccountGroup>();

    public virtual ICollection<PostingAccountGroup> PostingAccountGroupPurchaseCodeNavigations { get; set; } = new List<PostingAccountGroup>();

    public virtual ICollection<PostingAccountGroup> PostingAccountGroupPurchaseDiscountCodeNavigations { get; set; } = new List<PostingAccountGroup>();

    public virtual ICollection<PostingAccountGroup> PostingAccountGroupPurchaseReturnCodeNavigations { get; set; } = new List<PostingAccountGroup>();

    public virtual ICollection<PostingAccountGroup> PostingAccountGroupSalesCodeNavigations { get; set; } = new List<PostingAccountGroup>();

    public virtual ICollection<PostingAccountGroup> PostingAccountGroupSalesDiscountCodeNavigations { get; set; } = new List<PostingAccountGroup>();

    public virtual ICollection<PostingAccountGroup> PostingAccountGroupSalesReturnCodeNavigations { get; set; } = new List<PostingAccountGroup>();

    public virtual ICollection<Pqdtl> Pqdtls { get; set; } = new List<Pqdtl>();

    public virtual ICollection<Prdtl> Prdtls { get; set; } = new List<Prdtl>();

    public virtual ICollection<PrprocessingPo> PrprocessingPos { get; set; } = new List<PrprocessingPo>();

    public virtual ICollection<Prprocessing> Prprocessings { get; set; } = new List<Prprocessing>();

    public virtual ICollection<Pr> Prs { get; set; } = new List<Pr>();

    public virtual ICollection<PurchaseConsignmentReturn> PurchaseConsignmentReturns { get; set; } = new List<PurchaseConsignmentReturn>();

    public virtual ICollection<PurchaseConsignment> PurchaseConsignments { get; set; } = new List<PurchaseConsignment>();

    public virtual ICollection<Qt> Qts { get; set; } = new List<Qt>();

    public virtual ICollection<RecurrenceDtl> RecurrenceDtls { get; set; } = new List<RecurrenceDtl>();

    public virtual ICollection<Rq> Rqs { get; set; } = new List<Rq>();

    public virtual ICollection<So> Sos { get; set; } = new List<So>();

    public virtual ICollection<StockSet> StockSetBalanceStockNavigations { get; set; } = new List<StockSet>();

    public virtual ICollection<StockSet> StockSetCloseStockNavigations { get; set; } = new List<StockSet>();

    public virtual ICollection<StockSet> StockSetOpenStockNavigations { get; set; } = new List<StockSet>();

    public virtual Tariff? TariffCodeNavigation { get; set; }

    public virtual ICollection<TaxCode> TaxCodes { get; set; } = new List<TaxCode>();

    public virtual ICollection<TaxTran> TaxTranTaxAccNoNavigations { get; set; } = new List<TaxTran>();

    public virtual ICollection<TaxTran> TaxTranTaxableAccNoNavigations { get; set; } = new List<TaxTran>();

    public virtual ICollection<TaxTransAuditDtl> TaxTransAuditDtlTaxAccNoNavigations { get; set; } = new List<TaxTransAuditDtl>();

    public virtual ICollection<TaxTransAuditDtl> TaxTransAuditDtlTaxableAccNoNavigations { get; set; } = new List<TaxTransAuditDtl>();

    public virtual ICollection<TaxTransCancelled> TaxTransCancelledTaxAccNoNavigations { get; set; } = new List<TaxTransCancelled>();

    public virtual ICollection<TaxTransCancelled> TaxTransCancelledTaxableAccNoNavigations { get; set; } = new List<TaxTransCancelled>();

    public virtual ICollection<TemporaryCredit> TemporaryCredits { get; set; } = new List<TemporaryCredit>();

    public virtual ICollection<UnapprovedDocument> UnapprovedDocuments { get; set; } = new List<UnapprovedDocument>();

    public virtual ICollection<UnrealizedGainLossDocument> UnrealizedGainLossDocuments { get; set; } = new List<UnrealizedGainLossDocument>();

    public virtual ICollection<UnrealizedGainLossGlaccount> UnrealizedGainLossGlaccounts { get; set; } = new List<UnrealizedGainLossGlaccount>();

    public virtual ICollection<UnrealizedGainLoss> UnrealizedGainLossGlgainAccountNavigations { get; set; } = new List<UnrealizedGainLoss>();

    public virtual ICollection<UnrealizedGainLoss> UnrealizedGainLossGllossAccountNavigations { get; set; } = new List<UnrealizedGainLoss>();

    public virtual ICollection<UnrealizedGainLossRate> UnrealizedGainLossRateGlgainAccountNavigations { get; set; } = new List<UnrealizedGainLossRate>();

    public virtual ICollection<UnrealizedGainLossRate> UnrealizedGainLossRateGllossAccountNavigations { get; set; } = new List<UnrealizedGainLossRate>();

    public virtual ICollection<UnrealizedGainLoss> UnrealizedGainLossUnrealizedGainAccountNavigations { get; set; } = new List<UnrealizedGainLoss>();

    public virtual ICollection<UnrealizedGainLoss> UnrealizedGainLossUnrealizedLossAccountNavigations { get; set; } = new List<UnrealizedGainLoss>();

    public virtual ICollection<WithholdingTaxTran> WithholdingTaxTrans { get; set; } = new List<WithholdingTaxTran>();

    public virtual ICollection<WithholdingTax> WithholdingTaxWhtcreditableAccNoNavigations { get; set; } = new List<WithholdingTax>();

    public virtual ICollection<WithholdingTax> WithholdingTaxWhtpayableAccNoNavigations { get; set; } = new List<WithholdingTax>();

    public virtual ICollection<Xs> Xes { get; set; } = new List<Xs>();

    public virtual ICollection<Xp> Xps { get; set; } = new List<Xp>();
}
