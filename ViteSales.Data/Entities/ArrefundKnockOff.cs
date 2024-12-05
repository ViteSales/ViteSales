namespace ViteSales.Data.Entities;

public partial class ArrefundKnockOff
{
    public long KnockOffKey { get; set; }

    public long DocKey { get; set; }

    public string KnockOffDocType { get; set; } = null!;

    public long KnockOffDocKey { get; set; }

    public decimal? Amount { get; set; }

    public DateTime GainLossDate { get; set; }

    public string? Revalue { get; set; }

    public string? ProjNo { get; set; }

    public string? DeptNo { get; set; }

    public string? UseProjDept { get; set; }

    public long? FcrevalueKey { get; set; }

    public virtual Dept? DeptNoNavigation { get; set; }

    public virtual Project? ProjNoNavigation { get; set; }
}
