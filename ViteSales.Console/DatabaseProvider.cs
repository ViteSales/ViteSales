using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using ViteSales.Data.Entities;

namespace ViteSales.Console;

public class DatabaseProvider(bool migrationsEnabled)
{
    public ServiceProvider Get()
    {
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
                    System.Console.WriteLine($"Retry #{exceptions.Count} with delay {retryEventData.Delay} due to error: {exceptions.Last().Message}");
                });
        });
        var serviceProvider = serviceCollection.BuildServiceProvider();
        using var ctx = serviceProvider.GetRequiredService<ViteSalesContext>();
        
        if (!ctx.Database.CanConnect()) throw new Exception("Cannot connect to database");
        ctx.Database.SetCommandTimeout(TimeSpan.FromMinutes(10));
        
        if (migrationsEnabled)
        {
            var databaseCreator = 
                (RelationalDatabaseCreator) ctx.Database.GetService<IDatabaseCreator>();
            databaseCreator.CreateTables();
        }

        return serviceCollection.BuildServiceProvider();
    }
}

// dotnet ef dbcontext scaffold "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=frf@\!333\!@Fg;" Npgsql.EntityFrameworkCore.PostgreSQL -o Models
// "Host=aws-0-ap-southeast-1.pooler.supabase.com;Port=6543;Username=postgres.bafclcxttfppruzavrgm;Password=supabase123@;Database=postgres;"