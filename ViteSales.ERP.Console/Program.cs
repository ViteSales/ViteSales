using Google.Cloud.Diagnostics.Common;
using Hangfire;
using Hangfire.InMemory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ViteSales.ERP.Auth.Interfaces;
using ViteSales.ERP.Cloud.Const;
using ViteSales.ERP.Cloud.Extensions;
using ViteSales.ERP.Cloud.Interfaces;
using ViteSales.ERP.Cloud.Models.Request;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.Shared.Extensions;
using ViteSales.ERP.Shared.Models;
using ViteSales.ERP.Shared.Utils;

const string orgName = "some-org-name";
const string serviceName = "vitesales-console";
const string version = "1.0.0";
var appSettings = AppSettings.Read();

var cloud = new ViteSales.ERP.Cloud.ViteSalesCloud(appSettings);
var provider = cloud.Build();
var dbServer = provider.GetRequiredService<IDatabaseCloudService>();
var dbRequest = new CreateProjectRequest
{
    Project = new CreateProject
    {
        Settings = new Settings
        {
            MaintenanceWindow = new MaintenanceWindow
            {
                Weekdays = [7],
                StartTime = "22:00",
                EndTime = "01:00"
            },
            EnableLogicalReplication = true
        },
        Branch = new Branch
        {
            Name = "main",
            RoleName = "vitesales",
            DatabaseName = orgName
        },
        DefaultEndpointSettings = new DefaultEndpointSettings
        {
            AutoscalingLimitMinCu = DbCompute.Size1.GetSize(),
            AutoscalingLimitMaxCu = DbCompute.Size1To5.GetSize(),
            SuspendTimeoutSeconds = 600
        },
        PgVersion = 17,
        Name = orgName,
        RegionId = Regions.EuropeLondon.GetDbSlug(),
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
var sdkCollection = sdk.GetServiceCollection();
sdkCollection.Merge(auth.GetServiceCollection());
sdkCollection.Merge(cloud.GetServiceCollection());
sdkCollection.AddHangfire(configure =>
{
    configure.UseInMemoryStorage(new InMemoryStorageOptions
    {
        IdType = InMemoryStorageIdType.Guid
    });
});
sdkCollection.AddHangfireServer();
sdkCollection.AddLogging(configure =>
{
    configure.ClearProviders();
    configure.AddConsole();
    configure.AddGoogle(new LoggingServiceOptions
    {
        ProjectId = appSettings.GetGcpCredential().ProjectId,
        ServiceName = serviceName,
        Version = version,
    });
    configure.SetMinimumLevel(LogLevel.Information);
});
provider = sdk.Build();
var backgroundJobClient = provider.GetRequiredService<IBackgroundJobClient>();
var bucket = provider.GetRequiredService<IBucketCloudService>();
var bucketInfo = await bucket.CreateBucket(orgName, Regions.EuropeLondon);

var gcpPubSub = provider.GetRequiredService<IPubSubCloudService>();
await gcpPubSub.CreateTopic(cloudIdentifierPair);

var pkg = provider.GetRequiredService<IPackageInstallerService>();

var internalPkg = new ViteSales.ERP.SDK.Internal.Manifest();
var accountingPkg = new ViteSales.ERP.Accounting.Manifest();

await pkg.Install(internalPkg);
await pkg.Install(accountingPkg);

var authPkg = provider.GetRequiredService<IAuthentication>();
var uri = await authPkg.GetAuthorizationUri();
Console.WriteLine(uri != null ? uri.AbsoluteUri : "Failed to get authorization uri");

sdkCollection.Merge(accountingPkg.GetServices());
sdk.Build();

await dbServer.DeleteProject(dbInfo.Project.Id);
// await gcpPubSub.DropTopic(cloudIdentifierPair);
await bucket.DropBucket(bucketInfo);
using var server = new BackgroundJobServer();