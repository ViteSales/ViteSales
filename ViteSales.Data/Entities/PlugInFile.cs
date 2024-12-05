namespace ViteSales.Data.Entities;

public partial class PlugInFile
{
    public Guid Guid { get; set; }

    public string FileName { get; set; } = null!;

    public DateTime? CreationTimeUtc { get; set; }

    public DateTime? LastAccessTimeUtc { get; set; }

    public DateTime? LastWriteTimeUtc { get; set; }

    public string ExecuteAfterExtracted { get; set; } = null!;

    public Guid? MsiGuid { get; set; }

    public byte[]? FileImage { get; set; }

    public virtual PlugIn Gu { get; set; } = null!;
}
