namespace ViteSales.Data.Entities;

public partial class ArdepositRefund
{
    public long RefundKey { get; set; }

    public long DocKey { get; set; }

    public string DocNo { get; set; } = null!;

    public DateTime DocDate { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Note { get; set; }

    public decimal? RefundAmt { get; set; }

    public long? Cbkey { get; set; }

    public short PrintCount { get; set; }

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public virtual User CreatedUser { get; set; } = null!;

    public virtual User LastModifiedUser { get; set; } = null!;
}
