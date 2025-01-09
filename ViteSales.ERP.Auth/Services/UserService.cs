using Auth0.ManagementApi;
using Auth0.ManagementApi.Paging;
using Microsoft.Extensions.Options;
using ViteSales.ERP.Auth.Interfaces;
using ViteSales.ERP.Auth.Models;
using ViteSales.ERP.Shared.Models;

namespace ViteSales.ERP.Auth.Services;

public class UserService(IOptions<AuthSecrets> secrets, IAccessToken accessToken): IUser
{
    public async Task<IList<OrganizationInfo>?> GetAllOrganizations(string userId)
    {
        var mgmt = GetManagementClient();
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

    public async Task<UserInfo?> GetUserInfo(string userId)
    {
        var mgmt = GetManagementClient();
        var user = await mgmt.Users.GetAsync(userId);
        if (user is not null)
        {
            return new UserInfo
            {
                Id = userId,
                CreatedAt = user.CreatedAt,
                LastLoggedIn = user.LastLogin,
                LastPasswordChanged = user.LastPasswordReset,
                LastUpdated = user.UpdatedAt,
            };
        }
        return null;
    }
    
    private ManagementApiClient GetManagementClient()
    {
        ArgumentNullException.ThrowIfNull(secrets.Value);
        var token = accessToken.Get();
        return new ManagementApiClient(token, new Uri(secrets.Value.AuthorityUrl));
    }
}