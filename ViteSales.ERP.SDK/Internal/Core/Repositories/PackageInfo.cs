using System.Text.Json;
using Google.Protobuf.WellKnownTypes;
using Semver;
using SqlKata.Execution;
using ViteSales.ERP.SDK.Database;
using ViteSales.ERP.SDK.Database.Operation;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Internal.Core.Entities;
using ViteSales.ERP.SDK.Models;
using ViteSales.Shared.Extensions;

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
                .Get<PackageInfoInternal>()
                ?.ToList();

            if (packageCheck?.Any() == true)
            {
                packageId = packageCheck.First().Id;
                authorId = packageCheck.First().AuthorId;
                var existingVersion = packageCheck.First().Version;
                if (SemVersion.TryParse(existingVersion, out var oldVersion) &&
                    SemVersion.TryParse(package.Version, out var newVersion) &&
                    oldVersion.CompareSortOrderTo(newVersion) > 0)
                {
                    return;
                }
            }
        }
        
        if (!IgnorePackages.Contains(package.PackageName) && !initialTableExists)
        {
            throw new Exception("Initial modules are not installed");
        }

        List<PackageDetailsInternal> installedModules = new();
        if (await IsTableExist(nameof(PackageDetailsInternal)))
        {
            var queryCompiler = await Get<PackageDetailsInternal>();
            installedModules = queryCompiler
                .Get<PackageDetailsInternal>()
                .ToList();
        }

        var details = package.Modules.Select(module =>
        {
            var moduleType = module.GetType();
            return new Insert<PackageDetailsInternal>(new PackageDetailsInternal
            {
                Id = Guid.NewGuid(),
                PackageId = packageId,
                ModuleId = $"{moduleType.Namespace ?? string.Empty}.{moduleType.Name}",
                Name = module.Name,
                Entities = module.Entities.Select(x => x.Name),
                Others = new Dictionary<string, object>
                {
                    ["IsDefaultInserted"] = false
                }
            });
        }).ToList();

        details.RemoveAll(detail => installedModules
            .Any(module => module?.ModuleId == detail?.Data.ModuleId));

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
                var updates = new List<IOperation>();
                foreach (var module in package.Modules.ToList())
                {
                    var moduleType = module.GetType();
                    var moduleNamespace = moduleType.Namespace ?? string.Empty;
                    var moduleClassName = moduleType.Name;
                    var moduleId = $"{moduleNamespace}.{moduleClassName}";

                    var defaultInserted = installedModules
                        .Where(installed => installed?.ModuleId == moduleId)
                        .Any(@internal => @internal?.Others.GetJsonPropertyValue<bool>("IsDefaultInserted") ?? false);

                    var defaultValues = module.DefaultValues();
                    if (!defaultInserted && defaultValues.Count > 0)
                    {
                        actions.AddRange(defaultValues.Select(defaultValue =>
                        {
                            var type = defaultValue.GetType();
                            var insertOperationType = typeof(Insert<>).MakeGenericType(type);
                            return (IOperation)Activator.CreateInstance(insertOperationType, defaultValue)!;
                        }));
                        updates.Add(new Update<PackageDetailsInternal>(
                            new PackageDetailsInternal
                            {
                                Name = moduleClassName,
                                Entities = module.Entities.Select(x => x.Name),
                                Others = new Dictionary<string, object> { ["IsDefaultInserted"] = true },
                                PackageId = packageId,
                                ModuleId = moduleId,
                            }, 
                            new ConditionBuilder().And("ModuleId", "=", moduleId)
                        ));
                    }
                }
                actions.AddRange(details);
                actions.AddRange(updates);
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
    
    private List<string> IgnorePackages { get; } = [
        "AuditTrailInternal",
        "PackageAuthorsInternal",
        "PackageDetailsInternal",
        "PackageInfoInternal",
        "Core"
    ];
}