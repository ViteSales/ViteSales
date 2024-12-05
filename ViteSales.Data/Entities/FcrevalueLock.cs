namespace ViteSales.Data.Entities;

public partial class FcrevalueLock
{
    public long DtlKey { get; set; }

    public long FcrevalueKey { get; set; }

    public string ObjType { get; set; } = null!;

    public long ObjKey { get; set; }

    public string? PrevRevalue { get; set; }

    public decimal? PrevRevalueRate { get; set; }

    public decimal? RevalueRate { get; set; }
}
