namespace ViteSales.Data.Entities;

public partial class PlugIn
{
    public Guid Guid { get; set; }

    public int Seq { get; set; }

    public string Name { get; set; } = null!;

    public string Version { get; set; } = null!;

    public string? Description { get; set; }

    public string? WhatsNew { get; set; }

    public string ExtractFiles { get; set; } = null!;

    public string? AssemblyFile { get; set; }

    public string? Signature { get; set; }

    public string? MinimumAccountingVersion { get; set; }

    public string? ScriptLanguage { get; set; }

    public string? InstallationScript { get; set; }

    public DateTime? CreatedTimeStamp { get; set; }

    public string? Manufacturer { get; set; }

    public string? ManufacturerUrl { get; set; }

    public string? Copyright { get; set; }

    public string? SalesPhone { get; set; }

    public string? SupportPhone { get; set; }

    public string? AllowSync { get; set; }

    public string? SupportLicenseKey { get; set; }

    public string? LicenseKey { get; set; }

    public string? Dbversion { get; set; }

    public virtual ICollection<PlugInFile> PlugInFiles { get; set; } = new List<PlugInFile>();
}
