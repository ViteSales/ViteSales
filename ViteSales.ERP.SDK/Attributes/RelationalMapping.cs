namespace ViteSales.ERP.SDK.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class RelationalMappingAttribute(string table, string from, string to): Attribute
{
    public string FromTable => nameof(RelationalMappingAttribute);
    public string ToTable { get; set; } = table;
    public string FromColumn { get; set; } = from;
    public string ToColumn { get; set; } = to;
}