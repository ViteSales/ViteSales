using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Nodes;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Const;

namespace ViteSales.ERP.SDK.Internal.Core.Entities;

public class PackageDetailsInternal
{
    [PrimaryKey]
    [BindDataType(FieldTypes.Guid)]
    public Guid Id { get; set; }
    
    [Required]
    [BindDataType(FieldTypes.Guid)]
    public Guid PackageId { get; set; }
    
    [Required]
    [UniqueKey]
    [BindDataType(FieldTypes.Text)]
    public required string Name { get; set; }
    
    [Required]
    [BindDataType(FieldTypes.Text)]
    public required string ModuleId { get; set; }
    
    [Required]
    [BindDataType(FieldTypes.Json)]
    public required object Entities { get; set; }

    [Required]
    [BindDataType(FieldTypes.Json)]
    public object Others { get; set; }
}