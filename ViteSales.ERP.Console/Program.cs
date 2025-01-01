using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ViteSales.ERP.SDK.Database;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Services;
using ViteSales.ERP.SDK.Models;
using ViteSales.ERP.SDK.Services.MessageQueue;
using ViteSales.ERP.SDK.Utils;

var appSettings = AppSettings.Read();
var conn = new ConnectionConfig
{
    Host = "localhost",//"db.xrttljlbpuzsmzottcgs.supabase.co",
    Port = 5432,
    User = "postgres",
    Password = "frf@!333!@Fg",
    Database = "postgres"
};
var services = new ServiceCollection();
services.Configure<ConnectionConfig>(options =>
{
    options.Host = conn.Host;
    options.Database = conn.Database;
    options.Port = conn.Port;
    options.User = conn.User;
    options.Password = conn.Password;
});
services.Configure<AppSettings>(options =>
{
    options.Logging = appSettings.Logging;
    options.AuthCredential = appSettings.AuthCredential;
    options.GcpCredentials = appSettings.GetGcpCredential();
    options.GoogleCredential = appSettings.GetGoogleCredential();
});
services.AddLogging(configure =>
{
    configure.AddConsole();
});
services.AddTransient<IDbContext, DbContext>();
services.AddTransient<IPubSub, PubSub>();
services.AddTransient<IPackageInstallerService, PackageInstallerService>();
services.AddTransient<IPackageService, PackageService>();
services.AddTransient<ITableSchemaManager, TableSchemaService>();
services.AddTransient<IRoleAccessManager, RoleAccessService>();
var provider = services.BuildServiceProvider();

var pkg = provider.GetRequiredService<IPackageInstallerService>();
await pkg.Install(new ViteSales.ERP.SDK.Internal.Core.Manifest());
await pkg.Install(new ViteSales.ERP.Accounting.Manifest());
