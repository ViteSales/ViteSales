using ViteSales.ERP.Auth.Models;

namespace ViteSales.ERP.Auth.Interfaces;

public interface IUser
{
    Task<IList<OrganizationInfo>?> GetAllOrganizations(string userId);
    Task<UserInfo?> GetUserInfo(string userId);
}