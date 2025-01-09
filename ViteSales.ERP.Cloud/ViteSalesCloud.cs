using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ViteSales.ERP.Shared.Models;
using ViteSales.ERP.Shared.Utils;

namespace ViteSales.ERP.Cloud;

public class ViteSalesCloud
{
    private readonly ServiceCollection _services = new();
    public ViteSalesCloud(AppSettings appSettings, CloudIdentifierPair identifierPair)
    {
        _services.Configure<AppSettings>(options =>
        {
            options.Logging = appSettings.Logging;
            options.AuthSecrets = appSettings.AuthSecrets;
            options.GcpCredentials = appSettings.GetGcpCredential();
            options.GoogleCredential = appSettings.GetGoogleCredential();
            options.CacheDb = appSettings.CacheDb;
        });
        _services.Configure<CloudIdentifierPair>(options =>
        {
            options.Cloud = identifierPair.Cloud;
            options.Identifier = identifierPair.Identifier;
        });
        _services.AddLogging(configure =>
        {
            configure.AddConsole();
        });
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