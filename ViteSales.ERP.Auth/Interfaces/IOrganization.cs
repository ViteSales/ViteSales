using ViteSales.ERP.Auth.Models;

namespace ViteSales.ERP.Auth.Interfaces;

public interface IOrganization
{
    Task<string> Create(CreateOrganization organization, string userId);
    Task Delete(string id);
    Task<InvitationInfo> CreateInvitation(string orgId, string inviterId, string invitee, string role);
    Task<IList<InvitationInfo>?> GetInvitations(string orgId);
    Task<IList<MemberInfo>?> GetMembers(string orgId);
    Task RemoveMember(string orgId, string userId);
    Task UpdateRoles(string orgId, string userId, IList<string> roles);
    Task UpdateOrganization(string orgId, KeyValuePair<string, object> keyValuePair);
    Task DeleteRoles(string orgId, string userId, IList<string> roles);
    Task<OrganizationInfo?> GetOrganization(string orgId);
}