namespace ViteSales.Data.Entities;

public partial class Currency
{
    public string CurrencyCode { get; set; } = null!;

    public string? CurrencyWord { get; set; }

    public string? CurrencyWord2 { get; set; }

    public string? CurrencySymbol { get; set; }

    public decimal BankBuyRate { get; set; }

    public decimal BankSellRate { get; set; }

    public string? FcgainAccount { get; set; }

    public string? FclossAccount { get; set; }

    public string? GainLossJournalType { get; set; }

    public int LastUpdate { get; set; }

    public Guid Guid { get; set; }

    public virtual ICollection<Advqt> Advqts { get; set; } = new List<Advqt>();

    public virtual ICollection<Apcn> Apcns { get; set; } = new List<Apcn>();

    public virtual ICollection<Apdeposit> Apdeposits { get; set; } = new List<Apdeposit>();

    public virtual ICollection<Apdn> Apdns { get; set; } = new List<Apdn>();

    public virtual ICollection<Apinvoice> Apinvoices { get; set; } = new List<Apinvoice>();

    public virtual ICollection<Appayment> Appayments { get; set; } = new List<Appayment>();

    public virtual ICollection<Aprefund> Aprefunds { get; set; } = new List<Aprefund>();

    public virtual ICollection<Arapcontra> Arapcontras { get; set; } = new List<Arapcontra>();

    public virtual ICollection<Arcn> Arcns { get; set; } = new List<Arcn>();

    public virtual ICollection<Ardeposit> Ardeposits { get; set; } = new List<Ardeposit>();

    public virtual ICollection<Ardn> Ardns { get; set; } = new List<Ardn>();

    public virtual ICollection<Arinvoice> Arinvoices { get; set; } = new List<Arinvoice>();

    public virtual ICollection<Arpayment> Arpayments { get; set; } = new List<Arpayment>();

    public virtual ICollection<Arrefund> Arrefunds { get; set; } = new List<Arrefund>();

    public virtual ICollection<Cb> Cbs { get; set; } = new List<Cb>();

    public virtual ICollection<Cn> Cns { get; set; } = new List<Cn>();

    public virtual ICollection<ConsignmentReturn> ConsignmentReturns { get; set; } = new List<ConsignmentReturn>();

    public virtual ICollection<Consignment> Consignments { get; set; } = new List<Consignment>();

    public virtual ICollection<Cp> Cps { get; set; } = new List<Cp>();

    public virtual ICollection<Creditor> Creditors { get; set; } = new List<Creditor>();

    public virtual ICollection<CashSales> Cs { get; set; } = new List<CashSales>();

    public virtual ICollection<CurrRate> CurrRates { get; set; } = new List<CurrRate>();

    public virtual ICollection<Debtor> Debtors { get; set; } = new List<Debtor>();

    public virtual ICollection<Dn> Dns { get; set; } = new List<Dn>();

    public virtual ICollection<Do> Dos { get; set; } = new List<Do>();

    public virtual ICollection<Dr> Drs { get; set; } = new List<Dr>();

    public virtual Glmast? FcgainAccountNavigation { get; set; }

    public virtual Glmast? FclossAccountNavigation { get; set; }

    public virtual ICollection<FcrevalueDocument> FcrevalueDocuments { get; set; } = new List<FcrevalueDocument>();

    public virtual ICollection<FcrevalueRate> FcrevalueRates { get; set; } = new List<FcrevalueRate>();

    public virtual Journal? GainLossJournalTypeNavigation { get; set; }

    public virtual ICollection<Gldtl> Gldtls { get; set; } = new List<Gldtl>();

    public virtual ICollection<Glmast> Glmasts { get; set; } = new List<Glmast>();

    public virtual ICollection<Gr> Grs { get; set; } = new List<Gr>();

    public virtual ICollection<Gt> Gts { get; set; } = new List<Gt>();

    public virtual ICollection<ItemCurrencyPrice> ItemCurrencyPrices { get; set; } = new List<ItemCurrencyPrice>();

    public virtual ICollection<Iv> Ivs { get; set; } = new List<Iv>();

    public virtual ICollection<Je> Jes { get; set; } = new List<Je>();

    public virtual ICollection<Pi> Pis { get; set; } = new List<Pi>();

    public virtual ICollection<Po> Pos { get; set; } = new List<Po>();

    public virtual ICollection<PriceBookRule> PriceBookRules { get; set; } = new List<PriceBookRule>();

    public virtual ICollection<Pr> Prs { get; set; } = new List<Pr>();

    public virtual ICollection<PurchaseConsignmentReturn> PurchaseConsignmentReturns { get; set; } = new List<PurchaseConsignmentReturn>();

    public virtual ICollection<PurchaseConsignment> PurchaseConsignments { get; set; } = new List<PurchaseConsignment>();

    public virtual ICollection<Qt> Qts { get; set; } = new List<Qt>();

    public virtual ICollection<Rq> Rqs { get; set; } = new List<Rq>();

    public virtual ICollection<So> Sos { get; set; } = new List<So>();

    public virtual ICollection<TaxTrans> TaxTrans { get; set; } = new List<TaxTrans>();

    public virtual ICollection<TaxTransAuditDtl> TaxTransAuditDtls { get; set; } = new List<TaxTransAuditDtl>();

    public virtual ICollection<TaxTransCancelled> TaxTransCancelleds { get; set; } = new List<TaxTransCancelled>();

    public virtual ICollection<UnrealizedGainLossDocument> UnrealizedGainLossDocuments { get; set; } = new List<UnrealizedGainLossDocument>();

    public virtual ICollection<UnrealizedGainLossRate> UnrealizedGainLossRates { get; set; } = new List<UnrealizedGainLossRate>();

    public virtual ICollection<Xs> Xes { get; set; } = new List<Xs>();

    public virtual ICollection<Xp> Xps { get; set; } = new List<Xp>();
}
