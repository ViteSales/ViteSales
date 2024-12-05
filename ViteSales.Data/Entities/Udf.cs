namespace ViteSales.Data.Entities;

public partial class Udf
{
    public long AutoKey { get; set; }

    public string TableName { get; set; } = null!;

    public string FieldName { get; set; } = null!;

    public int Seq { get; set; }

    public string FieldType { get; set; } = null!;

    public string? Caption { get; set; }

    public string? Properties { get; set; }

    public Guid Guid { get; set; }
}
