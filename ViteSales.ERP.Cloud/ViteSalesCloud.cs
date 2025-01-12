using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ViteSales.ERP.Cloud.Interfaces;
using ViteSales.ERP.Cloud.Services;
using ViteSales.ERP.Shared.Utils;

namespace ViteSales.ERP.Cloud;

public class ViteSalesCloud
{
    private readonly ServiceCollection _services = new();
    public ViteSalesCloud(AppSettings appSettings)
    {
        _services.Configure<AppSettings>(options =>
        {
            options.Logging = appSettings.Logging;
            options.AuthSecrets = appSettings.AuthSecrets;
            options.GcpCredentials = appSettings.GetGcpCredential();
            options.GoogleCredential = appSettings.GetGoogleCredential();
            options.CacheDb = appSettings.CacheDb;
            options.ServerCredential = appSettings.ServerCredential;
        });
        _services.AddLogging(configure =>
        {
            configure.AddConsole();
        });
        _services.AddTransient<IPubSubCloudService, PubSubCloudService>();
        _services.AddTransient<IDatabaseCloudService, DatabaseCloudService>();
        _services.AddTransient<IBucketCloudService, BucketCloudService>();
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