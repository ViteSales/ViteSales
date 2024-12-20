using ViteSales.ERP.Accounting.GL.Modules;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.Accounting;

public class Manifest: IPackage
{
    public string Version { get; } = "0.0.1";
    public string License { get; } = "MIT";
    public AuthorInfo Author { get; } = new()
    {
        Author = "Mohammad Julfikar <md.julfikar.mahmud@gmail.com>",
        Company = "ViteSales Pvt. Ltd."
    };
    public string PackageName { get; } = "Accounting";
    public IEnumerable<IModule> Modules { get; } = [new AccountTypesModule()];
}