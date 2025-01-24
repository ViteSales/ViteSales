namespace ViteSales.ERP.Auth.Models.Auth0;

public class CreateUser
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string FullName { get; set; }
}