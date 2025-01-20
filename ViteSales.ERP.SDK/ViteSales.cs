using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ViteSales.ERP.SDK.Database;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Services;
using ViteSales.ERP.SDK.Services.MessageQueue;
using ViteSales.ERP.Shared.Cache;
using ViteSales.ERP.Shared.Extensions;
using ViteSales.ERP.Shared.Interfaces;
using ViteSales.ERP.Shared.Models;
using ViteSales.ERP.Shared.Utils;

namespace ViteSales.ERP.SDK;

public class ViteSales
{
    private readonly ServiceCollection _services = new();
    public ViteSales(AppSettings appSettings, ConnectionConfig conn)
    {
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
            options.AuthSecrets = appSettings.AuthSecrets;
            options.GcpCredentials = appSettings.GetGcpCredential();
            options.GoogleCredential = appSettings.GetGoogleCredential();
            options.CacheDb = appSettings.CacheDb;
        });
        _services.AddFluentEmail("<NO_EMAIL_ADDRESS>")
            .AddLiquidRenderer()
            .AddSendGridSender(appSettings.ServerCredential.SendGrid);
        _services.AddLogging(configure =>
        {
            configure.AddConsole();
        });
        _services.AddSingleton<ICacheClient, CacheClient>();
        _services.AddTransient<IPubSubService, PubSubService>();
        _services.AddTransient<IDbContext, DbContext>();
        _services.AddTransient<IPackageService, PackageService>();
        _services.AddTransient<IRoleAccessManager, RoleAccessService>();
        _services.AddTransient<ITableSchemaManager, TableSchemaService>();
        _services.AddTransient<IPackageInstallerService, PackageInstallerService>();
        _services.AddTransient<ISendEmailService, SendEmailService>();
        _services.AddTransient<IAttachmentIOService, AttachmentIOService>();
    }

    public ServiceCollection GetPublicServiceCollection()
    {
        _services.RemoveService<IRoleAccessManager>();
        _services.RemoveService<IPackageInstallerService>();
        _services.RemoveService<ITableSchemaManager>();
        _services.RemoveService<IPackageService>();
        return _services;
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