using System.ComponentModel.DataAnnotations;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Const;

namespace ViteSales.ERP.Accounting.GL.Entities;

[Display(Name = "Chart of Accounts")]
[MasterViewType(MasterViewTypes.TreeView)]
[MqStream]
public class AccountTypes
{
    [PrimaryKey]
    [FieldDataType(FieldTypes.Guid)]
    [RelationalMapping("AccountLedgerEntry","Id","AccountId")]
    public Guid Id { get; set; }
    
    [Required]
    [UniqueKey]
    [FieldDataType(FieldTypes.Text)]
    [Display(Name = "Account Name")]
    public required string AccountName { get; set; }
    
    [Required]
    [UniqueKey]
    [FieldDataType(FieldTypes.SmallText)]
    public required string AccountRootType { get; set; }

    [FieldDataType(FieldTypes.SmallText)] 
    [UniqueKey]
    public string ParentAccount { get; set; } = string.Empty;
    
    [FieldDataType(FieldTypes.Select)]
    [FieldData("GetAccountTypes")]
    [Display(Name = "Account Type")]
    public string AccountType { get; set; } = string.Empty;
    
    [Required]
    [FieldDataType(FieldTypes.Boolean)]
    public required bool IsGroup { get; set; }
}