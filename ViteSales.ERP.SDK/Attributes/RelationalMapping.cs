using SqlKata;

namespace ViteSales.ERP.SDK.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class RelationalMappingAttribute(string table, string from, string to): IgnoreAttribute
{
    public string FromTable => nameof(RelationalMappingAttribute);
    public string ToTable { get; set; } = table;
    public string FromColumn { get; set; } = from;
    public string ToColumn { get; set; } = to;
}