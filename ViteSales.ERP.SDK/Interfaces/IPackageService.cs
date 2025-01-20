namespace ViteSales.ERP.SDK.Interfaces;

public interface IPackageService
{
    Task<List<Exception>?> InstallPackageAsync(string namespaceName);
    Task<List<Exception>?> UninstallPackageAsync(string namespaceName);
}