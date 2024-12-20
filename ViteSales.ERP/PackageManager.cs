using System.Reflection;
using ViteSales.ERP.SDK.Database;
using ViteSales.ERP.SDK.Interfaces;
using ViteSales.ERP.SDK.Models;

namespace ViteSales.ERP;

public class PackageManager
{
    private readonly TableSchemaManager _schemaManager;
    private readonly List<Assembly> _assemblies;
    
    public PackageManager(ConnectionConfig config)
    {
        var connectionHandler = new Connection(config);
        _schemaManager = new TableSchemaManager(connectionHandler);
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
        var exceptions = new List<Exception>();
        var manifesto = GetNamespaceMatchingClasses(namespaceName).OfType<IPackage>();
        var enumerable = manifesto.ToList();
        if (enumerable.Count == 0)
        {
            exceptions.Add(new Exception($"Package {namespaceName} not found or Manifest is not implemented"));
        }
        foreach (var manifest in enumerable)
        {
            try
            {
                await _schemaManager.CreateOrUpdateTablesAsync(manifest.Modules.SelectMany(m => m.Entities));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                exceptions.Add(e);
            }
        }
        return exceptions;
    }

    public async Task<List<Exception>?> UninstallPackage(string namespaceName)
    {
        var exceptions = new List<Exception>();
        var manifesto = GetNamespaceMatchingClasses(namespaceName).OfType<IPackage>();
        var enumerable = manifesto.ToList();
        if (enumerable.Count == 0)
        {
            exceptions.Add(new Exception($"Package {namespaceName} not found or Manifest is not implemented"));
        }
        foreach (var manifest in enumerable)
        {
            try
            {
                await _schemaManager.DropTablesAsync(manifest.Modules.SelectMany(m => m.Entities));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                exceptions.Add(e);
            }
        }
        return exceptions;
    }
    
    private List<IPackage?> GetNamespaceMatchingClasses(string namespaceName)
    {
        return _assemblies
            .Where(assembly => !assembly.IsDynamic && assembly.GetName().Name != null)
            .SelectMany(assembly => assembly.GetTypes())
            .Where(t => typeof(IPackage).IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false })
            .Where(cls => cls is { Name: "Manifest", Namespace: not null })
            .Select(cls =>
            {
                if (Activator.CreateInstance(cls) is IPackage manifest && manifest.PackageName == namespaceName)
                    return manifest;
                return null;
            })
            .Where(manifest => manifest != null)!
            .ToList();
    }
}