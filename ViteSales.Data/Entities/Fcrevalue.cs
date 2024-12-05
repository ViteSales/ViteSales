namespace ViteSales.Data.Entities;

public partial class Fcrevalue
{
    public long FcrevalueKey { get; set; }

    public string DocNo { get; set; } = null!;

    public DateTime DocDate { get; set; }

    public string UnrealizedGainAccount { get; set; } = null!;

    public string UnrealizedLossAccount { get; set; } = null!;

    public string GainLossJournalType { get; set; } = null!;

    public string? Description { get; set; }

    public string? Note { get; set; }

    public decimal TotalGainLoss { get; set; }

    public long? Jekey { get; set; }

    public short? PrintCount { get; set; }

    public long RefCount { get; set; }

    public DateTime CreatedTimeStamp { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public string UseSingleGainLossAccount { get; set; } = null!;

    public long? GltrxId { get; set; }

    public virtual Journal GainLossJournalTypeNavigation { get; set; } = null!;

    public virtual Glmast UnrealizedGainAccountNavigation { get; set; } = null!;

    public virtual Glmast UnrealizedLossAccountNavigation { get; set; } = null!;
}
