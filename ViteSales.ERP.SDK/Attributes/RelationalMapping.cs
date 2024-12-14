namespace ViteSales.ERP.SDK.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class RelationalMappingAttribute(string table, string from, string to): Attribute
{
    public string Table { get; set; } = table;
    public string From { get; set; } = from;
    public string To { get; set; } = to;
}