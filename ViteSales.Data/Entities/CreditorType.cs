namespace ViteSales.Data.Entities;

public partial class CreditorType
{
    public long AutoKey { get; set; }

    public string CreditorTypeCode { get; set; } = null!;

    public string? Description { get; set; }

    public string? Desc2 { get; set; }

    public string IsActive { get; set; } = null!;

    public int LastUpdate { get; set; }

    public virtual ICollection<Creditor> Creditors { get; set; } = new List<Creditor>();
}
