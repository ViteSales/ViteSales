namespace ViteSales.Data.Entities;

public partial class Accountant
{
    public string AccountantId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public byte[]? SchedulerData { get; set; }

    public string? IsActive { get; set; }

    public virtual User User { get; set; } = null!;
}
