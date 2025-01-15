namespace ViteSales.ERP.SDK.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class ModuleEntitiesAttribute(params Type[] entities) : Attribute
{
    public Type[] Entities { get; } = entities;
}