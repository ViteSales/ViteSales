namespace ViteSales.ERP.SDK.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ModuleNameAttribute(string moduleName) : Attribute
{
    public string ModuleName { get; } = moduleName;
}