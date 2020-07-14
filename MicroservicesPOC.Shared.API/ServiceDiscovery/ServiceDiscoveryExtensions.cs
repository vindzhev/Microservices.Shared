namespace MicroservicesPOC.Shared.API.ServiceDiscovery
{
    using System;
    
    using Consul;
    
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceDiscoveryExtensions
    {
        private static ConsulClient CreateConsulClient(ServiceDiscoveryConfig serviceConfig) =>
            new ConsulClient(setup => setup.Address = serviceConfig.ServiceDiscoveryServerURL);

        public static void RegisterConsulServices(this IServiceCollection services, ServiceDiscoveryConfig serviceConfig)
        {
            if (serviceConfig == null)
                throw new ArgumentNullException(nameof(serviceConfig));

            ConsulClient consulClient = CreateConsulClient(serviceConfig);

            services.AddSingleton(serviceConfig);
            services.AddSingleton<IHostedService, ServiceDiscoveryHostedService>();
            services.AddSingleton<IConsulClient, ConsulClient>(c => consulClient);
        }
    }
}
