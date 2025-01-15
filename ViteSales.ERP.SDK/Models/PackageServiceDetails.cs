namespace ViteSales.ERP.SDK.Models;

public class PackageServiceDetails
{
    public Type ServiceType { get; set; } = null!;
    public string ServiceNamespace { get; set; } = null!;
    public string ServiceName { get; set; } = null!;
    public string ServiceId { get; set; } = null!;
    public string ServiceModuleName { get; set; } = null!;
    public IEnumerable<Type> ServiceModules { get; set; } = null!;
}