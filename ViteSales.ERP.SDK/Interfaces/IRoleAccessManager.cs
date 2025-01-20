using ViteSales.ERP.SDK.Const;
using ViteSales.ERP.SDK.Models;
using ViteSales.ERP.Shared.Models;

namespace ViteSales.ERP.SDK.Interfaces;

public interface IRoleAccessManager
{
    Task<ConnectionConfig> GetAccessConfigAsync(string username);
    Task<bool> IsUserExistsAsync(string username);
    Task CreateUserAsync(string username);
    Task DropUserAsync(string username);
    Task GrantAccessAsync(string username, List<AccessTypes> roles, List<string> tables);
    Task RemoveAccessAsync(string username, List<AccessTypes> roles, List<string> tables);
}