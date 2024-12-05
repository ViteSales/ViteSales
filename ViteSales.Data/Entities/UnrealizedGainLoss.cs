namespace ViteSales.Data.Entities;

public partial class UnrealizedGainLoss
{
    public long UnrealizedGainLossKey { get; set; }

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

    public string? GlgainAccount { get; set; }

    public string? GllossAccount { get; set; }

    public long? Jekey2 { get; set; }

    public string? DocNo2 { get; set; }

    public string ReverseGlposting { get; set; } = null!;

    public long? GltrxId { get; set; }

    public virtual Journal GainLossJournalTypeNavigation { get; set; } = null!;

    public virtual Glmast? GlgainAccountNavigation { get; set; }

    public virtual Glmast? GllossAccountNavigation { get; set; }

    public virtual Glmast UnrealizedGainAccountNavigation { get; set; } = null!;

    public virtual Glmast UnrealizedLossAccountNavigation { get; set; } = null!;
}
