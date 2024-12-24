using System.ComponentModel.DataAnnotations;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Const;

namespace ViteSales.ERP.SDK.Internal.Core.Entities;

public class PackageAuthorsInternal
{
    [PrimaryKey]
    [BindDataType(FieldTypes.Guid)]
    public Guid Id { get; set; }
    
    [Required]
    [UniqueKey]
    [BindDataType(FieldTypes.Text)]
    public required string Name { get; set; }
    
    [BindDataType(FieldTypes.Email)]
    public string? Email { get; set; }
    
    [BindDataType(FieldTypes.Text)]
    public string? Company { get; set; }
    
    [BindDataType(FieldTypes.Url)]
    public string? Website { get; set; }
    
    [BindDataType(FieldTypes.Phone)]
    public string? Phone { get; set; }
}