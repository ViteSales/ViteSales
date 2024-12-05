namespace ViteSales.Data.Entities;

public partial class ItemUom
{
    public long AutoKey { get; set; }

    public string ItemCode { get; set; } = null!;

    public string Uom { get; set; } = null!;

    public decimal Rate { get; set; }

    public string? Shelf { get; set; }

    public decimal? Price { get; set; }

    public decimal? Cost { get; set; }

    public decimal? RealCost { get; set; }

    public decimal? MostRecentlyCost { get; set; }

    public decimal? MinSalePrice { get; set; }

    public decimal? MaxSalePrice { get; set; }

    public decimal? MinPurchasePrice { get; set; }

    public decimal? MaxPurchasePrice { get; set; }

    public decimal? MinQty { get; set; }

    public decimal? MaxQty { get; set; }

    public decimal? NormalLevel { get; set; }

    public decimal? ReOlevel { get; set; }

    public decimal? ReOqty { get; set; }

    public decimal? Foclevel { get; set; }

    public decimal? Focqty { get; set; }

    public decimal? BonusPointQty { get; set; }

    public decimal? BonusPoint { get; set; }

    public decimal? Weight { get; set; }

    public string? WeightUom { get; set; }

    public decimal? Volume { get; set; }

    public string? VolumeUom { get; set; }

    public string? BarCode { get; set; }

    public int LastUpdate { get; set; }

    public decimal? RedeemBonusPoint { get; set; }

    public decimal? Csgnqty { get; set; }

    public decimal? Price2 { get; set; }

    public Guid Guid { get; set; }

    public decimal? Price3 { get; set; }

    public decimal? Price4 { get; set; }

    public decimal? Price5 { get; set; }

    public decimal? Price6 { get; set; }

    public decimal? MarkupRatio { get; set; }

    public decimal? MarkdownRatio2 { get; set; }

    public decimal? MarkdownRatio3 { get; set; }

    public decimal? MarkdownRatio4 { get; set; }

    public decimal? MarkdownRatio5 { get; set; }

    public decimal? MarkdownRatio6 { get; set; }

    public decimal? MarkdownRatioMinPrice { get; set; }

    public decimal? MarkdownRatioMaxPrice { get; set; }

    public bool AutoCalcPrice { get; set; }

    public bool AutoCalcPrice2 { get; set; }

    public bool AutoCalcPrice3 { get; set; }

    public bool AutoCalcPrice4 { get; set; }

    public bool AutoCalcPrice5 { get; set; }

    public bool AutoCalcPrice6 { get; set; }

    public bool AutoCalcMinSalePrice { get; set; }

    public bool AutoCalcMaxSalePrice { get; set; }

    public string? Measurement { get; set; }

    public virtual ICollection<Adjdtl> Adjdtls { get; set; } = new List<Adjdtl>();

    public virtual ICollection<Advqtdtl> Advqtdtls { get; set; } = new List<Advqtdtl>();

    public virtual ICollection<BonusPointRedemptionDtl> BonusPointRedemptionDtls { get; set; } = new List<BonusPointRedemptionDtl>();

    public virtual ICollection<Cndtl> Cndtls { get; set; } = new List<Cndtl>();

    public virtual ICollection<ConsignmentDtl> ConsignmentDtls { get; set; } = new List<ConsignmentDtl>();

    public virtual ICollection<ConsignmentReturnDtl> ConsignmentReturnDtls { get; set; } = new List<ConsignmentReturnDtl>();

    public virtual ICollection<Cpdtl> Cpdtls { get; set; } = new List<Cpdtl>();

    public virtual ICollection<CashSalesdtl> Csdtls { get; set; } = new List<CashSalesdtl>();

    public virtual ICollection<CsgnitemBalQty> CsgnitemBalQties { get; set; } = new List<CsgnitemBalQty>();

    public virtual ICollection<Csgnxferdtl> Csgnxferdtls { get; set; } = new List<Csgnxferdtl>();

    public virtual ICollection<Dndtl> Dndtls { get; set; } = new List<Dndtl>();

    public virtual ICollection<Dodtl> Dodtls { get; set; } = new List<Dodtl>();

    public virtual ICollection<Drdtl> Drdtls { get; set; } = new List<Drdtl>();

    public virtual ICollection<DrprocessingDo> DrprocessingDos { get; set; } = new List<DrprocessingDo>();

