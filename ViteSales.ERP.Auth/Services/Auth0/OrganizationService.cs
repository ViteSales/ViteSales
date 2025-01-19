using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ViteSales.ERP.Auth.Interfaces;
using ViteSales.ERP.Auth.Models;
using ViteSales.ERP.Shared.Extensions;
using ViteSales.ERP.Shared.Models;

namespace ViteSales.ERP.Auth.Services.Auth0;

public class OrganizationService(IOptions<AuthSecrets> secrets, IAccessToken accessToken, IUser userService, ILogger<OrganizationService> _logger): IOrganization
{
    public async Task<string> Create(CreateOrganization organization, string userId)
    {
        var mgmt = GetManagementClient();
        _logger.LogInformation("Creating organization with name: {OrganizationName}", organization.Name);
        var org = await mgmt.Organizations.CreateAsync(new OrganizationCreateRequest
        {
            Name = organization.Name.ToSlugUnique(),
            DisplayName = organization.Name,
            Branding = new OrganizationBranding
            {
                LogoUrl = organization.LogoUrl
            },
            Metadata = organization.Metadata
        });
        if (org == null)
        {
            _logger.LogError("Failed to create organization with name: {OrganizationName}", organization.Name);
            throw new Exception("Error creating organization");
        }
        _logger.LogInformation("Organization created successfully with ID: {OrganizationId}", org.Id);
        _logger.LogInformation("Assigning 'Admin' role to user with ID: {UserId}", userId);
        await mgmt.Organizations.AddMemberRolesAsync(org.Id, userId, new OrganizationAddMemberRolesRequest
        {
            Roles = new List<string> { "Admin" }
        });
        _logger.LogInformation("'Admin' role assigned successfully to user with ID: {UserId}", userId);
        return org.Id;
    }

    public async Task Delete(string id)
    {
        var mgmt = GetManagementClient();
        _logger.LogInformation("Deleting organization with ID: {OrganizationId}", id);
        await mgmt.Organizations.DeleteAsync(id);
        _logger.LogInformation("Organization with ID: {OrganizationId} deleted successfully", id);
    }

    public async Task<InvitationInfo> CreateInvitation(string orgId, string inviterId, string invitee, string role)
    {
        var mgmt = GetManagementClient();
        _logger.LogInformation("Fetching inviter information for user ID: {InviterId}", inviterId);
        var user = await mgmt.Users.GetAsync(inviterId);
        if (user == null)
        {
            _logger.LogError("Failed to fetch inviter information for user ID: {InviterId}", inviterId);
            throw new Exception("You are not authorized to create invitations. Try logging in again.");
        }
        _logger.LogInformation("Creating invitation for organization ID: {OrganizationId}, invitee: {InviteeEmail}, role: {Role}", orgId, invitee, role);
        var invitation = await mgmt.Organizations.CreateInvitationAsync(orgId, new OrganizationCreateInvitationRequest
        {
            ClientId = secrets.Value.ClientId,
            SendInvitationEmail = true,
            Inviter = new OrganizationInvitationInviter
            {
                Name = user.FullName,
            },
            Invitee = new OrganizationInvitationInvitee
            {
                Email = invitee,
            },
            Roles = new List<string> { role }
        });
        _logger.LogInformation("Invitation created successfully with ID: {InvitationId} for organization ID: {OrganizationId}", invitation.Id, orgId);
        return new InvitationInfo
        {
            InvitationUrl = invitation.InvitationUrl,
            ExpiresAt = invitation.ExpiresAt,
            CreatedAt = invitation.CreatedAt,
            Id = invitation.Id,
            Inviter = invitation.Inviter.Name,
            Invitee = invitation.Invitee.Email,
            OrganizationId = orgId,
            Roles = invitation.Roles
        };
    }

    public async Task<IList<InvitationInfo>?> GetInvitations(string orgId)
    {
        var mgmt = GetManagementClient();
        var invitations = await mgmt.Organizations.GetAllInvitationsAsync(orgId, new OrganizationGetAllInvitationsRequest
        {
            Sort = "created_at:-1",
            IncludeFields = true,
            Fields = "invitation_url,created_at,expires_at,organization_id,roles,id,inviter,invitee"
        }, new PaginationInfo(0, 500, true));
        if (invitations == null || invitations.Count == 0)
        {
            return null;
        }
        return invitations.Select(invitation => new InvitationInfo
            {
                InvitationUrl = invitation.InvitationUrl,
                ExpiresAt = invitation.ExpiresAt,
                CreatedAt = invitation.CreatedAt,
                Id = invitation.Id,
                Inviter = invitation.Inviter.Name,
                Invitee = invitation.Invitee.Email,
                OrganizationId = orgId,
                Roles = invitation.Roles
            })
            .ToList();
    }

