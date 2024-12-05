namespace ViteSales.Data.Entities;

public partial class UnapprovedDocument
{
    public long DocKey { get; set; }

    public string DocType { get; set; } = null!;

    public DateTime SaveTime { get; set; }

    public string DocNo { get; set; } = null!;

    public string? AccNo { get; set; }

    public string UserId { get; set; } = null!;

    public decimal NetTotal { get; set; }

    public string? Description { get; set; }

    public byte[]? Data { get; set; }

    public virtual Glmast? AccNoNavigation { get; set; }

    public virtual User User { get; set; } = null!;
}
