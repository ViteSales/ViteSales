namespace ViteSales.Data.Entities;

public partial class DocReportSetting
{
    public string DocType { get; set; } = null!;

    public byte[]? Data { get; set; }
}
