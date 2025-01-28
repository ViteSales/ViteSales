using Clerk.BackendAPI;
using Clerk.BackendAPI.Models.Errors;
using Clerk.BackendAPI.Models.Operations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ViteSales.ERP.Auth.Interfaces;
using ViteSales.ERP.Auth.Models;
using ViteSales.ERP.Shared.Extensions;
using ViteSales.ERP.Shared.Models;

namespace ViteSales.ERP.Auth.Services.Clerk;

public class OrganizationAuthService(IOptions<AuthSecrets> secrets, ILogger<OrganizationAuthService> _logger): IOrganizationAuthService
{
    public async Task<string> CreateAsync(CreateOrganization organization, string userId)
    {
        try
        {
            var client = GetManagementClient();
            var user = await client.Users.GetAsync(userId);
            if (user.User == null)
                throw new InvalidOperationException("Could not get user");
            if (user.User.PublicMetadata != null && user.User.PublicMetadata.ContainsKey("organization_id"))
                throw new InvalidOperationException("User already has an organization");
            var org = await client.Organizations.CreateAsync(new CreateOrganizationRequestBody
            {
                Name = organization.Name,
                Slug = organization.Name.ToSlug()
            });
            if (org.Organization == null)
                throw new InvalidOperationException("Could not create organization");
            try
            {
                await client.Users.UpdateAsync(userId, new UpdateUserRequestBody
                {
                    PublicMetadata = new Dictionary<string, object>
                    {
                        { "organization_id", org.Organization.Id }
                    }
                });
                await client.OrganizationMemberships.CreateAsync(org.Organization.Id,
                    new CreateOrganizationMembershipRequestBody
                    {
                        Role = "org:admin",
                        UserId = userId
                    });
            }
            catch (ClerkErrors ex)
            {
                await client.Organizations.DeleteAsync(org.Organization.Id);
                throw new Exception(string.Join("; ", ex.Errors.Select(err => err.LongMessage)));
            }
            return org.Organization!.Id;
        }
        catch (ClerkErrors ex)
        {
            throw new Exception(string.Join("; ", ex.Errors.Select(err => err.LongMessage)));
        }
    }

    public Task DeleteAsync(string id)
    {
        try
        {
            var client = GetManagementClient();
            return client.Organizations.DeleteAsync(id.ToSlug());
        }
        catch (ClerkErrors ex)
        {
            throw new Exception(string.Join("; ", ex.Errors.Select(err => err.LongMessage)));
        }
    }

    public async Task<InvitationInfo> CreateInvitationAsync(string orgId, string inviterId, string invitee, string role)
    {
        try
        {
            var client = GetManagementClient();
            var invitation = await client.Invitations.CreateAsync(new CreateInvitationRequestBody
            {
                EmailAddress = invitee,
                IgnoreExisting = true,
                Notify = true,
                RedirectUrl = secrets.Value.RedirectUri
            });
            if (invitation.Invitation == null)
                throw new InvalidOperationException("Could not create invitation");
            return new InvitationInfo
            {
                Id = invitation.Invitation!.Id,
                OrganizationId = orgId,
                Inviter = inviterId,
                Invitee = invitee,
                Roles = new List<string> { role },
                CreatedAt = DateTimeOffset.FromUnixTimeSeconds(invitation.Invitation.CreatedAt).DateTime,
                ExpiresAt = DateTime.Now.AddDays(30),
                InvitationUrl = invitation.Invitation.Url!
            };
        }
        catch (ClerkErrors ex)
        {
            throw new Exception(string.Join("; ", ex.Errors.Select(err => err.LongMessage)));
        }
    }

    public async Task<IList<InvitationInfo>> GetInvitationsAsync(string orgId)
    {
        try
        {
            var client = GetManagementClient();
            var invitations = await client.Invitations.ListAsync(200);
            if (invitations.InvitationList == null)
                return new List<InvitationInfo>();
            return invitations.InvitationList.Select(item => new InvitationInfo
            {
                Id = item.Id,
                OrganizationId = orgId,
                Inviter = string.Empty,
                Invitee = string.Empty,
                Roles = new List<string>(),
                CreatedAt = DateTimeOffset.FromUnixTimeSeconds(item.CreatedAt).DateTime,
                ExpiresAt = DateTime.Now.AddDays(30),
                InvitationUrl = item.Url!
            }).ToList();
        }
        catch (ClerkErrors ex)
        {
            throw new Exception(string.Join("; ", ex.Errors.Select(err => err.LongMessage)));
        }
    }

