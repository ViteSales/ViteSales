using ViteSales.ERP.Auth.Models;

namespace ViteSales.ERP.Auth.Interfaces;

public interface IUser
{
    Task<IList<OrganizationInfo>?> GetAllOrganizationsAsync(string userId);
    Task<UserInfo?> GetUserInfoAsync(string userId);
}