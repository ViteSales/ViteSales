using Microsoft.Extensions.DependencyInjection;
using ViteSales.ERP.Accounting.GL.Interfaces;
using ViteSales.ERP.Accounting.GL.Modules;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.Accounting;

public class Manifest: IPackage
{
    public string Version { get; } = "0.0.2";
    public string License { get; } = "MIT";
    public AuthorInfo Author { get; } = new()
    {
        Name = "Mohammad Julfikar <md.julfikar.mahmud@gmail.com>",
        Company = "ViteSales Pvt. Ltd."
    };
    public string PackageName { get; } = "Accounting";
    public ServiceCollection GetServices()
    {
        var services = new ServiceCollection();
        services.AddTransient<IAccountTypes, AccountTypesModule>();
        services.AddTransient<IFiscalYears, FiscalYearsModule>();
        return services;
    }
}