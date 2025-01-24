using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Models.Users;
using Auth0.ManagementApi.Paging;
using Microsoft.Extensions.Options;
using ViteSales.ERP.Auth.Interfaces;
using ViteSales.ERP.Auth.Models.Auth0;
using ViteSales.ERP.Shared.Models;

namespace ViteSales.ERP.Auth.Services.Auth0;

public class UserService(IOptions<AuthSecrets> secrets, IAccessToken accessToken): IUser
{
    public async Task<string?> GetUserIdAsync(string email)
    {
        using var mgmt = GetManagementClient();
        var user = await mgmt.Users.GetUsersByEmailAsync(email);
        return user.Count == 0 ? null : user[0].UserId;
    }

    public async Task<IList<T>> GetAllOrganizationsAsync<T>(string email) where T : class
    {
        using var mgmt = GetManagementClient();
        var userId = await GetUserIdAsync(email);
        if (userId is null)
            throw new Exception("User not found");
        var organizations = await mgmt.Users.GetAllOrganizationsAsync(userId, new PaginationInfo(0, 100, true));
        if (organizations == null || organizations.Count == 0)
        {
            return null;
        }
        return organizations.Select(org => new OrganizationInfo
        {
            Id = org.Id,
            Name = org.Name,
            DisplayName = org.DisplayName,
            LogoUrl = org.Branding.LogoUrl,
            Metadata = org.Metadata
        })
        .ToList() as IList<T> ?? throw new InvalidOperationException();
    }

    public async Task<T> GetUserInfoAsync<T>(string email) where T : class
    {
        using var mgmt = GetManagementClient();
        var users = await mgmt.Users.GetUsersByEmailAsync(email);
        if (users.Count == 0)
            throw new Exception("User not found");
        var user = users[0];
        return new UserInfo
        {
            Id = email,
            CreatedAt = user.CreatedAt,
            LastLoggedIn = user.LastLogin,
            LastPasswordChanged = user.LastPasswordReset,
            LastUpdated = user.UpdatedAt,
        } as T ?? throw new InvalidOperationException();
    }

    public async Task<bool> IsEmailVerifiedAsync(string email)
    {
        using var mgmt = GetManagementClient();
        var user = await mgmt.Users.GetUsersByEmailAsync(email);
        if (user.Count == 0)
            return false;
        return user[0].EmailVerified.HasValue && user[0].EmailVerified!.Value;
    }

    public async Task<string> CreateUserAsync(CreateUser user)
    {
        using var mgmt = GetManagementClient();
        var createdUser = await mgmt.Users.CreateAsync(new UserCreateRequest
        {
            Connection = "con_XZzdbtDuxJVdAXtJ",
            FullName = user.FullName,
            Email = user.Email,
            VerifyEmail = true,
            Password = user.Password,
        });
        return createdUser.UserId;
    }

    private ManagementApiClient GetManagementClient()
    {
        ArgumentNullException.ThrowIfNull(secrets.Value);
        var token = accessToken.Get();
        return new ManagementApiClient(token, new Uri(secrets.Value.AuthorityUrl));
    }
}