using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ViteSales.ERP.SDK.Database;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Models;
using ViteSales.ERP.SDK.Services;
using ViteSales.ERP.SDK.Services.MessageQueue;
using ViteSales.ERP.SDK.Utils;

namespace ViteSales.ERP.SDK;

public class ViteSales
{
    private readonly ServiceCollection _services = new();
    public ViteSales(ConnectionConfig conn)
    {
        var appSettings = AppSettings.Read();
        
        _services.Configure<ConnectionConfig>(options =>
        {
            options.Host = conn.Host;
            options.Database = conn.Database;
            options.Port = conn.Port;
            options.User = conn.User;
            options.Password = conn.Password;
        });
        _services.Configure<AppSettings>(options =>
        {
            options.Logging = appSettings.Logging;
            options.AuthCredential = appSettings.AuthCredential;
            options.GcpCredentials = appSettings.GetGcpCredential();
            options.GoogleCredential = appSettings.GetGoogleCredential();
        });
        _services.AddLogging(configure =>
        {
            configure.AddConsole();
        });
        _services.AddTransient<IPubSub, PubSub>();
        _services.AddTransient<IDbContext, DbContext>();
        _services.AddTransient<IPackageService, PackageService>();
        _services.AddTransient<IRoleAccessManager, RoleAccessService>();
        _services.AddTransient<ITableSchemaManager, TableSchemaService>();
        _services.AddTransient<IPackageInstallerService, PackageInstallerService>();
    }

    public ServiceCollection GetServiceCollection()
    {
        return _services;
    }

    public ServiceProvider Build()
    {
        return _services.BuildServiceProvider();
    }
}