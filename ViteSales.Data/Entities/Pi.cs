namespace ViteSales.Data.Entities;

public partial class Pi
{
    public long DocKey { get; set; }

    public string DocNo { get; set; } = null!;

    public DateTime DocDate { get; set; }

    public string? CreditorCode { get; set; }

    public string? CreditorName { get; set; }

    public string? Ref { get; set; }

    public string? SupplierDono { get; set; }

    public string? SupplierInvoiceNo { get; set; }

    public string? Description { get; set; }

    public string DisplayTerm { get; set; } = null!;

    public string? PurchaseAgent { get; set; }

    public string? InvAddr1 { get; set; }

    public string? InvAddr2 { get; set; }

    public string? InvAddr3 { get; set; }

    public string? InvAddr4 { get; set; }

    public string? Phone1 { get; set; }

    public string? Fax1 { get; set; }

    public string? Attention { get; set; }

    public string? BranchCode { get; set; }

    public decimal? Total { get; set; }

    public decimal? Footer1Param { get; set; }

    public decimal? Footer1Amt { get; set; }

    public decimal? Footer1LocalAmt { get; set; }

    public string? Footer1TaxCode { get; set; }

    public decimal? Footer2Param { get; set; }

    public decimal? Footer2Amt { get; set; }

    public decimal? Footer2LocalAmt { get; set; }

    public string? Footer2TaxCode { get; set; }

    public decimal? Footer3Param { get; set; }

    public decimal? Footer3Amt { get; set; }

    public decimal? Footer3LocalAmt { get; set; }

    public string? Footer3TaxCode { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public decimal CurrencyRate { get; set; }

    public decimal? NetTotal { get; set; }

    public decimal? LocalNetTotal { get; set; }

    public decimal? Tax { get; set; }

    public decimal? LocalTax { get; set; }

    public decimal? ForeignCharges { get; set; }

    public decimal? LocalCharges { get; set; }

    public string? LandedCostMethod { get; set; }

    public string PostToStock { get; set; } = null!;

    public string PostToGl { get; set; } = null!;

    public long? ReferDocKey { get; set; }

    public long? ReferPaymentDocKey { get; set; }

    public string Transferable { get; set; } = null!;

    public string? ToDocType { get; set; }

    public long? ToDocKey { get; set; }

    public string? Note { get; set; }

    public string? Remark1 { get; set; }

    public string? Remark2 { get; set; }

    public string? Remark3 { get; set; }

    public string? Remark4 { get; set; }

    public short PrintCount { get; set; }

    public string Cancelled { get; set; } = null!;

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public string? ExternalLink { get; set; }

    public string? RefDocNo { get; set; }

    public string CanSync { get; set; } = null!;

    public int LastUpdate { get; set; }

    public long? ToDtlKey { get; set; }

    public string? FullTransferOption { get; set; }

    public string? ShipVia { get; set; }

    public string? ShipInfo { get; set; }

    public string? RefNo2 { get; set; }

    public string? PurchaseLocation { get; set; }

    public decimal? Footer1Tax { get; set; }

    public decimal? Footer1LocalTax { get; set; }

    public decimal? Footer2Tax { get; set; }

    public decimal? Footer2LocalTax { get; set; }

    public decimal? Footer3Tax { get; set; }

    public decimal? Footer3LocalTax { get; set; }

    public decimal? ExTax { get; set; }

    public decimal? LocalExTax { get; set; }

    public decimal? AnalysisNetTotal { get; set; }

    public decimal? LocalAnalysisNetTotal { get; set; }

    public Guid Guid { get; set; }

    public decimal ToTaxCurrencyRate { get; set; }

    public string? CalcDiscountOnUnitPrice { get; set; }

    public string? TaxDocNo { get; set; }

    public decimal? TotalExTax { get; set; }

    public decimal? TaxableAmt { get; set; }

    public string InclusiveTax { get; set; } = null!;

    public decimal? Footer1TaxRate { get; set; }

    public decimal? Footer2TaxRate { get; set; }

    public decimal? Footer3TaxRate { get; set; }

    public DateTime? TaxDate { get; set; }

    public string IsRoundAdj { get; set; } = null!;

    public decimal? RoundAdj { get; set; }

    public decimal? FinalTotal { get; set; }

    public string? SelfBilledApprovalNo { get; set; }

    public int RoundingMethod { get; set; }

    public decimal? LocalTaxableAmt { get; set; }

    public decimal? TaxCurrencyTax { get; set; }

    public decimal? TaxCurrencyTaxableAmt { get; set; }

    public DateTime? DocDate2 { get; set; }

    public decimal? WithholdingTax { get; set; }

    public decimal? LocalWithholdingTax { get; set; }

    public decimal? TaxCurrencyWithholdingTax { get; set; }

    public decimal? WithholdingVat { get; set; }

    public decimal? LocalWithholdingVat { get; set; }

    public decimal? TaxCurrencyWithholdingVat { get; set; }

    public DateTime? WhtpostingDate { get; set; }

    public int? TaxEntityId { get; set; }

    public string DocStatus { get; set; } = null!;

    public DateTime? ExpiryTimeStamp { get; set; }

    public string? EinvoiceSelfBilledDocNo { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual User CreatedUser { get; set; } = null!;

    public virtual Glmast? CreditorCodeNavigation { get; set; }

    public virtual Currency CurrencyCodeNavigation { get; set; } = null!;

    public virtual Term DisplayTermNavigation { get; set; } = null!;

    public virtual TaxCodes? Footer1TaxCodeNavigation { get; set; }

    public virtual TaxCodes? Footer2TaxCodeNavigation { get; set; }

    public virtual TaxCodes? Footer3TaxCodeNavigation { get; set; }

    public virtual User LastModifiedUser { get; set; } = null!;

    public virtual PurchaseAgent? PurchaseAgentNavigation { get; set; }

    public virtual Location? PurchaseLocationNavigation { get; set; }

    public virtual ShippingMethod? ShipViaNavigation { get; set; }

    public virtual TaxEntity? TaxEntity { get; set; }
}
