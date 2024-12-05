namespace ViteSales.Data.Entities;

public partial class GstfilingReminder
{
    public long GstfilingReminderKey { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string? IsFirstTimeRemind { get; set; }

    public string? IsSecondTimeRemind { get; set; }
}
