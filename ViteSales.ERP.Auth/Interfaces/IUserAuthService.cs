using ViteSales.ERP.Auth.Models;

namespace ViteSales.ERP.Auth.Interfaces;

public interface IUserAuthService
{
    Task<bool> DeleteUserAsync(string email);
    Task<string?> GetUserIdAsync(string email);
    Task<IList<OrganizationInfo>> GetAllOrganizationsAsync(string email);
    Task<UserInfo?> GetUserInfoAsync(string email);
    Task<bool> IsEmailVerifiedAsync(string email);
    Task<string> CreateUserAsync(CreateUser user);
    Task<bool> IsUserExistAsync(string email);
}