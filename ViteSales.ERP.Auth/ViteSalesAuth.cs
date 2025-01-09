using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ViteSales.ERP.Auth.Client;
using ViteSales.ERP.Auth.Interfaces;
using ViteSales.ERP.Auth.Services;
using ViteSales.ERP.Shared.Cache;
using ViteSales.ERP.Shared.Interfaces;
using ViteSales.ERP.Shared.Models;
using ViteSales.ERP.Shared.Utils;

namespace ViteSales.ERP.Auth;

public class ViteSalesAuth
{
    private readonly ServiceCollection _services = new();
    public ViteSalesAuth(AppSettings appSettings)
    {
        var cacheCredentials = appSettings.CacheDb;
        var authSecrets = appSettings.AuthSecrets;
        var client = new AccessToken(authSecrets);
        var cache = new CacheClient(new CacheDbCredentials
        {
            Host = cacheCredentials.Host,
            User = cacheCredentials.User,
            Password = cacheCredentials.Password,
            Port = cacheCredentials.Port,
            Database = cacheCredentials.Database
        },"ViteSalesAuth");
        cache.Connect().Wait();
        
        _services.Configure<AuthSecrets>(options =>
        {
            options.AuthorityUrl = authSecrets.AuthorityUrl;
            options.ClientId = authSecrets.ClientId;
            options.ClientSecret = authSecrets.ClientSecret;
            options.Audience = authSecrets.Audience;
            options.GrantType = authSecrets.GrantType;
            options.AuthDomain = authSecrets.AuthDomain;
        });
        _services.AddSingleton<ICacheClient>(cache);
        _services.AddSingleton<IAccessToken>(client);
        _services.AddTransient<IAuthentication, AuthenticationService>();
        _services.AddTransient<IOrganization, OrganizationService>();
        _services.AddTransient<IUser, UserService>();
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