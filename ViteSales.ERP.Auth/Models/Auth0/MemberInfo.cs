namespace ViteSales.ERP.Auth.Models.Auth0;

public class MemberInfo
{
    public required string UserId { get; set; }
    public string Name { get; set; }
    public string PictureUrl { get; set; }
    public string Email { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? LastLoggedIn { get; set; }
    public DateTime? LastPasswordChanged { get; set; }
    public DateTime? LastUpdated { get; set; }
    public IList<MemberRole> Roles { get; set; }
}

public class MemberRole
{
    public string Id { get; set; }
    public string Name { get; set; }
}