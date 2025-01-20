namespace ViteSales.ERP.SDK.Interfaces;

public interface IPackageInstallerService
{
    Task InstallAsync(IPackage package);
    Task UninstallAsync(IPackage package);
}