    public async Task<IList<MemberInfo>> GetMembersAsync(string orgId)
    {
        try
        {
            var client = GetManagementClient();
            var members = await client.OrganizationMemberships.ListAsync(orgId.ToSlug());
            if (members.OrganizationMemberships == null)
                return new List<MemberInfo>();
            return members.OrganizationMemberships.Data.Select(item => new MemberInfo
            {
                UserId = item.Id!,
                Name = $"{item.PublicUserData!.FirstName!} {item.PublicUserData!.LastName!}",
                Email = item.PublicUserData.UserId!
            }).ToList();
        }
        catch (ClerkErrors ex)
        {
            throw new Exception(string.Join("; ", ex.Errors.Select(err => err.LongMessage)));
        }
    }

    public async Task<bool> IsOrganizationExistAsync(string orgName)
    {
        try
        {
            var org = await GetOrganizationAsync(orgName);
            return org != null;
        }
        catch (ClerkErrors ex)
        {
            throw new Exception(string.Join("; ", ex.Errors.Select(err => err.LongMessage)));
        }
    }

    public async Task AddMemberAsync(string orgId, string userId, IList<string> roles)
    {
        try
        {
            var client = GetManagementClient();
            var org = await GetOrganizationAsync(orgId);
            if (org == null)
                throw new InvalidOperationException("Organization does not exist");
            await client.OrganizationMemberships.CreateAsync(org.Id!, new CreateOrganizationMembershipRequestBody
            {
                Role = roles.Count > 0 ? roles[0] : string.Empty,
                UserId = userId
            });
        }
        catch (ClerkErrors ex)
        {
            throw new Exception(string.Join("; ", ex.Errors.Select(err => err.LongMessage)));
        }
    }

    public async Task RemoveMemberAsync(string orgId, string userId)
    {
        try
        {
            var client = GetManagementClient();
            var org = await GetOrganizationAsync(orgId);
            if (org == null)
                throw new InvalidOperationException("Organization does not exist");
            await client.OrganizationMemberships.DeleteAsync(org.Id!, userId);
        }
        catch (ClerkErrors ex)
        {
            throw new Exception(string.Join("; ", ex.Errors.Select(err => err.LongMessage)));
        }
    }

    public async Task UpdateRolesAsync(string orgId, string userId, IList<string> roles)
    {
        try
        {
            var client = GetManagementClient();
            var org = await GetOrganizationAsync(orgId);
            if (org == null)
                throw new InvalidOperationException("Organization does not exist");
            await client.OrganizationMemberships.UpdateAsync(org.Id!, userId, new UpdateOrganizationMembershipRequestBody
            {
                Role = roles.Count > 0 ? roles[0] : string.Empty
            });
        }
        catch (ClerkErrors ex)
        {
            throw new Exception(string.Join("; ", ex.Errors.Select(err => err.LongMessage)));
        }
    }

    public async Task UpdateOrganizationAsync(UpdateOrganization info)
    {
        try
        {
            var client = GetManagementClient();
            await client.Organizations.UpdateAsync(info.OrganizationId!, new UpdateOrganizationRequestBody
            {
                Name = info.Name,
                Slug = info.Name.ToSlug()
            });
        }
        catch (ClerkErrors ex)
        {
            throw new Exception(string.Join("; ", ex.Errors.Select(err => err.LongMessage)));
        }
    }

    public async Task DeleteRolesAsync(string orgId, string userId, IList<string> roles)
    {
        try
        {
            var client = GetManagementClient();
            var org = await GetOrganizationAsync(orgId);
            if (org == null)
                throw new InvalidOperationException("Organization does not exist");
            await client.OrganizationMemberships.UpdateAsync(org.Id!, userId, new UpdateOrganizationMembershipRequestBody
            {
                Role = string.Empty
            });
        }
        catch (ClerkErrors ex)
        {
            throw new Exception(string.Join("; ", ex.Errors.Select(err => err.LongMessage)));
        }
    }

    public async Task<OrganizationInfo?> GetOrganizationAsync(string orgId)
    {
        try
        {
            var client = GetManagementClient();
            var list = await client.Organizations.ListAsync();
            if (list.Organizations != null)
                return null;
            foreach (var organization in list.Organizations!.Data.Where(organization => organization.Slug == orgId.ToSlug()))
            {
                return new OrganizationInfo
                {
                    Id = organization.Id,
                    DisplayName = organization.Name,
                    LogoUrl = "",
                    Metadata = organization.PublicMetadata,
                    Name = organization.Name
                };
            }
            return null;
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