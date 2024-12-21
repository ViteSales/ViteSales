using System.ComponentModel.DataAnnotations;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Const;

namespace ViteSales.ERP.Accounting.GL.Entities;

public class AccountLedgerEntry
{
    [PrimaryKey]
    [BindDataType(FieldTypes.Guid)]
    public Guid Id { get; set; }
    
    [Required]
    [BindDataType(FieldTypes.Date)]
    public DateTime EntryDate { get; set; }
    
    [Required]
    [BindDataType(FieldTypes.ReadableId)]
    public required string AccountId { get; set; }
    
    [BindDataType(FieldTypes.ReadableId)]
    public string Code { get; set; }
    
    [BindDataType(FieldTypes.MultiSelect)]
    public string Type { get; set; }
    
    [BindDataType(FieldTypes.Currency)]
    public decimal Debit { get; set; }
    
    [BindDataType(FieldTypes.Currency)]
    public decimal Credit { get; set; }
    
    [BindDataType(FieldTypes.MultiSelect)]
    public string ReferenceType { get; set; }
    
    [BindDataType(FieldTypes.ReadableId)]
    public string CreatedBy { get; set; }
    
    [BindDataType(FieldTypes.ReadableId)]
    public string UpdatedBy { get; set; }
    
    [BindDataType(FieldTypes.DateTime)]
    public DateTime CreatedAt { get; set; }
    
    [BindDataType(FieldTypes.DateTime)]
    public DateTime UpdatedAt { get; set; }
}