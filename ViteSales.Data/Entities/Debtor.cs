namespace ViteSales.Data.Entities;

public partial class Debtor
{
    public long AutoKey { get; set; }

    public string AccNo { get; set; } = null!;

    public string? CompanyName { get; set; }

    public string? Desc2 { get; set; }

    public string? RegisterNo { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? Address3 { get; set; }

    public string? Address4 { get; set; }

    public string? PostCode { get; set; }

    public string? DeliverAddr1 { get; set; }

    public string? DeliverAddr2 { get; set; }

    public string? DeliverAddr3 { get; set; }

    public string? DeliverAddr4 { get; set; }

    public string? DeliverPostCode { get; set; }

    public string? Attention { get; set; }

    public string? Phone1 { get; set; }

    public string? Phone2 { get; set; }

    public string? Fax1 { get; set; }

    public string? Fax2 { get; set; }

    public string? AreaCode { get; set; }

    public string? SalesAgent { get; set; }

    public string? DebtorType { get; set; }

    public string? NatureOfBusiness { get; set; }

    public string? WebUrl { get; set; }

    public string? EmailAddress { get; set; }

    public string DisplayTerm { get; set; } = null!;

    public decimal? CreditLimit { get; set; }

    public string? AgingOn { get; set; }

    public string? StatementType { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public string AllowExceedCreditLimit { get; set; } = null!;

    public string? Note { get; set; }

    public string? ExemptNo { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string? PriceCategory { get; set; }

    public string? TaxCode { get; set; }

    public decimal DiscountPercent { get; set; }

    public string? DetailDiscount { get; set; }

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public decimal? OverdueLimit { get; set; }

    public string HasBonusPoint { get; set; } = null!;

    public decimal? OpeningBonusPoint { get; set; }

    public short? QtblockStatus { get; set; }

    public short? SoblockStatus { get; set; }

    public short? DoblockStatus { get; set; }

    public short? IvblockStatus { get; set; }

    public short? CsblockStatus { get; set; }

    public string? QtblockMessage { get; set; }

    public string? SoblockMessage { get; set; }

    public string? DoblockMessage { get; set; }

    public string? IvblockMessage { get; set; }

    public string? CsblockMessage { get; set; }

    public string? ExternalLink { get; set; }

    public string IsGroupCompany { get; set; } = null!;

    public string IsActive { get; set; } = null!;

    public int LastUpdate { get; set; }

    public string? ContactInfo { get; set; }

    public string? AccountGroup { get; set; }

    public decimal? MarkupRatio { get; set; }

    public string? CalcDiscountOnUnitPrice { get; set; }

    public DateTime? GststatusVerifiedDate { get; set; }

    public string InclusiveTax { get; set; } = null!;

    public int RoundingMethod { get; set; }

    public string? SelfBilledApprovalNo { get; set; }

    public Guid Guid { get; set; }

    public string? IsTaxRegistered { get; set; }

    public string? WithholdingTaxCode { get; set; }

    public string? MultiPrice { get; set; }

    public string? AllowChangeMultiPrice { get; set; }

    public string? Mobile { get; set; }

    public string? UdfCloseDay { get; set; }

    public string? UdfRestTime { get; set; }

    public string? UdfUnload { get; set; }

    public string? UdfLorrySize { get; set; }

    public decimal? UdfWeightCharges { get; set; }

    public decimal? UdfHandleCharges { get; set; }

    public short? CgblockStatus { get; set; }

    public string? CgblockMessage { get; set; }

    public string? WithholdingVatcode { get; set; }

    public int? TaxEntityId { get; set; }

    public string? GenerateLinkResultJson { get; set; }

    public string? IsCashSaleDebtor { get; set; }

    public virtual Glmast AccNoNavigation { get; set; } = null!;

    public virtual Area? AreaCodeNavigation { get; set; }

    public virtual User CreatedUser { get; set; } = null!;

    public virtual Currency CurrencyCodeNavigation { get; set; } = null!;

    public virtual DebtorType? DebtorTypeNavigation { get; set; }

    public virtual Term DisplayTermNavigation { get; set; } = null!;

    public virtual User LastModifiedUser { get; set; } = null!;

    public virtual ICollection<PriceBookRule> PriceBookRuleFromDebtorCodeNavigations { get; set; } = new List<PriceBookRule>();

    public virtual ICollection<PriceBookRule> PriceBookRuleToDebtorCodeNavigations { get; set; } = new List<PriceBookRule>();

    public virtual PriceCategory? PriceCategoryNavigation { get; set; }

    public virtual SalesAgent? SalesAgentNavigation { get; set; }

    public virtual TaxCode? TaxCodeNavigation { get; set; }

    public virtual TaxEntity? TaxEntity { get; set; }

    public virtual WithholdingTax? WithholdingTaxCodeNavigation { get; set; }

    public virtual WithholdingTax? WithholdingVatcodeNavigation { get; set; }
}
