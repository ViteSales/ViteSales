using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Const;

namespace ViteSales.Test.SamplePackage.Entities;

[Display(Name = "Sales Invoice")]
[Description("Create a new Sales Invoice")]
public class Invoice
{
    [PrimaryKey]
    public Guid Id { get; set; }
    
    [Required]
    [Display(Name = "Document No.")]
    [BindDataType(FieldTypes.ReadableId)]
    [IndexColumn("ASC")]
    [UniqueColumn]
    public string DocNo { get; set; }
    
    [Required]
    [BindDataType(FieldTypes.ReadableId)]
    [UniqueColumn]
    public string DocNoSet { get; set; }
    
    [Required]
    [Display(Name = "Debtor Code")]
    [BindData(nameof(GetDebtors))]
    [BindDataType(FieldTypes.Text)]
    [Editable(false)]
    public string DebtorCode { get; set; }
    
    [RelationalMapping("InvoiceItems", "Id", "InvoiceId")]
    [RelationalMapping("InvoiceItems", "DocNo", "DocNo")]
    public List<InvoiceItems> Items { get; set; } = new();

    public List<object> GetDebtors()
    {
        return new List<object>();
    }
}