namespace ViteSales.ERP.SDK.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class BindSubmitAttribute(Func<object> function): Attribute
{
    public Func<object> Function { get; set; } = function;
}