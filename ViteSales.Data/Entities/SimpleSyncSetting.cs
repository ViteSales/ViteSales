namespace ViteSales.Data.Entities;

public partial class SimpleSyncSetting
{
    public long SettingKey { get; set; }

    public string? Server { get; set; }

    public string? Password { get; set; }

    public string IsDefaultPassword { get; set; } = null!;

    public string? Dbname { get; set; }

    public string? LoginUserId { get; set; }

    public string? LoginPassword { get; set; }

    public string IsCopyPriceBook { get; set; } = null!;

    public string IsCopyOpening { get; set; } = null!;

    public string IsCopyQtfromServer { get; set; } = null!;

    public string IsCopySofromServer { get; set; } = null!;

    public string IsCopyDofromServer { get; set; } = null!;

    public string IsCopyIvfromServer { get; set; } = null!;

    public string IsCopyCsfromServer { get; set; } = null!;

    public string IsExportQttoServer { get; set; } = null!;

    public string IsExportSotoServer { get; set; } = null!;

    public string IsExportDotoServer { get; set; } = null!;

    public string IsExportIvtoServer { get; set; } = null!;

    public string IsExportCstoServer { get; set; } = null!;

    public string? SalesAgentFilterCriteria { get; set; }
}
