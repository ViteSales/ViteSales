using ViteSales.ERP.Auth.Interfaces;
using ViteSales.ERP.Auth.Models;

namespace ViteSales.ERP.Auth.Services.Kinde;

public class OrganizationService: IOrganization
{
    public Task<string> Create(CreateOrganization organization, string userId)
    {
        throw new NotImplementedException();
    }

    public Task Delete(string id)
    {
        throw new NotImplementedException();
    }

    public Task<InvitationInfo> CreateInvitation(string orgId, string inviterId, string invitee, string role)
    {
        throw new NotImplementedException();
    }

    public Task<IList<InvitationInfo>?> GetInvitations(string orgId)
    {
        throw new NotImplementedException();
    }

    public Task<IList<MemberInfo>?> GetMembers(string orgId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveMember(string orgId, string userId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRoles(string orgId, string userId, IList<string> roles)
    {
        throw new NotImplementedException();
    }

    public Task UpdateOrganization(string orgId, KeyValuePair<string, object> keyValuePair)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRoles(string orgId, string userId, IList<string> roles)
    {
        throw new NotImplementedException();
    }

    public Task<OrganizationInfo?> GetOrganization(string orgId)
    {
        throw new NotImplementedException();
    }
}