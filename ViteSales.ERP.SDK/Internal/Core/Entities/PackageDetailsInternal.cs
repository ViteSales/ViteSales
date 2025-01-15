using System.ComponentModel.DataAnnotations;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Const;

namespace ViteSales.ERP.SDK.Internal.Core.Entities;

public class PackageDetailsInternal
{
    [PrimaryKey]
    [FieldDataType(FieldTypes.Guid)]
    public Guid Id { get; set; }
    
    [Required]
    [FieldDataType(FieldTypes.Guid)]
    public Guid PackageId { get; set; }
    
    [Required]
    [UniqueKey]
    [FieldDataType(FieldTypes.Text)]
    public required string Name { get; set; }
    
    [Required]
    [FieldDataType(FieldTypes.Text)]
    public required string ServiceId { get; set; }
    
    [Required]
    [FieldDataType(FieldTypes.Json)]
    public required object Entities { get; set; }

    [Required]
    [FieldDataType(FieldTypes.Json)]
    public object Others { get; set; }
}