using System.ComponentModel.DataAnnotations;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Const;

namespace ViteSales.ERP.Accounting.GL.Entities;

[Display(Name = "Fiscal Years")]
[MqStream]
public class FiscalYears
{
    [PrimaryKey]
    [BindDataType(FieldTypes.Guid)]
    public Guid Id { get; set; }
    
    [Required]
    [BindDataType(FieldTypes.Text)]
    [Display(Name = "Fiscal Year Name")]
    [UniqueKey]
    public required string Name { get; set; }
    
    [Required]
    [BindDataType(FieldTypes.Date)]
    [Display(Name = "Start Date")]
    public DateTime StartDate { get; set; }
    
    [Required]
    [BindDataType(FieldTypes.Date)]
    [Display(Name = "End Date")]
    public DateTime EndDate { get; set; }
    
    [Required]
    [BindDataType(FieldTypes.Boolean)]
    [Display(Name = "Is Active")]
    public required bool IsActive { get; set; }
}