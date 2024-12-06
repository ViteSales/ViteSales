using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using ViteSales.Data.Entities;
using ViteSales.ERP.GL.AccountMaintenance;

const bool DO_MIGRATION = false;
// dotnet ef dbcontext scaffold "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=frf@\!333\!@Fg;" Npgsql.EntityFrameworkCore.PostgreSQL -o Models
// "Host=aws-0-ap-southeast-1.pooler.supabase.com;Port=6543;Username=postgres.bafclcxttfppruzavrgm;Password=supabase123@;Database=postgres;"
var serviceCollection = new ServiceCollection();
serviceCollection.AddDbContext<ViteSalesContext>(options =>
{
    options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=frf@!333!@Fg;Database=postgres;");
    options.EnableDetailedErrors();
    options.EnableServiceProviderCaching();
    options.EnableSensitiveDataLogging();
    options.LogTo(
        filter: (eventId, level) => eventId.Id == CoreEventId.ExecutionStrategyRetrying,
        logger: (eventData) =>
        {
            if (eventData is not ExecutionStrategyEventData retryEventData) return;
            var exceptions = retryEventData.ExceptionsEncountered;
            Console.WriteLine($"Retry #{exceptions.Count} with delay {retryEventData.Delay} due to error: {exceptions.Last().Message}");
        });
});
    
var serviceProvider = serviceCollection.BuildServiceProvider();

using var ctx = serviceProvider.GetRequiredService<ViteSalesContext>();
if (ctx.Database.CanConnect())
{
    Console.WriteLine("Connected!");
    ctx.Database.SetCommandTimeout(TimeSpan.FromMinutes(10));
    
    if (DO_MIGRATION)
    {
        var databaseCreator = 
            (RelationalDatabaseCreator) ctx.Database.GetService<IDatabaseCreator>();
        databaseCreator.CreateTables();
    }

    var accType = new AccTypeImpl(ctx);
    var dt = accType.Load();
    Console.WriteLine(dt.Rows.Count);
    
    AddDummyData(dt);
    accType.Save(dt);
}
static void AddDummyData(DataTable dt)
{
    // Adding new rows (simulate new entries)
    /*var newRow = dt.NewRow();
    newRow["AccTypeCode"] = "NT";
    newRow["Description"] = "New Type 1";
    newRow["Desc2"] = "Description 2 for New Type 1";
    newRow["IsBstype"] = "T";
    newRow["IsSystemType"] = "F";
    dt.Rows.Add(newRow);*/

    // Modify existing rows (simulate updates)
    /*if (dt.Rows.Count > 0)
    {
        var existingRow = dt.Rows[0];
        existingRow["Description"] = "Description for Existing Type";
        existingRow["Desc2"] = "Description 2 for Existing Type";
    }*/

    // Mark an existing row for deletion (simulate deletion)
    if (dt.Rows.Count > 0)
    {
        var rowToDelete = dt.Rows[0];
        rowToDelete.Delete();
    }
}