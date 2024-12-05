namespace ViteSales.Data.Entities;

public partial class RemarkName
{
    public string DocType { get; set; } = null!;

    public string? Remark1Name { get; set; }

    public string? Remark2Name { get; set; }

    public string? Remark3Name { get; set; }

    public string? Remark4Name { get; set; }

    public string Remark1Mru { get; set; } = null!;

    public string Remark2Mru { get; set; } = null!;

    public string Remark3Mru { get; set; } = null!;

    public string Remark4Mru { get; set; } = null!;
}
