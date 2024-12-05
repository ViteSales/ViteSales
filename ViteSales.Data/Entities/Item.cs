namespace ViteSales.Data.Entities;

public partial class Item
{
    public long AutoKey { get; set; }

    public string ItemCode { get; set; } = null!;

    public long DocKey { get; set; }

    public string? Description { get; set; }

    public string? Desc2 { get; set; }

    public string? FurtherDescription { get; set; }

    public string? ItemGroup { get; set; }

    public string? ItemType { get; set; }

    public decimal? AssemblyCost { get; set; }

    public string? LeadTime { get; set; }

    public string StockControl { get; set; } = null!;

    public string HasSerialNo { get; set; } = null!;

    public string HasBatchNo { get; set; } = null!;

    public decimal DutyRate { get; set; }

    public string? TaxCode { get; set; }

    public string? Note { get; set; }

    public byte[]? Image { get; set; }

    public byte CostingMethod { get; set; }

    public string SalesUom { get; set; } = null!;

    public string PurchaseUom { get; set; } = null!;

    public string ReportUom { get; set; } = null!;

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public string IsActive { get; set; } = null!;

    public int LastUpdate { get; set; }

    public string? SnformatName { get; set; }

    public string? IsCalcBonusPoint { get; set; }

    public decimal? MarkupRatio { get; set; }

    public string HasPromoter { get; set; } = null!;

    public string? GlobalCode { get; set; }

    public string? ItemBrand { get; set; }

    public string? ItemClass { get; set; }

    public string? ItemCategory { get; set; }

    public int? LeadTimeDay { get; set; }

    public string? ExternalLink { get; set; }

    public string Discontinued { get; set; } = null!;

    public string? AutoUomconversion { get; set; }

    public string BaseUom { get; set; } = null!;

    public string BackOrderControl { get; set; } = null!;

    public string? PurchaseTaxCode { get; set; }

    public string? TariffCode { get; set; }

    public Guid Guid { get; set; }

    public string? IsSalesItem { get; set; }

    public string? IsPurchaseItem { get; set; }

    public string? IsPositem { get; set; }

    public string? IsRawMaterialItem { get; set; }

    public string? IsFinishGoodsItem { get; set; }

    public string? MainSupplier { get; set; }

    public string? ImageFileName { get; set; }

    public string? UdfIsTransport { get; set; }

    public string? Classification { get; set; }

    public bool MustGenerateEinvoice { get; set; }

    public virtual ICollection<Aorprocessing> Aorprocessings { get; set; } = new List<Aorprocessing>();

    public virtual ICollection<AsmRprocessing> AsmRprocessings { get; set; } = new List<AsmRprocessing>();

    public virtual ICollection<Asmdtl> Asmdtls { get; set; } = new List<Asmdtl>();

    public virtual ICollection<AsmorderDtl> AsmorderDtls { get; set; } = new List<AsmorderDtl>();

    public virtual ICollection<Asmorder> Asmorders { get; set; } = new List<Asmorder>();

    public virtual ICollection<BomoptionalDtl> BomoptionalDtls { get; set; } = new List<BomoptionalDtl>();

    public virtual ICollection<BomoptionalLink> BomoptionalLinks { get; set; } = new List<BomoptionalLink>();

    public virtual User CreatedUser { get; set; } = null!;

    public virtual ICollection<ItemBom> ItemBomItemCodeNavigations { get; set; } = new List<ItemBom>();

    public virtual ICollection<ItemBom> ItemBomSubItemCodeNavigations { get; set; } = new List<ItemBom>();

    public virtual ItemBrand? ItemBrandNavigation { get; set; }

    public virtual ItemCategory? ItemCategoryNavigation { get; set; }

    public virtual ItemClass? ItemClassNavigation { get; set; }

    public virtual ICollection<ItemCostChangeHistory> ItemCostChangeHistories { get; set; } = new List<ItemCostChangeHistory>();

    public virtual ItemGroup? ItemGroupNavigation { get; set; }

    public virtual ICollection<ItemIngredient> ItemIngredientIngredientItemCodeNavigations { get; set; } = new List<ItemIngredient>();

    public virtual ICollection<ItemIngredient> ItemIngredientItemCodeNavigations { get; set; } = new List<ItemIngredient>();

    public virtual ICollection<ItemPriceChangeHistory> ItemPriceChangeHistories { get; set; } = new List<ItemPriceChangeHistory>();

    public virtual ICollection<ItemReplacement> ItemReplacementItemCodeNavigations { get; set; } = new List<ItemReplacement>();

    public virtual ICollection<ItemReplacement> ItemReplacementReplacementItemCodeNavigations { get; set; } = new List<ItemReplacement>();

    public virtual ICollection<ItemSerialNoDtl> ItemSerialNoDtls { get; set; } = new List<ItemSerialNoDtl>();

    public virtual ICollection<ItemSerialNo> ItemSerialNos { get; set; } = new List<ItemSerialNo>();

    public virtual ICollection<ItemSubCode> ItemSubCodes { get; set; } = new List<ItemSubCode>();

    public virtual ItemType? ItemTypeNavigation { get; set; }

    public virtual User LastModifiedUser { get; set; } = null!;

    public virtual ICollection<PriceBookRule> PriceBookRuleFromItemCodeNavigations { get; set; } = new List<PriceBookRule>();

    public virtual ICollection<PriceBookRule> PriceBookRuleToItemCodeNavigations { get; set; } = new List<PriceBookRule>();

    public virtual TaxCode? PurchaseTaxCodeNavigation { get; set; }

    public virtual ICollection<SerialNoTran> SerialNoTrans { get; set; } = new List<SerialNoTran>();

    public virtual ICollection<StockDisassemblyDtl> StockDisassemblyDtls { get; set; } = new List<StockDisassemblyDtl>();

    public virtual ICollection<StockDtlchangeQ> StockDtlchangeQs { get; set; } = new List<StockDtlchangeQ>();

    public virtual Tariff? TariffCodeNavigation { get; set; }

    public virtual TaxCode? TaxCodeNavigation { get; set; }
}
