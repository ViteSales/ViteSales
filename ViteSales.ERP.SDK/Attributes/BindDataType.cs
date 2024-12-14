using ViteSales.ERP.SDK.Const;

namespace ViteSales.ERP.SDK.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class BindDataTypeAttribute(FieldTypes type): Attribute
{
    public FieldTypes Type { get; set; } = type;
}