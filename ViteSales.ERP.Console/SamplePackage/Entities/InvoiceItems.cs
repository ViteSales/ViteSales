using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Const;

namespace ViteSales.ERP.Console.SamplePackage.Entities;

[Display(Name = "Sales Invoice Items")]
[Description("Items of a sales invoice")]
public class InvoiceItems
{
    [PrimaryKey]
    [BindDataType(FieldTypes.Guid)]
    public Guid Id { get; set; }
    
    [Required]
    [BindDataType(FieldTypes.Guid)]
    public Guid InvoiceId { get; set; }
    
    [Required]
    [Display(Name = "Item Name")]
    [BindDataType(FieldTypes.Text)]
    [IndexColumn("ASC")]
    public string ItemName { get; set; }
    
    [Required]
    [Display(Name = "Item Code")]
    [BindDataType(FieldTypes.ReadableId)]
    public string ItemCode { get; set; }
}