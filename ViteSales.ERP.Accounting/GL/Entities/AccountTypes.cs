using System.ComponentModel.DataAnnotations;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Const;

namespace ViteSales.ERP.Accounting.GL.Entities;

[Display(Name = "Chart of Accounts")]
public class AccountTypes
{
    [PrimaryKey]
    [BindDataType(FieldTypes.Guid)]
    [RelationalMapping("AccountLedgerEntry","Id","AccountId")]
    public Guid Id { get; set; }
    
    [Required]
    [UniqueKey]
    [BindDataType(FieldTypes.Text)]
    [Display(Name = "Account Name")]
    public required string AccountName { get; set; }
    
    [Required]
    [BindDataType(FieldTypes.SmallText)]
    public required string AccountRootType { get; set; }
    
    [Required]
    [BindDataType(FieldTypes.SmallText)]
    public required string ParentAccount { get; set; }
    
    [Required]
    [UniqueKey]
    [BindDataType(FieldTypes.MultiSelect)]
    [BindData("GetAccountTypes")]
    [Display(Name = "Account Type")]
    public required string AccountType { get; set; }
    
    [Required]
    [BindDataType(FieldTypes.Boolean)]
    public required bool IsGroup { get; set; }
}