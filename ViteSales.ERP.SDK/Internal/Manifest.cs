using Microsoft.Extensions.DependencyInjection;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Internal.Core.Modules;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.SDK.Internal;

public class Manifest: IPackage
{
    public string Version { get; } = "0.0.1";
    public string License { get; } = "MIT";
    public AuthorInfo Author { get; } = new()
    {
        Name = "Mohammad Julfikar <md.julfikar.mahmud@gmail.com>",
        Company = "ViteSales Pvt. Ltd."
    };
    public string PackageName { get; } = "Core";
    
    public ServiceCollection GetServices()
    {
        var services = new ServiceCollection();
        services.AddTransient<IModule, CoreModule>();
        return services;
    }
}