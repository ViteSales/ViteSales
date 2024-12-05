namespace ViteSales.Data.Entities;

public partial class RecurrenceDtl
{
    public long AutoKey { get; set; }

    public long RecurrenceKey { get; set; }

    public DateTime RecurrenceDate { get; set; }

    public string? AccNo { get; set; }

    public string? Status { get; set; }

    public long? SourceDocKey { get; set; }

    public string? SourceDocNo { get; set; }

    public virtual Glmast? AccNoNavigation { get; set; }
}
