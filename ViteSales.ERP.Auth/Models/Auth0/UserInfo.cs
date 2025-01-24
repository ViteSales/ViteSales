namespace ViteSales.ERP.Auth.Models.Auth0;

public class UserInfo
{
    public required string Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? LastLoggedIn { get; set; }
    public DateTime? LastPasswordChanged { get; set; }
    public DateTime? LastUpdated { get; set; }
}