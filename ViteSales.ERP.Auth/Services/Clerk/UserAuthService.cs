using Clerk.BackendAPI;
using Clerk.BackendAPI.Models.Operations;
using Clerk.BackendAPI.Models.Errors;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ViteSales.ERP.Auth.Interfaces;
using ViteSales.ERP.Auth.Models;
using ViteSales.ERP.Shared.Models;

namespace ViteSales.ERP.Auth.Services.Clerk;

public class UserAuthService(IOptions<AuthSecrets> secrets, IOrganizationAuthService organizationAuthService, ILogger<UserAuthService> _logger): IUserAuthService
{
    public async Task<bool> DeleteUserAsync(string email)
    {
        var client = GetManagementClient();
        try
        {
            var users = await client.Users.ListAsync(new GetUserListRequest
            {
                ExternalId = [email],
            });
            if (users.UserList == null)
                throw new Exception("User not found");
            var user = users.UserList.First();
            var userId = user.Id!;
            await client.Users.DeleteAsync(userId);
            await client.EmailAddresses.DeleteAsync(user.EmailAddresses!.Where(item=>item.EmailAddressValue == email).Select(item=>item.Id!).First());
            return true;
        }
        catch (ClerkErrors ex)
        {
            throw new Exception(string.Join("; ", ex.Errors.Select(err => err.LongMessage)));
        }
    }

    public async Task<string?> GetUserIdAsync(string email)
    {
        var client = GetManagementClient();
        try
        {
            var users = await client.Users.ListAsync(new GetUserListRequest
            {
                ExternalId = [email],
            });
            if (users.UserList == null)
                return null;
            foreach (var user in users.UserList)
            {
                if (user.EmailAddresses == null)
                    continue;
                var id = user.EmailAddresses!.Where(item => item.EmailAddressValue == email).Select(item => item.Id!).First();
                return id;
            }
            return null;
        }
        catch (ClerkErrors ex)
        {
            throw new Exception(string.Join("; ", ex.Errors.Select(err => err.LongMessage)));
        }
    }

    public async Task<IList<OrganizationInfo>> GetAllOrganizationsAsync(string email)
    {
        var client = GetManagementClient();
        try
        {
            var users = await client.Users.ListAsync(new GetUserListRequest
            {
                EmailAddress = [email],
            });
            if (users.UserList == null)
                return new List<OrganizationInfo>();
            var organizations = new List<OrganizationInfo>();
            foreach (var user in users.UserList)
            {
                if (user.PublicMetadata == null ||
                    !user.PublicMetadata.TryGetValue("organization_id", out var value)) continue;
                var orgId = value.ToString();
                if (orgId == null) continue;
                var org = await organizationAuthService.GetOrganizationAsync(orgId);
                if (org != null)
                    organizations.Add(org);
            }
            return organizations;
        }
        catch (ClerkErrors ex)
        {
            throw new Exception(string.Join("; ", ex.Errors.Select(err => err.LongMessage)));
        }
    }

    public async Task<UserInfo?> GetUserInfoAsync(string email)
    {
        var client = GetManagementClient();
        try
        {
            var users = await client.Users.ListAsync(new GetUserListRequest
            {
                ExternalId = [email],
            });
            if (users.UserList == null)
                return null;
            var user = users.UserList.First();
            return new UserInfo
            {
                Id = user.Id!,
                CreatedAt = DateTimeOffset.FromUnixTimeSeconds(user.CreatedAt!.Value).DateTime,
                LastLoggedIn = DateTime.Now,
                LastPasswordChanged = DateTime.Now,
                LastUpdated = DateTimeOffset.FromUnixTimeSeconds(user.UpdatedAt!.Value).DateTime,
            };
        }
        catch (ClerkErrors ex)
        {
            throw new Exception(string.Join("; ", ex.Errors.Select(err => err.LongMessage)));
        }
    }

    public async Task<bool> IsEmailVerifiedAsync(string email)
    {
        var client = GetManagementClient();
        try
        {
            var users = await client.Users.ListAsync(new GetUserListRequest
            {
                ExternalId = [email],
            });
            if (users.UserList == null)
                throw new Exception("User not found");
            var user = users.UserList.First();
            if (user.EmailAddresses == null)
                return false;
            foreach (var address in user.EmailAddresses)
            {
                if (address.Verification == null)
                    return false;
                if (address.Verification.Type.Value == "email_verified")
                    return true;
            }
            return false;
        }
        catch (ClerkErrors ex)
        {
            throw new Exception(string.Join("; ", ex.Errors.Select(err => err.LongMessage)));
        }
    }

    public async Task<string> CreateUserAsync(CreateUser user)
    {
        var client = GetManagementClient();
        try
        {
            var created = await client.Users.CreateAsync(new CreateUserRequestBody
            {
                ExternalId = user.Email, 
                CreateOrganizationsLimit = 1,
                FirstName = user.FullName,
                EmailAddress = [user.Email],
                CreateOrganizationEnabled = user?.IsAdmin ?? false,
                Password = user!.Password,
                DeleteSelfEnabled = false,
            });
            return created.User!.Id!;
        }
        catch (ClerkErrors ex)
        {
            throw new Exception(string.Join("; ", ex.Errors.Select(err => err.LongMessage)));
        }
    }

    public async Task<bool> IsUserExistAsync(string email)
    {
        var client = GetManagementClient();
        try
        {
            var users = await client.Users.ListAsync(new GetUserListRequest
            {
                ExternalId = [email]
            });
            if (users.UserList == null)
                return false;
            return users.UserList.Count > 0;
        }
        catch (ClerkErrors ex)
        {
            throw new Exception(string.Join("; ", ex.Errors.Select(err => err.LongMessage)));
        }
    }
    
    private ClerkBackendApi GetManagementClient()
    {
        ArgumentNullException.ThrowIfNull(secrets.Value);
        return new ClerkBackendApi(secrets.Value.ClientSecret);
    }
}