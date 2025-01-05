using ViteSales.ERP.SDK.Const;

namespace ViteSales.ERP.SDK.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class MasterViewTypeAttribute(MasterViewTypes type): Attribute
{
    public MasterViewTypes Type { get; set; } = type;
}