namespace ViteSales.ERP.Auth.Models;

public class UpdateOrganization
{
    public required string OrganizationId { get; set; }
    public required string Name { get; set; }
    public string? LogoUrl { get; set; }
    public object? Metadata { get; set; }
}