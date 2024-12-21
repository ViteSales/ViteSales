namespace ViteSales.ERP.SDK.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ErrorMessageAttribute(string message): Attribute
{
    public string Message { get; set; } = message;
}