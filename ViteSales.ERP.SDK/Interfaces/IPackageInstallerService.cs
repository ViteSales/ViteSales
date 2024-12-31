namespace ViteSales.ERP.SDK.Interfaces;

public interface IPackageInstallerService
{
    Task Install(IPackage package);
    Task Uninstall(IPackage package);
}