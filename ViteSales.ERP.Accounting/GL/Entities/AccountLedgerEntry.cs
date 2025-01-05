using System.ComponentModel.DataAnnotations;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Const;

namespace ViteSales.ERP.Accounting.GL.Entities;

[MqStream]
public class AccountLedgerEntry
{
    [PrimaryKey]
    [FieldDataType(FieldTypes.Guid)]
    public Guid Id { get; set; }
    
    [Required]
    [FieldDataType(FieldTypes.Date)]
    public DateTime EntryDate { get; set; }
    
    [Required]
    [FieldDataType(FieldTypes.Guid)]
    public required string AccountId { get; set; }
    
    [FieldDataType(FieldTypes.ReadableId)]
    public string Code { get; set; }
    
    [FieldDataType(FieldTypes.MultiSelect)]
    public string Type { get; set; }
    
    [FieldDataType(FieldTypes.Currency)]
    public decimal Debit { get; set; }
    
    [FieldDataType(FieldTypes.Currency)]
    public decimal Credit { get; set; }
    
    [FieldDataType(FieldTypes.MultiSelect)]
    public string ReferenceType { get; set; }
    
    [FieldDataType(FieldTypes.ReadableId)]
    public string CreatedBy { get; set; }
    
    [FieldDataType(FieldTypes.ReadableId)]
    public string UpdatedBy { get; set; }
    
    [FieldDataType(FieldTypes.DateTime)]
    public DateTime CreatedAt { get; set; }
    
    [FieldDataType(FieldTypes.DateTime)]
    public DateTime UpdatedAt { get; set; }
}