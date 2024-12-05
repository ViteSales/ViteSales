namespace ViteSales.Data.Entities;

public partial class Recurrence
{
    public long RecurrenceKey { get; set; }

    public string? RecurrenceName { get; set; }

    public DateTime StartDate { get; set; }

    public string EndType { get; set; } = null!;

    public int? NoOfOccurs { get; set; }

    public DateTime? EndDate { get; set; }

    public string? RecurrenceDateType { get; set; }

    public string? RecurrenceDay1 { get; set; }

    public string? RecurrenceDay2 { get; set; }

    public string? RecurrenceDay3 { get; set; }

    public string? RecurrenceDay4 { get; set; }

    public string? RecurrenceDay5 { get; set; }

    public string? RecurrenceDay6 { get; set; }

    public string? RecurrenceDay7 { get; set; }

    public int? RecurrenceParam1 { get; set; }

    public int? RecurrenceParam2 { get; set; }

    public string? IsParam2FromBack { get; set; }

    public string? IsActive { get; set; }

    public string AutoExecute { get; set; } = null!;

    public string? AlertUserId { get; set; }

    public string DocType { get; set; } = null!;

    public string? DocNoFormatName { get; set; }

    public byte[]? Data { get; set; }

    public DateTime LastModified { get; set; }

    public string LastModifiedUserId { get; set; } = null!;

    public DateTime CreatedTimeStamp { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public virtual User? AlertUser { get; set; }

    public virtual User CreatedUser { get; set; } = null!;

    public virtual User LastModifiedUser { get; set; } = null!;
}
