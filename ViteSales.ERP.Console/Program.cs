using Microsoft.Extensions.DependencyInjection;
using ViteSales.ERP.Auth.Interfaces;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Models;
using ViteSales.Shared.Extensions;
using ViteSales.Shared.Models;
using ViteSales.Shared.Utils;

var conn = new ConnectionConfig
{
    Host = "localhost",//"db.xrttljlbpuzsmzottcgs.supabase.co",
    Port = 5432,
    User = "postgres",
    Password = "frf@!333!@Fg",
    Database = "postgres"
};
var appSettings = AppSettings.Read();

var auth = new ViteSales.ERP.Auth.ViteSalesAuth(appSettings);
var sdk = new ViteSales.ERP.SDK.ViteSales(appSettings, conn);

sdk.GetServiceCollection().Merge(auth.GetServiceCollection());
var provider = sdk.Build();

/*var pkg = provider.GetRequiredService<IPackageInstallerService>();
await pkg.Install(new ViteSales.ERP.SDK.Internal.Core.Manifest());
await pkg.Install(new ViteSales.ERP.Accounting.Manifest());*/

var authPkg = provider.GetRequiredService<IAuthentication>();
var uri = await authPkg.GetAuthorizationUri();
if(uri != null)
{
    Console.WriteLine(uri.AbsoluteUri);
}
else
{
    Console.WriteLine("Failed to get authorization uri");
}