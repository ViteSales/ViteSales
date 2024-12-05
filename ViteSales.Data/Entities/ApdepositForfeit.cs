namespace ViteSales.Data.Entities;

public partial class ApdepositForfeit
{
    public long ForfeitKey { get; set; }

    public long DocKey { get; set; }

    public DateTime DocDate { get; set; }

    public string? Description { get; set; }

    public decimal? ForfeitedAmt { get; set; }

    public string? ForfeitedAccNo { get; set; }

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public virtual User CreatedUser { get; set; } = null!;

    public virtual Glmast? ForfeitedAccNoNavigation { get; set; }

    public virtual User LastModifiedUser { get; set; } = null!;
}
