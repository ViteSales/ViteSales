using ViteSales.ERP.SDK.Const;

namespace ViteSales.ERP.SDK.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class DetailsViewTypeAttribute(DetailsViewTypes type): Attribute
{
    public DetailsViewTypes Type { get; set; } = type;
}