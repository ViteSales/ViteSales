using ViteSales.ERP.Auth.Interfaces;
using ViteSales.ERP.Auth.Models;

namespace ViteSales.ERP.Auth.Services.Kinde;

public class UserService: IUser
{
    public Task<IList<OrganizationInfo>?> GetAllOrganizations(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<UserInfo?> GetUserInfo(string userId)
    {
        throw new NotImplementedException();
    }
}