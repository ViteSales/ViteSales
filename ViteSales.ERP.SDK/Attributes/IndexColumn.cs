namespace ViteSales.ERP.SDK.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class IndexColumnAttribute(string direction): Attribute
{
    public string Direction { get; set; } = direction;
}