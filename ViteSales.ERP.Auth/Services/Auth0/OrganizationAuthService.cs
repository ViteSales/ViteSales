using Auth0.Core.Exceptions;
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

public class OrganizationAuthService(IOptions<AuthSecrets> secrets, IAccessToken accessToken, IUserAuthService userAuthServiceService, ILogger<OrganizationAuthService> _logger): IOrganizationAuthService
{
    public async Task<string> CreateAsync(CreateOrganization organization, string userId)
    {
        ArgumentNullException.ThrowIfNull(organization);
        
        using var mgmt = GetManagementClient();
        _logger.LogInformation("Creating organization with name: {OrganizationName}", organization.Name);
        var org = await mgmt.Organizations.CreateAsync(new OrganizationCreateRequest
        {
            Name = organization.Name.ToSlug(),
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

    public async Task DeleteAsync(string id)
    {
        using var mgmt = GetManagementClient();
        _logger.LogInformation("Deleting organization with ID: {OrganizationId}", id);
        await mgmt.Organizations.DeleteAsync(id);
        _logger.LogInformation("Organization with ID: {OrganizationId} deleted successfully", id);
    }

    public async Task<InvitationInfo> CreateInvitationAsync(string orgId, string inviterId, string invitee, string role)
    {
        using var mgmt = GetManagementClient();
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

    public async Task<IList<InvitationInfo>> GetInvitationsAsync(string orgId)
    {
        using var mgmt = GetManagementClient();
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

    public async Task<IList<MemberInfo>> GetMembersAsync(string orgId)
    {
        using var mgmt = GetManagementClient();
        var members = await mgmt.Organizations.GetAllMembersAsync(orgId, new PaginationInfo(0, 500, true));
        if (members == null || members.Count == 0)
        {
            return new List<MemberInfo>();
        }

        return members.Select(async member =>
            {
                var user = await userAuthServiceService.GetUserInfoAsync(member.UserId);
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

    public async Task<bool> IsOrganizationExistAsync(string orgName)
    {
        using var mgmt = GetManagementClient();
        try
        {
            var org = await mgmt.Organizations.GetAsync(orgName.ToSlug());
            return org != null;
        }
        catch (ErrorApiException e)
        {
            _logger.LogError(e, "Error checking if organization exists");
            throw;
        }
    }

    public async Task AddMemberAsync(string orgId, string userId, IList<string> roles)
    {
        using var mgmt = GetManagementClient();
        _logger.LogInformation("Adding member with ID: {UserId} to organization ID: {OrganizationId}", userId, orgId);

        await mgmt.Organizations.AddMembersAsync(orgId, new OrganizationAddMembersRequest
        {
            Members = new List<string> { userId }
        });
        _logger.LogInformation("Successfully added member with ID: {UserId} to organization ID: {OrganizationId}", userId, orgId);

        _logger.LogInformation("Assigning roles: {Roles} to member with ID: {UserId} in organization ID: {OrganizationId}", 
            string.Join(", ", roles), userId, orgId);

        await mgmt.Organizations.AddMemberRolesAsync(orgId, userId, new OrganizationAddMemberRolesRequest
        {
            Roles = roles
        });
        _logger.LogInformation("Successfully assigned roles: {Roles} to member with ID: {UserId} in organization ID: {OrganizationId}",
            string.Join(", ", roles), userId, orgId);
    }

    public async Task RemoveMemberAsync(string orgId, string userId)
    {
        using var mgmt = GetManagementClient();
        _logger.LogInformation("Removing member with ID: {UserId} from organization ID: {OrganizationId}", userId, orgId);
        await mgmt.Organizations.DeleteMemberAsync(orgId, new OrganizationDeleteMembersRequest
        {
            Members = new List<string> { userId }
        });
        _logger.LogInformation("Successfully removed member with ID: {UserId} from organization ID: {OrganizationId}", userId, orgId);
    }

    public async Task UpdateRolesAsync(string orgId, string userId, IList<string> roles)
    {
        using var mgmt = GetManagementClient();
        _logger.LogInformation("Updating roles for member with ID: {UserId} in organization ID: {OrganizationId} with roles: {Roles}", 
            userId, orgId, string.Join(", ", roles));
        await mgmt.Organizations.AddMemberRolesAsync(orgId, userId, new OrganizationAddMemberRolesRequest
        {
            Roles = roles
        });
        _logger.LogInformation("Successfully updated roles for member with ID: {UserId} in organization ID: {OrganizationId}", userId, orgId);
    }

    public async Task DeleteRolesAsync(string orgId, string userId, IList<string> roles)
    {
        using var mgmt = GetManagementClient();
        _logger.LogInformation("Deleting roles for member with ID: {UserId} in organization ID: {OrganizationId} with roles: {Roles}", 
            userId, orgId, string.Join(", ", roles));
        await mgmt.Organizations.DeleteMemberRolesAsync(orgId, userId, new OrganizationDeleteMemberRolesRequest
        {
            Roles = roles
        });
        _logger.LogInformation("Successfully deleted roles for member with ID: {UserId} in organization ID: {OrganizationId}", userId, orgId);
    }

    public async Task<OrganizationInfo?> GetOrganizationAsync(string orgId)
    {
        using var mgmt = GetManagementClient();
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

    public async Task UpdateOrganizationAsync(UpdateOrganization info)
    {
        using var mgmt = GetManagementClient();
        await mgmt.Organizations.UpdateAsync(info.OrganizationId, new OrganizationUpdateRequest
        {
            DisplayName = info.Name,
            Metadata = info.Metadata,
            Branding = new OrganizationBranding
            {
                LogoUrl = info.LogoUrl
            }
        });
    }

    private ManagementApiClient GetManagementClient()
    {
        ArgumentNullException.ThrowIfNull(secrets.Value);
        var token = accessToken.Get().Replace("Bearer ", "");;
        return new ManagementApiClient(token, new Uri(secrets.Value.Audience));
    }
}