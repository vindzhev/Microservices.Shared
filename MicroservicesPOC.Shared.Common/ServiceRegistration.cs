namespace MicroservicesPOC.Shared.Common
{
    using System;
    using System.Linq;
    using System.Reflection;
    
    using MicroservicesPOC.Shared.Common.Services;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceRegistration
    {
        public static IServiceCollection AddConventionalServices(this IServiceCollection services, Assembly assembly)
        {
            Type serviceInterfaceType = typeof(IService);
            Type scopedServiceInterfaceType = typeof(IScopedService);
            Type singletonServiceInterfaceType = typeof(ISingletonService);

            var types = assembly.GetExportedTypes()
                .Where(x => x.IsClass && !x.IsAbstract)
                .Select(x => new { Implementation = x, Service = x.GetInterface($"I{x.Name.Replace("Service", string.Empty)}") })
                .Where(x => x.Service != null);

            foreach (var type in types)
            {
                if (serviceInterfaceType.IsAssignableFrom(type.Service))
                    services.AddTransient(type.Service, type.Implementation);
                else if (scopedServiceInterfaceType.IsAssignableFrom(type.Service))
                    services.AddScoped(type.Service, type.Implementation);
                else if (singletonServiceInterfaceType.IsAssignableFrom(type.Service))
                    services.AddSingleton(type.Service);
            }

            return services;
        }
    }
}
