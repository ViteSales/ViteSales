namespace ViteSales.ERP.Auth.Models;

public class OrganizationInfo
{
    public required string Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public string LogoUrl { get; set; }
    public object? Metadata { get; set; }
}