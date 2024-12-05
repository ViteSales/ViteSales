namespace ViteSales.Data.Entities;

public partial class DefaultReport
{
    public long AutoKey { get; set; }

    public string ReportType { get; set; } = null!;

    public string ReportName { get; set; } = null!;
}
