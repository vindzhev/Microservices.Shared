namespace MicroservicesPOC.Shared.API.ServiceDiscovery
{
    using System;
    
    using Consul;
    
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using System.Net.Http;
    using System.Net.Http.Headers;

    public static class ServiceDiscoveryExtensions
    {
        private static ConsulClient CreateConsulClient(ServiceDiscoveryConfig serviceConfig)
        {
            ConsulClientConfiguration configuration = new ConsulClientConfiguration { Address = serviceConfig.ServiceDiscoveryServerURL };
            HttpClient httpClient = new HttpClient() { BaseAddress = serviceConfig.ServiceDiscoveryServerURL };

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return new ConsulClient(configuration, httpClient);
        }

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
