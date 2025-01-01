using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Internal;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP.SDK.Services;

public class PackageService: IPackageService
{
    private readonly List<Assembly> _assemblies;
    private readonly IPackageInstallerService _packageInstallerService;
    private readonly ILogger<PackageService> _logger;
    
    public PackageService(IPackageInstallerService installerService,IOptions<ConnectionConfig> cfg, ILogger<PackageService> log)
    {
        ArgumentNullException.ThrowIfNull(cfg);
        ArgumentNullException.ThrowIfNull(log);
        _logger = log;
        _logger.LogInformation("PackageService initialized");
        
        var config = cfg.Value;
        _packageInstallerService = installerService;
        _assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a =>
            {
                var name = a.GetName().Name;
                return name != null &&
                       name.StartsWith("ViteSales.ERP", StringComparison.OrdinalIgnoreCase);
            })
            .ToList();
    }

    public async Task<List<Exception>?> InstallPackage(string namespaceName)
    {
        _logger.LogDebug("Starting installation of package: {NamespaceName}", namespaceName);
        var exceptions = new List<Exception>();
        var manifesto = GetNamespaceMatchingClasses(namespaceName).OfType<IPackage>();
        var enumerable = manifesto.ToList();
        if (enumerable.Count == 0)
        {
            _logger.LogWarning("Package {NamespaceName} not found or Manifest is not implemented", namespaceName);
            exceptions.Add(new Exception($"Package {namespaceName} not found or Manifest is not implemented"));
        }
        foreach (var manifest in enumerable)
        {
            try
            {
                _logger.LogDebug("Installing package manifest: {PackageName}", manifest.PackageName);
                await _packageInstallerService.Install(manifest);
                _logger.LogInformation("Successfully installed package manifest: {PackageName}", manifest.PackageName);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while installing package: {NamespaceName}", namespaceName);
                exceptions.Add(e);
            }
        }
        _logger.LogInformation("Finished installation of package: {NamespaceName}", namespaceName);
        return exceptions;
    }

    public async Task<List<Exception>?> UninstallPackage(string namespaceName)
    {
        _logger.LogDebug("Starting uninstallation of package: {NamespaceName}", namespaceName);
        var exceptions = new List<Exception>();
        var manifesto = GetNamespaceMatchingClasses(namespaceName).OfType<IPackage>();
        var enumerable = manifesto.ToList();
        if (enumerable.Count == 0)
        {
            _logger.LogWarning("Package {NamespaceName} not found or Manifest is not implemented", namespaceName);
            exceptions.Add(new Exception($"Package {namespaceName} not found or Manifest is not implemented"));
        }
        foreach (var manifest in enumerable)
        {
            try
            {
                _logger.LogDebug("Uninstalling package manifest: {PackageName}", manifest.PackageName);
                await _packageInstallerService.Uninstall(manifest);
                _logger.LogInformation("Successfully uninstalled package manifest: {PackageName}", manifest.PackageName);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while uninstalling package: {NamespaceName}", namespaceName);
                exceptions.Add(e);
            }
        }
        _logger.LogInformation("Finished uninstallation of package: {NamespaceName}", namespaceName);
        return exceptions;
    }
    
    private List<IPackage?> GetNamespaceMatchingClasses(string namespaceName)
    {
        _logger.LogInformation("Searching for classes matching namespace: {NamespaceName}", namespaceName);
        return _assemblies
            .Where(assembly => !assembly.IsDynamic && assembly.GetName().Name != null)
            .SelectMany(assembly => assembly.GetTypes())
            .Where(t => typeof(IPackage).IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false })
            .Where(cls => cls is { Name: "Manifest", Namespace: not null })
            .Select(cls =>
            {
                try
                {
                    if (Activator.CreateInstance(cls) is IPackage manifest && manifest.PackageName == namespaceName)
                    {
                        _logger.LogInformation("Matching class found: {ClassName}", cls.Name);
                        return manifest;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error occurred while creating instance of class: {ClassName}", cls.Name);
                }
                return null;
            })
            .Where(manifest => manifest != null)!
            .ToList();
    }
}