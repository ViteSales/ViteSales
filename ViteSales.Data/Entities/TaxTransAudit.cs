namespace ViteSales.Data.Entities;

public partial class TaxTransAudit
{
    public long TaxTransAuditKey { get; set; }

    public DateTime AuditDateTime { get; set; }

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
