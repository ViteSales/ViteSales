namespace ViteSales.Data.Entities;

public partial class Report
{
    public long AutoKey { get; set; }

    public string ReportName { get; set; } = null!;

    public string? ReportType { get; set; }

    public byte[]? ReportTemplate { get; set; }

    public Guid Guid { get; set; }
}
