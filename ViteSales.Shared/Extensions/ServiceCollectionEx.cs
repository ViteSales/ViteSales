using Microsoft.Extensions.DependencyInjection;

namespace ViteSales.Shared.Extensions;

public static class ServiceCollectionEx
{
    public static void Merge(this IServiceCollection target, IServiceCollection source)
    {
        foreach (var service in source)
        {
            if (!target.Any(sd => sd.ServiceType == service.ServiceType && sd.Lifetime == service.Lifetime))
            {
                target.Add(service);
            }
            else
            {
                var descriptorsToRemove = target.Where(sd => sd.ServiceType == service.ServiceType).ToList();
                foreach (var descriptor in descriptorsToRemove)
                {
                    target.Remove(descriptor);
                }
                target.Add(service);
            }
        }
    }
}