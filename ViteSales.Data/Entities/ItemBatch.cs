namespace ViteSales.Data.Entities;

public partial class ItemBatch
{
    public long AutoKey { get; set; }

    public string ItemCode { get; set; } = null!;

    public string BatchNo { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? ManufacturedDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public DateTime? LastSaleDate { get; set; }

    public Guid Guid { get; set; }

    public virtual ICollection<Adjdtl> Adjdtls { get; set; } = new List<Adjdtl>();

    public virtual ICollection<Asmdtl> Asmdtls { get; set; } = new List<Asmdtl>();

    public virtual ICollection<AsmorderDtl> AsmorderDtls { get; set; } = new List<AsmorderDtl>();

    public virtual ICollection<Asmorder> Asmorders { get; set; } = new List<Asmorder>();

    public virtual ICollection<Asm> Asms { get; set; } = new List<Asm>();

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

    public virtual ICollection<Grdtl> Grdtls { get; set; } = new List<Grdtl>();

    public virtual ICollection<Gtdtl> Gtdtls { get; set; } = new List<Gtdtl>();

    public virtual ICollection<Iphist> Iphists { get; set; } = new List<Iphist>();

    public virtual ICollection<Issdtl> Issdtls { get; set; } = new List<Issdtl>();

    public virtual ICollection<ItemOpening> ItemOpenings { get; set; } = new List<ItemOpening>();

    public virtual ICollection<ItemSerialNo> ItemSerialNos { get; set; } = new List<ItemSerialNo>();

    public virtual ICollection<Ivdtl> Ivdtls { get; set; } = new List<Ivdtl>();

    public virtual ICollection<Pidtl> Pidtls { get; set; } = new List<Pidtl>();

    public virtual ICollection<Prdtl> Prdtls { get; set; } = new List<Prdtl>();

    public virtual ICollection<PurchaseConsignmentDtl> PurchaseConsignmentDtls { get; set; } = new List<PurchaseConsignmentDtl>();

    public virtual ICollection<PurchaseConsignmentReturnDtl> PurchaseConsignmentReturnDtls { get; set; } = new List<PurchaseConsignmentReturnDtl>();

    public virtual ICollection<Rcvdtl> Rcvdtls { get; set; } = new List<Rcvdtl>();

    public virtual ICollection<StockDisassembly> StockDisassemblies { get; set; } = new List<StockDisassembly>();

    public virtual ICollection<StockDisassemblyDtl> StockDisassemblyDtls { get; set; } = new List<StockDisassemblyDtl>();

    public virtual ICollection<StockTakeDtl> StockTakeDtls { get; set; } = new List<StockTakeDtl>();

    public virtual ICollection<UomconvDtl> UomconvDtls { get; set; } = new List<UomconvDtl>();

    public virtual ICollection<UtdstockCost> UtdstockCosts { get; set; } = new List<UtdstockCost>();

    public virtual ICollection<Woffdtl> Woffdtls { get; set; } = new List<Woffdtl>();

    public virtual ICollection<Xferdtl> Xferdtls { get; set; } = new List<Xferdtl>();
}
