using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ViteSales.ERP.SDK.Database;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Services;
using ViteSales.ERP.SDK.Models;
using ViteSales.ERP.SDK.Utils;

var gcpConfig = GcpConfig.ReadGcpJsonFile();
var conn = new ConnectionConfig
{
    Host = "localhost",//"db.xrttljlbpuzsmzottcgs.supabase.co",
    Port = 5432,
    User = "postgres",
    Password = "frf@!333!@Fg",
    Database = "postgres"
};
var services = new ServiceCollection();
services.Configure<GcpConfig>(options =>
{
    options.Credential = gcpConfig.Credential;
    options.AuthInfo = gcpConfig.AuthInfo;
});
services.Configure<ConnectionConfig>(options =>
{
    options.Host = conn.Host;
    options.Database = conn.Database;
    options.Port = conn.Port;
    options.User = conn.User;
    options.Password = conn.Password;
});
services.AddLogging(configure =>
{
    configure.AddConsole();
});
services.AddTransient<IDbContext, DbContext>();
services.AddTransient<IPackageInstallerService, PackageInstallerService>();
services.AddTransient<IPackageService, PackageService>();
services.AddTransient<ITableSchemaManager, TableSchemaService>();
services.AddTransient<IRoleAccessManager, RoleAccessService>();
var provider = services.BuildServiceProvider();

var pkg = provider.GetRequiredService<IPackageInstallerService>();
await pkg.Install(new ViteSales.ERP.SDK.Internal.Core.Manifest());
await pkg.Install(new ViteSales.ERP.Accounting.Manifest());

/*using ViteSales.ERP.SDK.Internal.Core.Repositories;
using ViteSales.ERP.SDK.Models;

var conn = new ConnectionConfig()
{
/*Host = "localhost"#1#
Host = "db.xrttljlbpuzsmzottcgs.supabase.co",
Port = 5432,
User = "postgres",
Password = "frf@!333!@Fg",
Database = "postgres"
};

var pkg = new PackageInstallerService(conn);
await pkg.Install(new ViteSales.ERP.SDK.Internal.Core.Manifest());
await pkg.Install(new ViteSales.ERP.Accounting.Manifest());*/

// const string username = "staff_member";
//
// var rbac = new RoleAccessService(conn);
// await rbac.DropUser(username);
// await rbac.CreateUser(username);
// await rbac.GrantAccess(username, [
//     DbUserRoles.Read,
//     DbUserRoles.Write
// ], ["AccountTypes"]);
// await rbac.IsUserExists(username);
// await rbac.RemoveAccess(username, [
//     DbUserRoles.Read,
//     DbUserRoles.Write
// ], ["AccountTypes"]);

// await pkg.Uninstall(new ViteSales.ERP.SDK.Internal.Core.Manifest());

/*
var db = new DbContext(conn);
var manager = new TableSchemaService(conn);
await manager.CreateOrUpdateTablesAsync(manifest.Modules.SelectMany(x => x.Entities));
*/