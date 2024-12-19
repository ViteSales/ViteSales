using ViteSales.ERP.Console.SamplePackage.Modules;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.Console.SamplePackage;

public class Manifest: IPackage
{
    public string Version { get; } = "1.0.0";
    public string License { get; } = "MIT";

    public AuthorInfo Author { get; } = new()
    {
        Author = "Mohammad Julfikar <md.julfikar.mahmud@gmail.com>",
        Company = "ViteSales Pvt. Ltd."
    };
    public string PackageName { get; } = "Sales";
    public IEnumerable<IModule> Modules { get; set; } = [new InvoiceModule()];
}