    public virtual ICollection<Drprocessing> Drprocessings { get; set; } = new List<Drprocessing>();

    public virtual ICollection<Grdtl> Grdtls { get; set; } = new List<Grdtl>();

    public virtual ICollection<Gtdtl> Gtdtls { get; set; } = new List<Gtdtl>();

    public virtual ICollection<Iphist> Iphists { get; set; } = new List<Iphist>();

    public virtual ICollection<Issdtl> Issdtls { get; set; } = new List<Issdtl>();

    public virtual ICollection<ItemBatchBalQty> ItemBatchBalQties { get; set; } = new List<ItemBatchBalQty>();

    public virtual ICollection<ItemCostChangeHistory> ItemCostChangeHistories { get; set; } = new List<ItemCostChangeHistory>();

    public virtual ICollection<ItemCostHistory> ItemCostHistories { get; set; } = new List<ItemCostHistory>();

    public virtual ICollection<ItemCurrencyPrice> ItemCurrencyPrices { get; set; } = new List<ItemCurrencyPrice>();

    public virtual ICollection<ItemLocationPrice> ItemLocationPrices { get; set; } = new List<ItemLocationPrice>();

    public virtual ICollection<ItemOpening> ItemOpenings { get; set; } = new List<ItemOpening>();

    public virtual ICollection<ItemPriceChangeHistory> ItemPriceChangeHistories { get; set; } = new List<ItemPriceChangeHistory>();

    public virtual ICollection<ItemPrice> ItemPrices { get; set; } = new List<ItemPrice>();

    public virtual ICollection<Ivdtl> Ivdtls { get; set; } = new List<Ivdtl>();

    public virtual ICollection<PackageDtl> PackageDtls { get; set; } = new List<PackageDtl>();

    public virtual ICollection<Pidtl> Pidtls { get; set; } = new List<Pidtl>();

    public virtual ICollection<Podtl> Podtls { get; set; } = new List<Podtl>();

    public virtual ICollection<Pqdtl> Pqdtls { get; set; } = new List<Pqdtl>();

    public virtual ICollection<Prdtl> Prdtls { get; set; } = new List<Prdtl>();

    public virtual ICollection<PrprocessingPo> PrprocessingPos { get; set; } = new List<PrprocessingPo>();

    public virtual ICollection<Prprocessing> Prprocessings { get; set; } = new List<Prprocessing>();

    public virtual ICollection<PurchaseConsignmentDtl> PurchaseConsignmentDtls { get; set; } = new List<PurchaseConsignmentDtl>();

    public virtual ICollection<PurchaseConsignmentReturnDtl> PurchaseConsignmentReturnDtls { get; set; } = new List<PurchaseConsignmentReturnDtl>();

    public virtual ICollection<Qtdtl> Qtdtls { get; set; } = new List<Qtdtl>();

    public virtual ICollection<Rcvdtl> Rcvdtls { get; set; } = new List<Rcvdtl>();

    public virtual ICollection<Rqdtl> Rqdtls { get; set; } = new List<Rqdtl>();

    public virtual ICollection<Sodtl> Sodtls { get; set; } = new List<Sodtl>();

    public virtual ICollection<StockDtl> StockDtls { get; set; } = new List<StockDtl>();

    public virtual ICollection<StockTakeDtl> StockTakeDtls { get; set; } = new List<StockTakeDtl>();

    public virtual ICollection<UomconvDtl> UomconvDtlItemUomNavigations { get; set; } = new List<UomconvDtl>();

    public virtual ICollection<UomconvDtl> UomconvDtlItemUoms { get; set; } = new List<UomconvDtl>();

    public virtual ICollection<UpdateCostDtl> UpdateCostDtls { get; set; } = new List<UpdateCostDtl>();

    public virtual ICollection<UtdstockCost> UtdstockCosts { get; set; } = new List<UtdstockCost>();

    public virtual ICollection<Woffdtl> Woffdtls { get; set; } = new List<Woffdtl>();

    public virtual ICollection<Xferdtl> Xferdtls { get; set; } = new List<Xferdtl>();

    public virtual ICollection<Xpdtl> Xpdtls { get; set; } = new List<Xpdtl>();

    public virtual ICollection<Xsdtl> Xsdtls { get; set; } = new List<Xsdtl>();
}
