namespace ViteSales.Data.Entities;

public partial class User
{
    public long AutoKey { get; set; }

    public string UserId { get; set; } = null!;

    public string? UserName { get; set; }

    public string? Department { get; set; }

    public string? Passwd { get; set; }

    public byte[]? Signature { get; set; }

    public string? EmailAddress { get; set; }

    public string FilterBySalesAgent { get; set; } = null!;

    public string FilterByPurchaseAgent { get; set; } = null!;

    public string IsActive { get; set; } = null!;

    public int LastUpdate { get; set; }

    public string? MainPage { get; set; }

    public int PasswordAge { get; set; }

    public DateTime? LastPasswordDate { get; set; }

    public string? Location { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public string IsExportDebtorToBranch { get; set; } = null!;

    public string IsExportCreditorToBranch { get; set; } = null!;

    public string IsExportItemToBranch { get; set; } = null!;

    public string IsExportPriceHistoryToBranch { get; set; } = null!;

    public string IsExportQttoBranch { get; set; } = null!;

    public string IsExportSotoBranch { get; set; } = null!;

    public string IsExportDotoBranch { get; set; } = null!;

    public string IsExportIvtoBranch { get; set; } = null!;

    public string IsExportCstoBranch { get; set; } = null!;

    public string? PosmainPage { get; set; }

    public decimal? SalesCreditLimitIncrementPercentage { get; set; }

    public decimal? SalesOverdueLimitIncrementPercentage { get; set; }

    public decimal? PurchaseCreditLimitIncrementPercentage { get; set; }

    public decimal? PurchaseOverdueLimitIncrementPercentage { get; set; }

    public string? FilteredByCreatedUserId { get; set; }

    public string? FilteredByLastModifiedUserId { get; set; }

    public string? PasswordStrength { get; set; }

    public string UserType { get; set; } = null!;

    public string FilterByAccNo { get; set; } = null!;

    public byte[]? StartScreenData { get; set; }

    public string? StoreEmail { get; set; }

    public string? StorePassword { get; set; }

    public byte[]? PosStartScreenData { get; set; }

    public bool? ShowAgingItemInquiry { get; set; }

    public virtual ICollection<Accountant> Accountants { get; set; } = new List<Accountant>();

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public virtual ICollection<Adj> AdjCreatedUsers { get; set; } = new List<Adj>();

    public virtual ICollection<Adj> AdjLastModifiedUsers { get; set; } = new List<Adj>();

    public virtual ICollection<Advqt> AdvqtApprovalUsers { get; set; } = new List<Advqt>();

    public virtual ICollection<Advqt> AdvqtConfirmUsers { get; set; } = new List<Advqt>();

    public virtual ICollection<Advqt> AdvqtCreatedUsers { get; set; } = new List<Advqt>();

    public virtual ICollection<Advqt> AdvqtLastModifiedUsers { get; set; } = new List<Advqt>();

    public virtual ICollection<Advqt> AdvqtSucessLostUsers { get; set; } = new List<Advqt>();

    public virtual ICollection<Aorprocessing> AorprocessingCreatedUsers { get; set; } = new List<Aorprocessing>();

    public virtual ICollection<Aorprocessing> AorprocessingLastModifiedUsers { get; set; } = new List<Aorprocessing>();

    public virtual ICollection<Apcn> ApcnCreatedUsers { get; set; } = new List<Apcn>();

    public virtual ICollection<Apcn> ApcnLastModifiedUsers { get; set; } = new List<Apcn>();

    public virtual ICollection<Apdeposit> ApdepositCreatedUsers { get; set; } = new List<Apdeposit>();

    public virtual ICollection<ApdepositForfeit> ApdepositForfeitCreatedUsers { get; set; } = new List<ApdepositForfeit>();

    public virtual ICollection<ApdepositForfeit> ApdepositForfeitLastModifiedUsers { get; set; } = new List<ApdepositForfeit>();

    public virtual ICollection<Apdeposit> ApdepositLastModifiedUsers { get; set; } = new List<Apdeposit>();

    public virtual ICollection<ApdepositRefund> ApdepositRefundCreatedUsers { get; set; } = new List<ApdepositRefund>();

    public virtual ICollection<ApdepositRefund> ApdepositRefundLastModifiedUsers { get; set; } = new List<ApdepositRefund>();

    public virtual ICollection<Apdn> ApdnCreatedUsers { get; set; } = new List<Apdn>();

    public virtual ICollection<Apdn> ApdnLastModifiedUsers { get; set; } = new List<Apdn>();

    public virtual ICollection<Apinvoice> ApinvoiceCreatedUsers { get; set; } = new List<Apinvoice>();

    public virtual ICollection<Apinvoice> ApinvoiceLastModifiedUsers { get; set; } = new List<Apinvoice>();

    public virtual ICollection<Appayment> AppaymentCreatedUsers { get; set; } = new List<Appayment>();

    public virtual ICollection<Appayment> AppaymentLastModifiedUsers { get; set; } = new List<Appayment>();

    public virtual ICollection<Aprefund> AprefundCreatedUsers { get; set; } = new List<Aprefund>();

    public virtual ICollection<Aprefund> AprefundLastModifiedUsers { get; set; } = new List<Aprefund>();

    public virtual ICollection<Arapcontra> ArapcontraCreatedUsers { get; set; } = new List<Arapcontra>();

    public virtual ICollection<Arapcontra> ArapcontraLastModifiedUsers { get; set; } = new List<Arapcontra>();

    public virtual ICollection<Arcn> ArcnCreatedUsers { get; set; } = new List<Arcn>();

    public virtual ICollection<Arcn> ArcnLastModifiedUsers { get; set; } = new List<Arcn>();

    public virtual ICollection<Ardeposit> ArdepositCreatedUsers { get; set; } = new List<Ardeposit>();

    public virtual ICollection<ArdepositForfeit> ArdepositForfeitCreatedUsers { get; set; } = new List<ArdepositForfeit>();

    public virtual ICollection<ArdepositForfeit> ArdepositForfeitLastModifiedUsers { get; set; } = new List<ArdepositForfeit>();

    public virtual ICollection<Ardeposit> ArdepositLastModifiedUsers { get; set; } = new List<Ardeposit>();

    public virtual ICollection<ArdepositRefund> ArdepositRefundCreatedUsers { get; set; } = new List<ArdepositRefund>();

    public virtual ICollection<ArdepositRefund> ArdepositRefundLastModifiedUsers { get; set; } = new List<ArdepositRefund>();

    public virtual ICollection<Ardn> ArdnCreatedUsers { get; set; } = new List<Ardn>();

    public virtual ICollection<Ardn> ArdnLastModifiedUsers { get; set; } = new List<Ardn>();

    public virtual ICollection<Arinvoice> ArinvoiceCreatedUsers { get; set; } = new List<Arinvoice>();

    public virtual ICollection<Arinvoice> ArinvoiceLastModifiedUsers { get; set; } = new List<Arinvoice>();

    public virtual ICollection<Arpayment> ArpaymentCreatedUsers { get; set; } = new List<Arpayment>();

    public virtual ICollection<Arpayment> ArpaymentLastModifiedUsers { get; set; } = new List<Arpayment>();

    public virtual ICollection<Arrefund> ArrefundCreatedUsers { get; set; } = new List<Arrefund>();

    public virtual ICollection<Arrefund> ArrefundLastModifiedUsers { get; set; } = new List<Arrefund>();

    public virtual ICollection<Asm> AsmCreatedUsers { get; set; } = new List<Asm>();

    public virtual ICollection<Asm> AsmLastModifiedUsers { get; set; } = new List<Asm>();

    public virtual ICollection<AsmRprocessing> AsmRprocessingCreatedUsers { get; set; } = new List<AsmRprocessing>();

    public virtual ICollection<AsmRprocessing> AsmRprocessingLastModifiedUsers { get; set; } = new List<AsmRprocessing>();

    public virtual ICollection<Asmorder> AsmorderCreatedUsers { get; set; } = new List<Asmorder>();

    public virtual ICollection<AsmorderDtl> AsmorderDtlLastAorpmodifiedUsers { get; set; } = new List<AsmorderDtl>();

    public virtual ICollection<AsmorderDtl> AsmorderDtlLastPrmodifiedUsers { get; set; } = new List<AsmorderDtl>();

    public virtual ICollection<Asmorder> AsmorderLastAopmodifiedUsers { get; set; } = new List<Asmorder>();

    public virtual ICollection<Asmorder> AsmorderLastModifiedUsers { get; set; } = new List<Asmorder>();

    public virtual ICollection<BonusPointAdj> BonusPointAdjCreatedUsers { get; set; } = new List<BonusPointAdj>();

    public virtual ICollection<BonusPointAdj> BonusPointAdjLastModifiedUsers { get; set; } = new List<BonusPointAdj>();

    public virtual ICollection<BonusPointRedemption> BonusPointRedemptionCreatedUsers { get; set; } = new List<BonusPointRedemption>();

    public virtual ICollection<BonusPointRedemption> BonusPointRedemptionLastModifiedUsers { get; set; } = new List<BonusPointRedemption>();

    public virtual ICollection<CashSales> CCreatedUsers { get; set; } = new List<CashSales>();

    public virtual ICollection<CashSales> CLastModifiedUsers { get; set; } = new List<CashSales>();

    public virtual ICollection<Cb> CbCreatedUsers { get; set; } = new List<Cb>();

    public virtual ICollection<Cb> CbLastModifiedUsers { get; set; } = new List<Cb>();

    public virtual ICollection<Cn> CnCreatedUsers { get; set; } = new List<Cn>();

    public virtual ICollection<Cn> CnLastModifiedUsers { get; set; } = new List<Cn>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Consignment> ConsignmentCreatedUsers { get; set; } = new List<Consignment>();

    public virtual ICollection<Consignment> ConsignmentLastModifiedUsers { get; set; } = new List<Consignment>();

    public virtual ICollection<ConsignmentReturn> ConsignmentReturnCreatedUsers { get; set; } = new List<ConsignmentReturn>();

    public virtual ICollection<ConsignmentReturn> ConsignmentReturnLastModifiedUsers { get; set; } = new List<ConsignmentReturn>();

    public virtual ICollection<Cp> CpCreatedUsers { get; set; } = new List<Cp>();

    public virtual ICollection<Cp> CpLastModifiedUsers { get; set; } = new List<Cp>();

    public virtual ICollection<CreditControlSync> CreditControlSyncs { get; set; } = new List<CreditControlSync>();

    public virtual ICollection<Creditor> CreditorCreatedUsers { get; set; } = new List<Creditor>();

    public virtual ICollection<Creditor> CreditorLastModifiedUsers { get; set; } = new List<Creditor>();

    public virtual ICollection<Csgnxfer> CsgnxferCreatedUsers { get; set; } = new List<Csgnxfer>();

    public virtual ICollection<Csgnxfer> CsgnxferLastModifiedUsers { get; set; } = new List<Csgnxfer>();

    public virtual ICollection<Debtor> DebtorCreatedUsers { get; set; } = new List<Debtor>();

    public virtual ICollection<Debtor> DebtorLastModifiedUsers { get; set; } = new List<Debtor>();

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual ICollection<Dn> DnCreatedUsers { get; set; } = new List<Dn>();

    public virtual ICollection<Dn> DnLastModifiedUsers { get; set; } = new List<Dn>();

    public virtual ICollection<Do> DoCreatedUsers { get; set; } = new List<Do>();

    public virtual ICollection<Do> DoLastModifiedUsers { get; set; } = new List<Do>();

    public virtual ICollection<DocNoFormatUser> DocNoFormatUsers { get; set; } = new List<DocNoFormatUser>();

    public virtual ICollection<DocTemplate> DocTemplates { get; set; } = new List<DocTemplate>();

    public virtual ICollection<Dr> DrCreatedUsers { get; set; } = new List<Dr>();

    public virtual ICollection<Dr> DrLastModifiedUsers { get; set; } = new List<Dr>();

    public virtual ICollection<Drprocessing> DrprocessingCreatedUsers { get; set; } = new List<Drprocessing>();

    public virtual ICollection<DrprocessingDo> DrprocessingDoCreatedUsers { get; set; } = new List<DrprocessingDo>();

    public virtual ICollection<DrprocessingDo> DrprocessingDoLastModifiedUsers { get; set; } = new List<DrprocessingDo>();

    public virtual ICollection<Drprocessing> DrprocessingLastModifiedUsers { get; set; } = new List<Drprocessing>();

    public virtual ICollection<EventLog> EventLogs { get; set; } = new List<EventLog>();

    public virtual ICollection<GltrxIdtrash> GltrxIdtrashes { get; set; } = new List<GltrxIdtrash>();

    public virtual ICollection<Gr> GrCreatedUsers { get; set; } = new List<Gr>();

    public virtual ICollection<Gr> GrLastModifiedUsers { get; set; } = new List<Gr>();

    public virtual ICollection<Gt> GtCreatedUsers { get; set; } = new List<Gt>();

    public virtual ICollection<Gt> GtLastModifiedUsers { get; set; } = new List<Gt>();

    public virtual ICollection<Iss> IssCreatedUsers { get; set; } = new List<Iss>();

    public virtual ICollection<Iss> IssLastModifiedUsers { get; set; } = new List<Iss>();

    public virtual ICollection<Item> ItemCreatedUsers { get; set; } = new List<Item>();

    public virtual ICollection<Item> ItemLastModifiedUsers { get; set; } = new List<Item>();

    public virtual ICollection<Iv> IvCreatedUsers { get; set; } = new List<Iv>();

    public virtual ICollection<Iv> IvLastModifiedUsers { get; set; } = new List<Iv>();

    public virtual ICollection<Je> JeCreatedUsers { get; set; } = new List<Je>();

    public virtual ICollection<Je> JeLastModifiedUsers { get; set; } = new List<Je>();

    public virtual Location? LocationNavigation { get; set; }

    public virtual ICollection<Pi> PiCreatedUsers { get; set; } = new List<Pi>();

    public virtual ICollection<Pi> PiLastModifiedUsers { get; set; } = new List<Pi>();

    public virtual ICollection<Po> PoCreatedUsers { get; set; } = new List<Po>();

    public virtual ICollection<Po> PoLastModifiedUsers { get; set; } = new List<Po>();

    public virtual ICollection<Pq> PqApprovalUsers { get; set; } = new List<Pq>();

    public virtual ICollection<Pq> PqCreatedUsers { get; set; } = new List<Pq>();

    public virtual ICollection<Pq> PqLastModifiedUsers { get; set; } = new List<Pq>();

    public virtual ICollection<Pr> PrCreatedUsers { get; set; } = new List<Pr>();

    public virtual ICollection<Pr> PrLastModifiedUsers { get; set; } = new List<Pr>();

    public virtual Project? ProjNoNavigation { get; set; }

    public virtual ICollection<Prprocessing> PrprocessingCreatedUsers { get; set; } = new List<Prprocessing>();

    public virtual ICollection<Prprocessing> PrprocessingLastModifiedUsers { get; set; } = new List<Prprocessing>();

    public virtual ICollection<PrprocessingPo> PrprocessingPoCreatedUsers { get; set; } = new List<PrprocessingPo>();

    public virtual ICollection<PrprocessingPo> PrprocessingPoLastModifiedUsers { get; set; } = new List<PrprocessingPo>();

    public virtual ICollection<PurchaseConsignment> PurchaseConsignmentCreatedUsers { get; set; } = new List<PurchaseConsignment>();

    public virtual ICollection<PurchaseConsignment> PurchaseConsignmentLastModifiedUsers { get; set; } = new List<PurchaseConsignment>();

    public virtual ICollection<PurchaseConsignmentReturn> PurchaseConsignmentReturnCreatedUsers { get; set; } = new List<PurchaseConsignmentReturn>();

    public virtual ICollection<PurchaseConsignmentReturn> PurchaseConsignmentReturnLastModifiedUsers { get; set; } = new List<PurchaseConsignmentReturn>();

    public virtual ICollection<Qt> QtApprovalUsers { get; set; } = new List<Qt>();

    public virtual ICollection<Qt> QtCreatedUsers { get; set; } = new List<Qt>();

    public virtual ICollection<Qt> QtLastModifiedUsers { get; set; } = new List<Qt>();

    public virtual ICollection<Rcv> RcvCreatedUsers { get; set; } = new List<Rcv>();

    public virtual ICollection<Rcv> RcvLastModifiedUsers { get; set; } = new List<Rcv>();

    public virtual ICollection<Recurrence> RecurrenceAlertUsers { get; set; } = new List<Recurrence>();

    public virtual ICollection<Recurrence> RecurrenceCreatedUsers { get; set; } = new List<Recurrence>();

    public virtual ICollection<Recurrence> RecurrenceLastModifiedUsers { get; set; } = new List<Recurrence>();

    public virtual ICollection<Rq> RqCreatedUsers { get; set; } = new List<Rq>();

    public virtual ICollection<Rq> RqLastModifiedUsers { get; set; } = new List<Rq>();

    public virtual ICollection<So> SoCreatedUsers { get; set; } = new List<So>();

    public virtual ICollection<So> SoLastModifiedUsers { get; set; } = new List<So>();

    public virtual ICollection<Sodtl> SodtlLastAorpmodifiedUsers { get; set; } = new List<Sodtl>();

    public virtual ICollection<Sodtl> SodtlLastDrpmodifiedUsers { get; set; } = new List<Sodtl>();

    public virtual ICollection<Sodtl> SodtlLastOpmodifiedUsers { get; set; } = new List<Sodtl>();

    public virtual ICollection<StockDisassembly> StockDisassemblyCreatedUsers { get; set; } = new List<StockDisassembly>();

    public virtual ICollection<StockDisassembly> StockDisassemblyLastModifiedUsers { get; set; } = new List<StockDisassembly>();

    public virtual ICollection<StockTake> StockTakeCreatedUsers { get; set; } = new List<StockTake>();

    public virtual ICollection<StockTake> StockTakeLastModifiedUsers { get; set; } = new List<StockTake>();

    public virtual ICollection<TaxTransAudit> TaxTransAudits { get; set; } = new List<TaxTransAudit>();

    public virtual ICollection<UnapprovedDocument> UnapprovedDocuments { get; set; } = new List<UnapprovedDocument>();

    public virtual ICollection<Uomconv> UomconvCreatedUsers { get; set; } = new List<Uomconv>();

    public virtual ICollection<Uomconv> UomconvLastModifiedUsers { get; set; } = new List<Uomconv>();

    public virtual ICollection<UpdateCost> UpdateCostCreatedUsers { get; set; } = new List<UpdateCost>();

    public virtual ICollection<UpdateCost> UpdateCostLastModifiedUsers { get; set; } = new List<UpdateCost>();

    public virtual ICollection<Woff> WoffCreatedUsers { get; set; } = new List<Woff>();

    public virtual ICollection<Woff> WoffLastModifiedUsers { get; set; } = new List<Woff>();

    public virtual ICollection<Xs> XCreatedUsers { get; set; } = new List<Xs>();

    public virtual ICollection<Xs> XLastModifiedUsers { get; set; } = new List<Xs>();

    public virtual ICollection<Xfer> XferCreatedUsers { get; set; } = new List<Xfer>();

    public virtual ICollection<Xfer> XferLastModifiedUsers { get; set; } = new List<Xfer>();

    public virtual ICollection<Xp> XpCreatedUsers { get; set; } = new List<Xp>();

    public virtual ICollection<Xp> XpLastModifiedUsers { get; set; } = new List<Xp>();
}
