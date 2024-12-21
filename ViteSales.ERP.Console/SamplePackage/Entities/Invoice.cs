using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Const;

namespace ViteSales.ERP.Console.SamplePackage.Entities;

[Display(Name = "Sales Invoice")]
[Description("Create a new Sales Invoice")]
public class Invoice
{
    [PrimaryKey]
    [BindDataType(FieldTypes.Guid)]
    public Guid Id { get; set; }
    
    [Required]
    [Display(Name = "Document No.")]
    [BindDataType(FieldTypes.ReadableId)]
    [IndexColumn("ASC")]
    [UniqueKey]
    public string DocNo { get; set; }
    
    [Required]
    [BindDataType(FieldTypes.ReadableId)]
    public string DocNoSet { get; set; }
    
    [Required]
    [Display(Name = "Debtor Code")]
    [BindData(nameof(GetDebtors))]
    [BindDataType(FieldTypes.Char)]
    [Editable(false)]
    public string DebtorCode { get; set; }
    
    [RelationalMapping("InvoiceItems", "Id", "InvoiceId")]
    public List<InvoiceItems> Items { get; set; } = [];

    public List<object> GetDebtors()
    {
        return new List<object>();
    }
}