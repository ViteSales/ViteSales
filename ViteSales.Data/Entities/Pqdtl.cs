namespace ViteSales.Data.Entities;

public partial class Pqdtl
{
    public long DtlKey { get; set; }

    public long DocKey { get; set; }

    public int Seq { get; set; }

    public byte? Indent { get; set; }

    public string? FontStyle { get; set; }

    public string MainItem { get; set; } = null!;

    public string? Numbering { get; set; }

    public string? ItemCode { get; set; }

    public string? Location { get; set; }

    public string? Description { get; set; }

    public string? FurtherDescription { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public string? Uom { get; set; }

    public string? UserUom { get; set; }

    public decimal? Qty { get; set; }

    public decimal? Rate { get; set; }

    public decimal? SmallestQty { get; set; }

    public decimal TransferedQty { get; set; }

    public string? DtlType { get; set; }

    public long? PackageDocKey { get; set; }

    public long? ParentDtlKey { get; set; }

    public decimal? SubQty { get; set; }

    public string? CreditorCode { get; set; }

    public string? CreditorName { get; set; }

    public DateTime? RequiredDate { get; set; }

    public string Transferable { get; set; } = null!;

    public string PrintOut { get; set; } = null!;

    public Guid Guid { get; set; }

    public string? Desc2 { get; set; }

    public virtual Glmast? CreditorCodeNavigation { get; set; }

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual ItemUom? ItemUom { get; set; }

    public virtual Location? LocationNavigation { get; set; }

    public virtual Project? ProjNoNavigation { get; set; }
}
