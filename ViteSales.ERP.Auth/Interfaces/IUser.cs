using ViteSales.ERP.Auth.Models.Auth0;

namespace ViteSales.ERP.Auth.Interfaces;

public interface IUser
{
    Task<string?> GetUserIdAsync(string email);
    Task<IList<T>> GetAllOrganizationsAsync<T>(string email) where T : class;
    Task<T> GetUserInfoAsync<T>(string email) where T : class;
    Task<bool> IsEmailVerifiedAsync(string email);
    Task<string> CreateUserAsync(CreateUser user);
}