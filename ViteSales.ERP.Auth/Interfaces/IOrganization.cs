using ViteSales.ERP.Auth.Models;

namespace ViteSales.ERP.Auth.Interfaces;

public interface IOrganization
{
    Task<string> CreateAsync(CreateOrganization organization, string userId);
    Task DeleteAsync(string id);
    Task<InvitationInfo> CreateInvitationAsync(string orgId, string inviterId, string invitee, string role);
    Task<IList<InvitationInfo>?> GetInvitationsAsync(string orgId);
    Task<IList<MemberInfo>?> GetMembersAsync(string orgId);
    Task RemoveMemberAsync(string orgId, string userId);
    Task UpdateRolesAsync(string orgId, string userId, IList<string> roles);
    Task UpdateOrganizationAsync(string orgId, KeyValuePair<string, object> keyValuePair);
    Task DeleteRolesAsync(string orgId, string userId, IList<string> roles);
    Task<OrganizationInfo?> GetOrganizationAsync(string orgId);
}