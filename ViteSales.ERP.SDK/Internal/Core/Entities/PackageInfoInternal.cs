using System.ComponentModel.DataAnnotations;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Const;

namespace ViteSales.ERP.SDK.Internal.Core.Entities;

[Serializable]
public class PackageInfoInternal
{
    [PrimaryKey]
    [BindDataType(FieldTypes.Guid)]
    public Guid Id { get; set; }
    
    [Required]
    [UniqueKey]
    [BindDataType(FieldTypes.Text)]
    public required string Name { get; set; }
    
    [Required]
    [BindDataType(FieldTypes.Guid)]
    public required Guid AuthorId { get; set; }
    
    [Required]
    [BindDataType(FieldTypes.SmallText)]
    public required string Version { get; set; }
    
    [BindDataType(FieldTypes.Text)]
    public string? License { get; set; }
    
    [RelationalMapping("PackageDetailsInternal", "Id", "PackageId")]
    public List<PackageDetailsInternal> Modules { get; set; } = [];
}