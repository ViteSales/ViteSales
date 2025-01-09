using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Semver;
using SqlKata.Execution;
using ViteSales.ERP.SDK.Database.Operation;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Internal.Core.Entities;
using ViteSales.ERP.SDK.Models;
using ViteSales.ERP.Shared.Extensions;
using ViteSales.ERP.Shared.Models;

namespace ViteSales.ERP.SDK.Services;

public class PackageInstallerService: IPackageInstallerService
{
    private readonly ILogger<PackageInstallerService> _logger;
    private readonly IDbContext _dbContext;
    private readonly ITableSchemaManager _tableSchemaManager;

    public PackageInstallerService(ITableSchemaManager tableSchemaManager, IDbContext dbContext, IOptions<ConnectionConfig> cfg, ILogger<PackageInstallerService> log)
    {
        ArgumentNullException.ThrowIfNull(cfg.Value);
        _logger = log;
        _dbContext = dbContext;
        _tableSchemaManager = tableSchemaManager;
    }
    
    public async Task Install(IPackage package)
    {
        _logger.LogDebug("Starting installation of package: {PackageName}, Version: {PackageVersion}", package.PackageName, package.Version);
        var authorId = Guid.NewGuid();
        var packageId = Guid.NewGuid();

        try
        {
            var initialTableExists = await _dbContext.IsTableExist(nameof(PackageInfoInternal));
            if (initialTableExists)
            {
                var factory = await _dbContext.Get<PackageInfoInternal>();
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
                        _logger.LogWarning("Package: {PackageName} is already installed with a newer version: {ExistingVersion}. Skipping installation.", package.PackageName, existingVersion);
                        return;
                    }
                }
            }
            
            if (!IgnorePackages.Contains(package.PackageName) && !initialTableExists)
            {
                _logger.LogError("Initial modules are not installed. Cannot proceed with installation of package: {PackageName}", package.PackageName);
                throw new Exception("Initial modules are not installed");
            }

            List<PackageDetailsInternal> installedModules = new();
            if (await _dbContext.IsTableExist(nameof(PackageDetailsInternal)))
            {
                var queryCompiler = await _dbContext.Get<PackageDetailsInternal>();
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

            await _tableSchemaManager.CreateOrUpdateTablesAsync(package.Modules.SelectMany(m => m.Entities));
            await _dbContext.SaveChanges(() =>
            {
                _logger.LogInformation("Saving package information to the database for package: {PackageName}, Version: {PackageVersion}", package.PackageName, package.Version);
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
                        _logger.LogDebug("Inserting default values for module: {ModuleName} in package: {PackageName}", moduleClassName, package.PackageName);
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
            _logger.LogInformation("Package: {PackageName}, Version: {PackageVersion} installed successfully.", package.PackageName, package.Version);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred during the installation of package: {PackageName}, Version: {PackageVersion}", package.PackageName, package.Version);
            throw;
        }
    }

    public async Task Uninstall(IPackage package)
    {
        try
        {
            _logger.LogDebug("Starting uninstallation of package: {PackageName}", package.PackageName);

            var factory = await _dbContext.Get<PackageInfoInternal>();
            var packageCheck = factory.Select("*")
                .Where("Name", package.PackageName)
                .Get<PackageInfoInternal>();
            if (packageCheck is null)
            {
                _logger.LogError("Package {PackageName} not found in the database.", package.PackageName);
                throw new Exception("Package not found");
            }
            var packageInfoCheck = packageCheck.ToList();
            if (packageInfoCheck.Count == 0)
            {
                _logger.LogError("Package {PackageName} not found in the database.", package.PackageName);
                throw new Exception("Package not found");
            }
            var pkg = packageInfoCheck.First();
            if (pkg is null)
            {
                _logger.LogError("Package {PackageName} not found in the database.", package.PackageName);
                throw new Exception("Package not found");
            }

            _logger.LogDebug("Deleting package metadata for package: {PackageName}", package.PackageName);
            await _dbContext.SaveChanges(() =>
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

            _logger.LogDebug("Dropping tables for package modules: {PackageName}", package.PackageName);
            await _tableSchemaManager.DropTablesAsync(package.Modules.SelectMany(m => m.Entities));

            _logger.LogInformation("Package uninstalled successfully: {PackageName}", package.PackageName);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while uninstalling package: {PackageName}", package.PackageName);
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