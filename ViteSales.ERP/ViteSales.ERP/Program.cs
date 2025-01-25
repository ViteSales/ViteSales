﻿using ViteSales.ERP.Data;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;
using ViteSales.ERP;

//Register Syncfusion license https://help.syncfusion.com/common/essential-studio/licensing/how-to-generate
//Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR LICENSE KEY");
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddMemoryCache();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddSyncfusionBlazor();
            builder.Services.AddSingleton<PowerPointService>();
            builder.Services.AddSingleton<WordService>();
            builder.Services.AddSingleton<PdfService>();
            builder.Services.AddSingleton<ExcelService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
