namespace ViteSales.Data.Entities;

public partial class FiscalYear
{
    public long AutoKey { get; set; }

    public string FiscalYearName { get; set; } = null!;

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string IsActive { get; set; } = null!;
}
