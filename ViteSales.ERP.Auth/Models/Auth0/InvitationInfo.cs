namespace ViteSales.ERP.Auth.Models.Auth0;

public class InvitationInfo
{
    public string Id { get; set; }
    public string OrganizationId { get; set; }
    public string Inviter { get; set; }
    public string Invitee { get; set; }
    public string InvitationUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public IList<string> Roles { get; set; }
}