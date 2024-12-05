namespace ViteSales.Data.Entities;

public partial class ReportAttribute
{
    public string ReportName { get; set; } = null!;

    public string? DenyUsers { get; set; }

    public string? Attributes { get; set; }
}
