namespace ViteSales.ERP.Auth.Models;

public class CreateOrganization
{
    public required string Name { get; set; }
    public string? LogoUrl { get; set; }
    public object? Metadata { get; set; }
}