
using ViteSales.ERP.Auth.Models;

namespace ViteSales.ERP.Auth.Interfaces;

public interface IOrganizationAuthService
{
    Task<string> CreateAsync(CreateOrganization organization, string userId);
    Task DeleteAsync(string id);
    Task<InvitationInfo> CreateInvitationAsync(string orgId, string inviterId, string invitee, string role);
    Task<IList<InvitationInfo>> GetInvitationsAsync(string orgId);
    Task<IList<MemberInfo>> GetMembersAsync(string orgId);
    Task<bool> IsOrganizationExistAsync(string orgName);
    Task AddMemberAsync(string orgId, string userId, IList<string> roles);
    Task RemoveMemberAsync(string orgId, string userId);
    Task UpdateRolesAsync(string orgId, string userId, IList<string> roles);
    Task UpdateOrganizationAsync(UpdateOrganization info);
    Task DeleteRolesAsync(string orgId, string userId, IList<string> roles);
    Task<OrganizationInfo?> GetOrganizationAsync(string orgId);
}