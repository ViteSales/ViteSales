using ViteSales.ERP.SDK.Const;
using ViteSales.ERP.SDK.Models;
using ViteSales.ERP.Shared.Models;

namespace ViteSales.ERP.SDK.Interfaces;

public interface IRoleAccessManager
{
    Task<ConnectionConfig> GetAccessConfig(string username);
    Task<bool> IsUserExists(string username);
    Task CreateUser(string username);
    Task DropUser(string username);
    Task GrantAccess(string username, List<AccessTypes> roles, List<string> tables);
    Task RemoveAccess(string username, List<AccessTypes> roles, List<string> tables);
}