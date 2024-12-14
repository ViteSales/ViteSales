using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.SDK.Interfaces;

public interface IPackage
{
    public string Version { get; }
    public string License { get; }
    public AuthorInfo Author { get; }
    
    public string PackageName { get; }
    public IEnumerable<IModule> Modules { get; }
}