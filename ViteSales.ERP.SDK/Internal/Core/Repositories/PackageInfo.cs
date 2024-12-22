using System.Text.Json;
using SqlKata.Execution;
using ViteSales.ERP.SDK.Database;
using ViteSales.ERP.SDK.Database.Operation;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Internal.Core.Entities;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.SDK.Internal.Core.Repositories;

public class PackageInfo(ConnectionConfig config): DbContext(config)
{
    private readonly ConnectionConfig _config = config;

    public async Task Install(IPackage package)
    {
        var mgr = new TableSchemaManager(_config);
        var authorId = Guid.NewGuid();
        var packageId = Guid.NewGuid();
        if (!IgnoreModules.Contains(package.PackageName))
        {
            var factory = await Get<PackageInfoInternal>();
            var packageCheck = factory.Select("*")
                .Where("Name", package.PackageName)
                .Get<PackageInfoInternal>();
        
            if (packageCheck is not null)
            {
                var packageInfoCheck = packageCheck.ToList();
                if (packageInfoCheck.First().Version == package.Version)
                {
                    return;
                }
            }
        }
        
        var details = (from module in package.Modules
            let moduleType = module.GetType()
            let moduleNamespace = moduleType.Namespace ?? string.Empty
            let moduleClassName = moduleType.Name
            select new Insert<PackageDetailsInternal>(new PackageDetailsInternal()
            {
                Id = Guid.NewGuid(),
                PackageId = packageId,
                ModuleId = $"{moduleNamespace}.{moduleClassName}",
                Name = module.Name,
                Entities = JsonSerializer.Serialize(module.Entities.Select(x => Activator.CreateInstance(x)!))
            })
        ).ToList();

        try
        {
            await mgr.CreateOrUpdateTablesAsync(package.Modules.SelectMany(m => m.Entities));
            await SaveChanges(() =>
            {
                var author = new Insert<PackageAuthorsInternal>(new PackageAuthorsInternal()
                {
                    Id = authorId,
                    Author = package.Author.Author,
                    Company = package.Author.Company,
                    Email = package.Author.Email,
                    Phone = package.Author.Phone,
                    Website = package.Author.Website
                });
                var info = new Insert<PackageInfoInternal>(new PackageInfoInternal()
                {
                    AuthorId = authorId,
                    Id = packageId,
                    License = package.License,
                    Name = package.PackageName,
                    Version = package.Version
                });
                var actions = new List<IOperation>();
                actions.Add(author);
                actions.AddRange(details);
                actions.Add(info);
                return actions;
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Uninstall(IPackage package)
    {
        var mgr = new TableSchemaManager(_config);
        try
        {
            var factory = await Get<PackageInfoInternal>();
            var packageCheck = factory.Select("*")
                .Where("Name", package.PackageName)
                .Get<PackageInfoInternal>();
            if (packageCheck is null)
            {
                throw new Exception("Package not found");
            }
            var packageInfoCheck = packageCheck.ToList();
            if (packageInfoCheck.Count == 0)
            {
                throw new Exception("Package not found");
            }
            var pkg = packageInfoCheck.First();
            if (pkg is null)
            {
                throw new Exception("Package not found");
            }
            await SaveChanges(() =>
            {
                var actions = new List<IOperation>
                {
                    new Delete<PackageAuthorsInternal>([new WhereClause()
                    {
                        Field = "Id",
                        Operator = "=",
                        Value = pkg.AuthorId
                    }]),
                    new Delete<PackageDetailsInternal>([new WhereClause()
                    {
                        Field = "PackageId",
                        Operator = "=",
                        Value = pkg.Id
                    }]),
                    new Delete<PackageInfoInternal>([new WhereClause()
                    {
                        Field = "Id",
                        Operator = "=",
                        Value = pkg.Id
                    }])
                };
                return actions;
            });
            await mgr.DropTablesAsync(package.Modules.SelectMany(m => m.Entities));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    private List<string> IgnoreModules { get; } = [
        "AuditTrailInternal",
        "PackageAuthorsInternal",
        "PackageDetailsInternal",
        "PackageInfoInternal",
        "Core"
    ];
}