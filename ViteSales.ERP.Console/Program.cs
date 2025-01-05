using Microsoft.Extensions.DependencyInjection;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Models;

var conn = new ConnectionConfig
{
    Host = "localhost",//"db.xrttljlbpuzsmzottcgs.supabase.co",
    Port = 5432,
    User = "postgres",
    Password = "frf@!333!@Fg",
    Database = "postgres"
};
var services = new ViteSales.ERP.SDK.ViteSales(conn);
var provider = services.Build();

var pkg = provider.GetRequiredService<IPackageInstallerService>();
await pkg.Install(new ViteSales.ERP.SDK.Internal.Core.Manifest());
await pkg.Install(new ViteSales.ERP.Accounting.Manifest());
