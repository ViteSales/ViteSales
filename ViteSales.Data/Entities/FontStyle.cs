namespace ViteSales.Data.Entities;

public partial class FontStyle
{
    public long AutoKey { get; set; }

    public string FontStyle1 { get; set; } = null!;

    public string FontName { get; set; } = null!;

    public decimal FontSize { get; set; }

    public int FontColor { get; set; }

    public string Bold { get; set; } = null!;

    public string Italic { get; set; } = null!;

    public string Underline { get; set; } = null!;

    public byte? Indent { get; set; }

    public virtual ICollection<Advqtdtl> Advqtdtls { get; set; } = new List<Advqtdtl>();

    public virtual ICollection<Cndtl> Cndtls { get; set; } = new List<Cndtl>();

    public virtual ICollection<ConsignmentDtl> ConsignmentDtls { get; set; } = new List<ConsignmentDtl>();

    public virtual ICollection<ConsignmentReturnDtl> ConsignmentReturnDtls { get; set; } = new List<ConsignmentReturnDtl>();

    public virtual ICollection<Cpdtl> Cpdtls { get; set; } = new List<Cpdtl>();

    public virtual ICollection<CashSalesdtl> Csdtls { get; set; } = new List<CashSalesdtl>();

    public virtual ICollection<Dndtl> Dndtls { get; set; } = new List<Dndtl>();

    public virtual ICollection<Dodtl> Dodtls { get; set; } = new List<Dodtl>();

    public virtual ICollection<Drdtl> Drdtls { get; set; } = new List<Drdtl>();

    public virtual ICollection<Grdtl> Grdtls { get; set; } = new List<Grdtl>();

    public virtual ICollection<Gtdtl> Gtdtls { get; set; } = new List<Gtdtl>();

    public virtual ICollection<Ivdtl> Ivdtls { get; set; } = new List<Ivdtl>();

    public virtual ICollection<Pidtl> Pidtls { get; set; } = new List<Pidtl>();

    public virtual ICollection<Podtl> Podtls { get; set; } = new List<Podtl>();

    public virtual ICollection<Prdtl> Prdtls { get; set; } = new List<Prdtl>();

    public virtual ICollection<PurchaseConsignmentDtl> PurchaseConsignmentDtls { get; set; } = new List<PurchaseConsignmentDtl>();

    public virtual ICollection<PurchaseConsignmentReturnDtl> PurchaseConsignmentReturnDtls { get; set; } = new List<PurchaseConsignmentReturnDtl>();

    public virtual ICollection<Qtdtl> Qtdtls { get; set; } = new List<Qtdtl>();

    public virtual ICollection<Rqdtl> Rqdtls { get; set; } = new List<Rqdtl>();

    public virtual ICollection<Sodtl> Sodtls { get; set; } = new List<Sodtl>();

    public virtual ICollection<Xpdtl> Xpdtls { get; set; } = new List<Xpdtl>();

    public virtual ICollection<Xsdtl> Xsdtls { get; set; } = new List<Xsdtl>();
}
