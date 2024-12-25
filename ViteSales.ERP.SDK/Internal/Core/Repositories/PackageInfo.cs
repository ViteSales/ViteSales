using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using System.Xml.Serialization;
using SqlKata.Execution;
using ViteSales.ERP.SDK.Database;
using ViteSales.ERP.SDK.Database.Operation;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Internal.Core.Entities;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.SDK.Internal.Core.Repositories;

public class PackageInfo(ConnectionConfig config): DbContext(config,"PackageInfo")
{
    private readonly ConnectionConfig _config = config;

    public async Task Install(IPackage package)
    {
        var mgr = new TableSchemaManager(_config);
        var authorId = Guid.NewGuid();
        var packageId = Guid.NewGuid();
        var initialTableExists = await IsTableExist(nameof(PackageInfoInternal));
        if (initialTableExists)
        {
            var factory = await Get<PackageInfoInternal>();
            var packageCheck = factory.Select("*")
                .Where("Name", package.PackageName)
                .Get<PackageInfoInternal>();
        
            if (packageCheck is not null)
            {
                var packageInfoCheck = packageCheck.ToList();
                if (packageInfoCheck.Count > 0 && packageInfoCheck.First().Version == package.Version)
                {
                    return;
                }
            }
        }
        if (!IgnoreModules.Contains(package.PackageName) && !initialTableExists)
        {
            throw new Exception("Initial modules are not installed");
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
                Entities = module.Entities.Select(x => x.Name)
            })
        ).ToList();

        try
        {
            await mgr.CreateOrUpdateTablesAsync(package.Modules.SelectMany(m => m.Entities));
            await SaveChanges(() =>
            {
                var author = new Upsert<PackageAuthorsInternal>(new PackageAuthorsInternal
                {
                    Id = authorId,
                    Name = package.Author.Name,
                    Company = package.Author.Company,
                    Email = package.Author.Email,
                    Phone = package.Author.Phone,
                    Website = package.Author.Website
                },new ConditionBuilder()
                        .And("Name","=", package.Author.Name)
                    );
                var info = new Upsert<PackageInfoInternal>(new PackageInfoInternal
                {
                    AuthorId = authorId,
                    Id = packageId,
                    License = package.License,
                    Name = package.PackageName,
                    Version = package.Version
                }, new ConditionBuilder()
                    .And("Name", "=", package.PackageName));
                var actions = new List<IOperation> { author, info };
                foreach (var module in package.Modules.ToList())
                {
                    var defaultValues = module.DefaultValues();
                    if (defaultValues is { Count: 0 }) continue;
                    actions.AddRange(from defaultValue in defaultValues let type = defaultValue.GetType() let insertOperationType = typeof(Insert<>).MakeGenericType(type) select (IOperation)Activator.CreateInstance(insertOperationType, defaultValue)!);
                }
                actions.AddRange(details);
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
                    new Delete<PackageAuthorsInternal>(new ConditionBuilder()
                        .And("Id","=",pkg.AuthorId)),
                    new Delete<PackageDetailsInternal>(new ConditionBuilder()
                        .And("PackageId","=",pkg.Id)),
                    new Delete<PackageInfoInternal>(new ConditionBuilder()
                        .And("Id","=",pkg.Id))
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