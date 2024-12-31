namespace ViteSales.ERP.SDK.Interfaces;

public interface IPackageService
{
    Task<List<Exception>?> InstallPackage(string namespaceName);
    Task<List<Exception>?> UninstallPackage(string namespaceName);
}