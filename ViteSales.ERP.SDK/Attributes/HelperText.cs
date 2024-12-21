namespace ViteSales.ERP.SDK.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class HelperTextAttribute(string text): Attribute
{
    public string Text { get; set; } = text;
}