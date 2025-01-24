
using ViteSales.ERP.Auth.Models.Auth0;

namespace ViteSales.ERP.Auth.Interfaces;

public interface IOrganization
{
    Task<string> CreateAsync(object organization, string userId);
    Task DeleteAsync(string id);
    Task<T> CreateInvitationAsync<T>(string orgId, string inviterId, string invitee, string role) where T: class;
    Task<IList<T>> GetInvitationsAsync<T>(string orgId) where T: class;
    Task<IList<T>> GetMembersAsync<T>(string orgId) where T: class;
    Task<bool> IsOrganizationExistAsync(string orgName);
    Task AddMemberAsync(string orgId, string userId, IList<string> roles);
    Task RemoveMemberAsync(string orgId, string userId);
    Task UpdateRolesAsync(string orgId, string userId, IList<string> roles);
    Task UpdateOrganizationAsync(UpdateOrganization info);
    Task DeleteRolesAsync(string orgId, string userId, IList<string> roles);
    Task<T> GetOrganizationAsync<T>(string orgId) where T: class;
}