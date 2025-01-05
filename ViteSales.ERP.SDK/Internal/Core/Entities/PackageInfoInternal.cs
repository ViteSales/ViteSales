using System.ComponentModel.DataAnnotations;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Const;

namespace ViteSales.ERP.SDK.Internal.Core.Entities;

public class PackageInfoInternal
{
    [PrimaryKey]
    [FieldDataType(FieldTypes.Guid)]
    public Guid Id { get; set; }
    
    [Required]
    [UniqueKey]
    [FieldDataType(FieldTypes.Text)]
    public required string Name { get; set; }
    
    [Required]
    [FieldDataType(FieldTypes.Guid)]
    public required Guid AuthorId { get; set; }
    
    [Required]
    [FieldDataType(FieldTypes.SmallText)]
    public required string Version { get; set; }
    
    [FieldDataType(FieldTypes.Text)]
    public string? License { get; set; }
    
    [RelationalMapping("PackageDetailsInternal", "Id", "PackageId")]
    public List<PackageDetailsInternal> Modules { get; set; } = [];
}