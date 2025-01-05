using System.ComponentModel.DataAnnotations;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Const;

namespace ViteSales.ERP.SDK.Internal.Core.Entities;

[Serializable]
public class AuditTrailInternal
{
    [PrimaryKey]
    [FieldDataType(FieldTypes.Guid)]
    public Guid Id { get; set; }
    
    [Required]
    [FieldDataType(FieldTypes.ShortCode)]
    public required string DataId { get; set; }
    
    [Required]
    [FieldDataType(FieldTypes.Text)]
    public required string Module { get; set; }
    
    [Required]
    [FieldDataType(FieldTypes.Text)]
    public required string Action { get; set; }
    
    [Required]
    [FieldDataType(FieldTypes.Json)]
    public required string Data { get; set; }
    
    [Required]
    [FieldDataType(FieldTypes.ShortCode)]
    public required string ActionBy { get; set; }
    
    [Required]
    [FieldDataType(FieldTypes.DateTime)]
    public required DateTime ActionAt { get; set; }
}