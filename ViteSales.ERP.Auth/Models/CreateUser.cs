namespace ViteSales.ERP.Auth.Models;

public class CreateUser
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string FullName { get; set; }
    public bool? IsAdmin { get; set; }
}