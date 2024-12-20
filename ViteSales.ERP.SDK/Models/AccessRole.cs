using ViteSales.ERP.SDK.Const;

namespace ViteSales.ERP.SDK.Models;

public class AccessRole
{
    public AccessTypes AccessType { get; set; }
    public UserTypes UserType { get; set; }
}