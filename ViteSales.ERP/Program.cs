using Google.Cloud.Diagnostics.Common;
using Hangfire;
using Hangfire.InMemory;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Syncfusion.Blazor;
using ViteSales.ERP;
using ViteSales.ERP.Cloud;
using ViteSales.ERP.Components;
using ViteSales.ERP.Shared.Extensions;
using ViteSales.ERP.Shared.Utils;

var appSettings = AppSettings.Read();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/secure");
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR(options =>
{
    options.MaximumReceiveMessageSize = 1024 * 1024 * 1024;
});
builder.Services.AddMemoryCache();

#region ViteSalesConfiguration

var vsCloud = new ViteSalesCloud(appSettings);
builder.Services.Configure<AppSettings>(option =>
{
    option.AuthSecrets = appSettings.AuthSecrets;
    option.GcpCredentials = appSettings.GetGcpCredential();
    option.GoogleCredential = appSettings.GetGoogleCredential();
    option.CacheDb = appSettings.CacheDb;
    option.ServerCredential = appSettings.ServerCredential;
    option.Logging = appSettings.Logging;
    option.DefaultDb = appSettings.DefaultDb;
});
builder.Services.Merge(vsCloud.GetServiceCollection());

#endregion

#region ViteSalesEssentials

builder.Services.AddHangfire(configure =>
{
    configure.UseInMemoryStorage(new InMemoryStorageOptions
    {
        IdType = InMemoryStorageIdType.Guid
    });
});
builder.Services.AddHangfireServer();
builder.Services.AddLogging(configure =>
{
    configure.ClearProviders();
    configure.AddConsole();
    configure.AddGoogle(new LoggingServiceOptions
    {
        ProjectId = appSettings.GetGcpCredential().ProjectId,
        ServiceName = builder.Configuration.GetSection("ServiceName").Value,
        Version = builder.Configuration.GetSection("ServiceVersion").Value,
    });
    configure.SetMinimumLevel(LogLevel.Information);
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOpenIdConnect();
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddSyncfusionBlazor();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(builder.Configuration.GetSection("SyncfusionLicense").Value);

#endregion

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapRazorPages();
app.UseAuthentication();
app.UseAuthorization();
app.Run();