namespace ViteSales.Data.Entities;

public partial class Location
{
    public long AutoKey { get; set; }

    public string Location1 { get; set; } = null!;

    public string? Description { get; set; }

    public string? Desc2 { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? Address3 { get; set; }

    public string? Address4 { get; set; }

    public string? PostCode { get; set; }

    public string? Phone1 { get; set; }

    public string? Phone2 { get; set; }

    public string? Fax1 { get; set; }

    public string? Fax2 { get; set; }

    public string? Contact { get; set; }

    public string? Note { get; set; }

    public string IsActive { get; set; } = null!;

    public int LastUpdate { get; set; }

    public string? AreaCode { get; set; }

    public string? CashPaymentMethod { get; set; }

    public string? DebitCardPaymentMethod { get; set; }

    public string? VoucherPaymentMethod { get; set; }

    public string? ChequePaymentMethod { get; set; }

    public string? PointPaymentMethod { get; set; }

    public string? RoundingAccNo { get; set; }

    public string? DepositAccNo { get; set; }

    public string? ForfeitedAccNo { get; set; }

    public string? CreditCardChargesAccNo { get; set; }

    public string? PointPaymentAccNo { get; set; }

    public string? ServiceChargeAccNo { get; set; }

    public string? VoucherForfeitedAccNo { get; set; }

    public Guid Guid { get; set; }

    public string? TipAccNo { get; set; }

    public virtual ICollection<Adjdtl> Adjdtls { get; set; } = new List<Adjdtl>();

    public virtual ICollection<Advqtdtl> Advqtdtls { get; set; } = new List<Advqtdtl>();

    public virtual ICollection<Advqt> Advqts { get; set; } = new List<Advqt>();

    public virtual ICollection<Aorprocessing> Aorprocessings { get; set; } = new List<Aorprocessing>();

    public virtual Area? AreaCodeNavigation { get; set; }

    public virtual ICollection<AsmRprocessing> AsmRprocessings { get; set; } = new List<AsmRprocessing>();

    public virtual ICollection<Asmdtl> Asmdtls { get; set; } = new List<Asmdtl>();

    public virtual ICollection<AsmorderDtl> AsmorderDtls { get; set; } = new List<AsmorderDtl>();

    public virtual ICollection<Asmorder> Asmorders { get; set; } = new List<Asmorder>();

    public virtual ICollection<Asm> Asms { get; set; } = new List<Asm>();

    public virtual ICollection<BonusPointRedemptionDtl> BonusPointRedemptionDtls { get; set; } = new List<BonusPointRedemptionDtl>();

    public virtual PaymentMethod? CashPaymentMethodNavigation { get; set; }

    public virtual PaymentMethod? ChequePaymentMethodNavigation { get; set; }

    public virtual ICollection<Cndtl> Cndtls { get; set; } = new List<Cndtl>();

    public virtual ICollection<Cn> Cns { get; set; } = new List<Cn>();

    public virtual ICollection<ConsignmentDtl> ConsignmentDtls { get; set; } = new List<ConsignmentDtl>();

    public virtual ICollection<ConsignmentReturnDtl> ConsignmentReturnDtls { get; set; } = new List<ConsignmentReturnDtl>();

    public virtual ICollection<ConsignmentReturn> ConsignmentReturns { get; set; } = new List<ConsignmentReturn>();

    public virtual ICollection<Consignment> Consignments { get; set; } = new List<Consignment>();

    public virtual ICollection<Cpdtl> Cpdtls { get; set; } = new List<Cpdtl>();

    public virtual ICollection<Cp> Cps { get; set; } = new List<Cp>();

    public virtual Glmast? CreditCardChargesAccNoNavigation { get; set; }

    public virtual ICollection<CashSales> Cs { get; set; } = new List<CashSales>();

    public virtual ICollection<CashSalesdtl> Csdtls { get; set; } = new List<CashSalesdtl>();

    public virtual ICollection<CsgnitemBalQty> CsgnitemBalQties { get; set; } = new List<CsgnitemBalQty>();

    public virtual ICollection<Csgnxferdtl> Csgnxferdtls { get; set; } = new List<Csgnxferdtl>();

    public virtual PaymentMethod? DebitCardPaymentMethodNavigation { get; set; }

    public virtual Glmast? DepositAccNoNavigation { get; set; }

    public virtual ICollection<Dndtl> Dndtls { get; set; } = new List<Dndtl>();

    public virtual ICollection<Dn> Dns { get; set; } = new List<Dn>();

    public virtual ICollection<Dodtl> Dodtls { get; set; } = new List<Dodtl>();

    public virtual ICollection<Do> Dos { get; set; } = new List<Do>();

    public virtual ICollection<Drdtl> Drdtls { get; set; } = new List<Drdtl>();

    public virtual ICollection<Drprocessing> Drprocessings { get; set; } = new List<Drprocessing>();

    public virtual ICollection<Dr> Drs { get; set; } = new List<Dr>();

    public virtual Glmast? ForfeitedAccNoNavigation { get; set; }

    public virtual ICollection<Grdtl> Grdtls { get; set; } = new List<Grdtl>();

    public virtual ICollection<Gr> Grs { get; set; } = new List<Gr>();

    public virtual ICollection<Gtdtl> Gtdtls { get; set; } = new List<Gtdtl>();

    public virtual ICollection<Gt> Gts { get; set; } = new List<Gt>();

    public virtual ICollection<Iphist> Iphists { get; set; } = new List<Iphist>();

    public virtual ICollection<Issdtl> Issdtls { get; set; } = new List<Issdtl>();

    public virtual ICollection<ItemBatchBalQty> ItemBatchBalQties { get; set; } = new List<ItemBatchBalQty>();

    public virtual ICollection<ItemLocationPrice> ItemLocationPrices { get; set; } = new List<ItemLocationPrice>();

    public virtual ICollection<ItemOpening> ItemOpenings { get; set; } = new List<ItemOpening>();

    public virtual ICollection<ItemSerialNoDtl> ItemSerialNoDtls { get; set; } = new List<ItemSerialNoDtl>();

    public virtual ICollection<Ivdtl> Ivdtls { get; set; } = new List<Ivdtl>();

    public virtual ICollection<Iv> Ivs { get; set; } = new List<Iv>();

    public virtual ICollection<Pidtl> Pidtls { get; set; } = new List<Pidtl>();

    public virtual ICollection<Pi> Pis { get; set; } = new List<Pi>();

    public virtual ICollection<Podtl> Podtls { get; set; } = new List<Podtl>();

    public virtual Glmast? PointPaymentAccNoNavigation { get; set; }

    public virtual PaymentMethod? PointPaymentMethodNavigation { get; set; }

    public virtual ICollection<Po> Pos { get; set; } = new List<Po>();

    public virtual ICollection<Pqdtl> Pqdtls { get; set; } = new List<Pqdtl>();

    public virtual ICollection<Pq> Pqs { get; set; } = new List<Pq>();

    public virtual ICollection<Prdtl> Prdtls { get; set; } = new List<Prdtl>();

    public virtual ICollection<PriceBookRule> PriceBookRuleFromLocationNavigations { get; set; } = new List<PriceBookRule>();

    public virtual ICollection<PriceBookRule> PriceBookRuleToLocationNavigations { get; set; } = new List<PriceBookRule>();

    public virtual ICollection<Prprocessing> Prprocessings { get; set; } = new List<Prprocessing>();

    public virtual ICollection<Pr> Prs { get; set; } = new List<Pr>();

    public virtual ICollection<PurchaseConsignmentDtl> PurchaseConsignmentDtls { get; set; } = new List<PurchaseConsignmentDtl>();

    public virtual ICollection<PurchaseConsignmentReturnDtl> PurchaseConsignmentReturnDtls { get; set; } = new List<PurchaseConsignmentReturnDtl>();

    public virtual ICollection<PurchaseConsignmentReturn> PurchaseConsignmentReturns { get; set; } = new List<PurchaseConsignmentReturn>();

    public virtual ICollection<PurchaseConsignment> PurchaseConsignments { get; set; } = new List<PurchaseConsignment>();

    public virtual ICollection<Qtdtl> Qtdtls { get; set; } = new List<Qtdtl>();

    public virtual ICollection<Qt> Qts { get; set; } = new List<Qt>();

    public virtual ICollection<Rcvdtl> Rcvdtls { get; set; } = new List<Rcvdtl>();

    public virtual Glmast? RoundingAccNoNavigation { get; set; }

    public virtual ICollection<Rqdtl> Rqdtls { get; set; } = new List<Rqdtl>();

    public virtual ICollection<Rq> Rqs { get; set; } = new List<Rq>();

    public virtual Glmast? ServiceChargeAccNoNavigation { get; set; }

    public virtual ICollection<Sodtl> Sodtls { get; set; } = new List<Sodtl>();

    public virtual ICollection<So> Sos { get; set; } = new List<So>();

    public virtual ICollection<StockDisassembly> StockDisassemblies { get; set; } = new List<StockDisassembly>();

    public virtual ICollection<StockDisassemblyDtl> StockDisassemblyDtls { get; set; } = new List<StockDisassemblyDtl>();

    public virtual ICollection<StockDtl> StockDtls { get; set; } = new List<StockDtl>();

    public virtual ICollection<StockTake> StockTakes { get; set; } = new List<StockTake>();

    public virtual Glmast? TipAccNoNavigation { get; set; }

    public virtual ICollection<UomconvDtl> UomconvDtls { get; set; } = new List<UomconvDtl>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<UtdstockCost> UtdstockCosts { get; set; } = new List<UtdstockCost>();

    public virtual Glmast? VoucherForfeitedAccNoNavigation { get; set; }

    public virtual PaymentMethod? VoucherPaymentMethodNavigation { get; set; }

    public virtual ICollection<Woffdtl> Woffdtls { get; set; } = new List<Woffdtl>();

    public virtual ICollection<Xs> Xes { get; set; } = new List<Xs>();

    public virtual ICollection<Xfer> XferFromLocationNavigations { get; set; } = new List<Xfer>();

    public virtual ICollection<Xfer> XferToLocationNavigations { get; set; } = new List<Xfer>();

    public virtual ICollection<Xpdtl> Xpdtls { get; set; } = new List<Xpdtl>();

    public virtual ICollection<Xp> Xps { get; set; } = new List<Xp>();

    public virtual ICollection<Xsdtl> Xsdtls { get; set; } = new List<Xsdtl>();
}
