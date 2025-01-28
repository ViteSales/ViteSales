using Auth0.Core.Exceptions;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Models.Users;
using Auth0.ManagementApi.Paging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ViteSales.ERP.Auth.Interfaces;
using ViteSales.ERP.Auth.Models;
using ViteSales.ERP.Shared.Models;

namespace ViteSales.ERP.Auth.Services.Auth0;

public class UserAuthService(IOptions<AuthSecrets> secrets, IAccessToken accessToken, ILogger<UserAuthService> _logger): IUserAuthService
{
    public Task<bool> DeleteUserAsync(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<string?> GetUserIdAsync(string email)
    {
        using var mgmt = GetManagementClient();
        var user = await mgmt.Users.GetUsersByEmailAsync(email);
        return user.Count == 0 ? null : user[0].UserId;
    }

    public async Task<IList<OrganizationInfo>> GetAllOrganizationsAsync(string email)
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
            .ToList();
    }

    public async Task<UserInfo?> GetUserInfoAsync(string email)
    {
        using var mgmt = GetManagementClient();
        var users = await mgmt.Users.GetUsersByEmailAsync(email);
        if (users.Count == 0)
            return null;
        var user = users[0];
        return new UserInfo
        {
            Id = email,
            CreatedAt = user.CreatedAt,
            LastLoggedIn = user.LastLogin,
            LastPasswordChanged = user.LastPasswordReset,
            LastUpdated = user.UpdatedAt,
        };
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

    public async Task<bool> IsUserExistAsync(string email)
    {
        using var mgmt = GetManagementClient();
        try
        {
            var user = await mgmt.Users.GetUsersByEmailAsync(email);
            if (user == null) return false;
            return user.Count > 0;
        }
        catch (ErrorApiException e)
        {
            _logger.LogError(e, "Error checking if user exists");
            throw;
        }
    }

    private ManagementApiClient GetManagementClient()
    {
        ArgumentNullException.ThrowIfNull(secrets.Value);
        var token = accessToken.Get().Replace("Bearer ", "");
        return new ManagementApiClient(token, new Uri(secrets.Value.Audience));
    }
}