    public async Task<IList<MemberInfo>?> GetMembers(string orgId)
    {
        var mgmt = GetManagementClient();
        var members = await mgmt.Organizations.GetAllMembersAsync(orgId, new PaginationInfo(0, 500, true));
        if (members == null || members.Count == 0)
        {
            return null;
        }
        return members.Select(async member =>
            {
                var user = await userService.GetUserInfo(member.UserId);
                return new MemberInfo
                {
                    UserId = member.UserId,
                    Email = member.Email,
                    PictureUrl = member.Picture,
                    Name = member.Name,
                    CreatedAt = user?.CreatedAt,
                    LastLoggedIn = user?.LastLoggedIn,
                    LastPasswordChanged = user?.LastPasswordChanged,
                    LastUpdated = user?.LastUpdated,
                    Roles = member.Roles.Select(r => new MemberRole
                    {
                        Id = r.Id,
                        Name = r.Name
                    }).ToList()
                };
            })
            .Select(x => x.Result)
            .ToList();
    }

    public async Task RemoveMember(string orgId, string userId)
    {
        var mgmt = GetManagementClient();
        _logger.LogInformation("Removing member with ID: {UserId} from organization ID: {OrganizationId}", userId, orgId);
        await mgmt.Organizations.DeleteMemberAsync(orgId, new OrganizationDeleteMembersRequest
        {
            Members = new List<string> { userId }
        });
        _logger.LogInformation("Successfully removed member with ID: {UserId} from organization ID: {OrganizationId}", userId, orgId);
    }

    public async Task UpdateRoles(string orgId, string userId, IList<string> roles)
    {
        var mgmt = GetManagementClient();
        _logger.LogInformation("Updating roles for member with ID: {UserId} in organization ID: {OrganizationId} with roles: {Roles}", 
            userId, orgId, string.Join(", ", roles));
        await mgmt.Organizations.AddMemberRolesAsync(orgId, userId, new OrganizationAddMemberRolesRequest
        {
            Roles = roles
        });
        _logger.LogInformation("Successfully updated roles for member with ID: {UserId} in organization ID: {OrganizationId}", userId, orgId);
    }

    public async Task DeleteRoles(string orgId, string userId, IList<string> roles)
    {
        var mgmt = GetManagementClient();
        _logger.LogInformation("Deleting roles for member with ID: {UserId} in organization ID: {OrganizationId} with roles: {Roles}", 
            userId, orgId, string.Join(", ", roles));
        await mgmt.Organizations.DeleteMemberRolesAsync(orgId, userId, new OrganizationDeleteMemberRolesRequest
        {
            Roles = roles
        });
        _logger.LogInformation("Successfully deleted roles for member with ID: {UserId} in organization ID: {OrganizationId}", userId, orgId);
    }

    public async Task<OrganizationInfo?> GetOrganization(string orgId)
    {
        var mgmt = GetManagementClient();
        var org = await mgmt.Organizations.GetAsync(orgId);
        if (org is not null)
        {
            return new OrganizationInfo
            {
                Id = org.Id,
                Name = org.Name,
                DisplayName = org.DisplayName,
                LogoUrl = org.Branding.LogoUrl,
                Metadata = org.Metadata
            };
        }
        return null;
    }

    public async Task UpdateOrganization(string orgId, KeyValuePair<string, object> keyValuePair)
    {
        var mgmt = GetManagementClient();
        await mgmt.Organizations.UpdateAsync(orgId, new OrganizationUpdateRequest
        {
            Metadata = keyValuePair
        });
    }

    private ManagementApiClient GetManagementClient()
    {
        ArgumentNullException.ThrowIfNull(secrets.Value);
        var token = accessToken.Get();
        return new ManagementApiClient(token, new Uri(secrets.Value.AuthorityUrl));
    }
}