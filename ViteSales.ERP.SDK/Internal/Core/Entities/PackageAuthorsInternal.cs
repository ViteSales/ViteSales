using System.ComponentModel.DataAnnotations;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Const;

namespace ViteSales.ERP.SDK.Internal.Core.Entities;

public class PackageAuthorsInternal
{
    [PrimaryKey]
    [FieldDataType(FieldTypes.Guid)]
    public Guid Id { get; set; }
    
    [Required]
    [UniqueKey]
    [FieldDataType(FieldTypes.Text)]
    public required string Name { get; set; }
    
    [FieldDataType(FieldTypes.Email)]
    public string? Email { get; set; }
    
    [FieldDataType(FieldTypes.Text)]
    public string? Company { get; set; }
    
    [FieldDataType(FieldTypes.Url)]
    public string? Website { get; set; }
    
    [FieldDataType(FieldTypes.Phone)]
    public string? Phone { get; set; }
}