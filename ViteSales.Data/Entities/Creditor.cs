namespace ViteSales.Data.Entities;

public partial class Creditor
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

    public string? PurchaseAgent { get; set; }

    public string? CreditorType { get; set; }

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

    public short? PoblockStatus { get; set; }

    public short? GnblockStatus { get; set; }

    public short? PiblockStatus { get; set; }

    public short? CpblockStatus { get; set; }

    public string? PoblockMessage { get; set; }

    public string? GnblockMessage { get; set; }

    public string? PiblockMessage { get; set; }

    public string? CpblockMessage { get; set; }

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

    public string? SelfBilledApprovalNo { get; set; }

    public int RoundingMethod { get; set; }

    public string? WithholdingTaxCode { get; set; }

    public string? Mobile { get; set; }

    public short? PgblockStatus { get; set; }

    public string? PgblockMessage { get; set; }

    public string? WithholdingVatcode { get; set; }

    public int? TaxEntityId { get; set; }

    public string? GenerateLinkResultJson { get; set; }

    public virtual Glmast AccNoNavigation { get; set; } = null!;

    public virtual Area? AreaCodeNavigation { get; set; }

    public virtual User CreatedUser { get; set; } = null!;

    public virtual CreditorType? CreditorTypeNavigation { get; set; }

    public virtual Currency CurrencyCodeNavigation { get; set; } = null!;

    public virtual Term DisplayTermNavigation { get; set; } = null!;

    public virtual User LastModifiedUser { get; set; } = null!;

    public virtual PriceCategory? PriceCategoryNavigation { get; set; }

    public virtual PurchaseAgent? PurchaseAgentNavigation { get; set; }

    public virtual TaxCode? TaxCodeNavigation { get; set; }

    public virtual TaxEntity? TaxEntity { get; set; }

    public virtual WithholdingTax? WithholdingTaxCodeNavigation { get; set; }

    public virtual WithholdingTax? WithholdingVatcodeNavigation { get; set; }
}
