using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ViteSales.ERP.SDK.Attributes;
using ViteSales.ERP.SDK.Models;
using ViteSales.ERP.SDK.Services;

namespace ViteSales.ERP.SDK.Utils;

public static class PackageServicesEx
{
    public static IEnumerable<PackageServiceDetails> GetDetails(this ServiceCollection service)
    {
        return service
            .Where(descriptor =>
            {
                var serviceType = descriptor.ImplementationType;
                return serviceType is not null;
            })
            .Where(descriptor =>
            {
                var serviceType = descriptor.ImplementationType;
                return serviceType!.GetCustomAttribute<ModuleNameAttribute>() is not null &&
                       serviceType!.GetCustomAttribute<ModuleNameAttribute>() is not null;
            })
            .Select(descriptor =>
            {
                var serviceType = descriptor.ImplementationType;
                var serviceNamespace = serviceType!.Namespace ?? string.Empty;
                var serviceName = serviceType.Name;
                var serviceId = $"{serviceNamespace}.{serviceName}";
                var moduleEntities = new List<Type>();
                var entities = serviceType.GetCustomAttribute<ModuleEntitiesAttribute>();
                if (entities is not null)
                {
                    moduleEntities = entities.Entities.ToList();
                }

                var moduleName = serviceType.GetCustomAttribute<ModuleNameAttribute>();
                return new PackageServiceDetails
                {
                    ServiceId = serviceId,
                    ServiceModuleName = moduleName!.ModuleName,
                    ServiceEntities = moduleEntities,
                    ServiceName = serviceName,
                    ServiceNamespace = serviceNamespace,
                    ServiceType = serviceType
                };
            });
    }
}