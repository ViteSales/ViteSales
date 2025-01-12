using Microsoft.Extensions.DependencyInjection;
using ViteSales.ERP.Auth.Interfaces;
using ViteSales.ERP.Cloud.Const;
using ViteSales.ERP.Cloud.Extensions;
using ViteSales.ERP.Cloud.Interfaces;
using ViteSales.ERP.Cloud.Models.Request;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.Shared.Extensions;
using ViteSales.ERP.Shared.Models;
using ViteSales.ERP.Shared.Utils;


var appSettings = AppSettings.Read();

var cloud = new ViteSales.ERP.Cloud.ViteSalesCloud(appSettings);
var provider = cloud.Build();
var dbServer = provider.GetRequiredService<IDatabaseCloudService>();
var dbRequest = new CreateProjectRequest
{
    Project = new CreateProject
    {
        DefaultEndpointSettings = new DefaultEndpointSettings
        {
            AutoscalingLimitMaxCu = DbCompute.Size1.GetSize(),
            AutoscalingLimitMinCu = DbCompute.Size1.GetSize(),
            SuspendTimeoutSeconds = 600
        },
        PgVersion = 17,
        Name = "some-org-name",
        RegionId = Regions.EuropeLondon.GetDbSlug(),
        Branch = new Branch
        {
            Name = "main",
            DatabaseName = "some-org-name",
            RoleName = "vitesales",
        },
        StorePasswords = true
    }
};
var dbInfo = await dbServer.CreateProject(dbRequest);
if (dbInfo == null)
{
    Console.WriteLine("failed to create project");
    return;
}
var connectionParams = dbInfo!.ConnectionUris[0].ConnectionParameters;
var conn = new ConnectionConfig
{
    Host = connectionParams.PoolerHost,
    User = connectionParams.Role,
    Password = connectionParams.Password,
    Database = connectionParams.Database
};
var cloudIdentifierPair = new CloudIdentifierPair
{
    Cloud = conn.Host,
    Identifier = conn.Database
};
    
var auth = new ViteSales.ERP.Auth.ViteSalesAuth(appSettings);
var sdk = new ViteSales.ERP.SDK.ViteSales(appSettings, conn);
sdk.GetServiceCollection().Merge(auth.GetServiceCollection());
sdk.GetServiceCollection().Merge(cloud.GetServiceCollection());
    
provider = sdk.Build();
    
var gcpPubSub = provider.GetRequiredService<IPubSubCloudService>();
await gcpPubSub.CreateTopic(cloudIdentifierPair);
    
var pkg = provider.GetRequiredService<IPackageInstallerService>();
await pkg.Install(new ViteSales.ERP.SDK.Internal.Manifest());
await pkg.Install(new ViteSales.ERP.Accounting.Manifest());

var authPkg = provider.GetRequiredService<IAuthentication>();
var uri = await authPkg.GetAuthorizationUri();
Console.WriteLine(uri != null ? uri.AbsoluteUri : "Failed to get authorization uri");
