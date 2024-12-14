using System.ComponentModel.DataAnnotations;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Const;

namespace ViteSales.Test.SamplePackage.Entities;

[Display(Name = "Sales Invoice Items")]
[Description("Items of a sales invoice")]
public class InvoiceItems
{
    [PrimaryKey]
    public Guid Id { get; set; }
    
    [Required]
    public Guid InvoiceId { get; set; }
    
    [Required]
    [Display(Name = "Item Code")]
    [BindDataType(FieldTypes.ReadableId)]
    public string ItemCode { get; set; }
}