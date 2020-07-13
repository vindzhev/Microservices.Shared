namespace MicroservicesPOC.Shared.API.ServiceDiscovery
{
    using System;

    using Microsoft.Extensions.Configuration;
    
    public static class ServiceConfigExtensions
    {
        public static ServiceDiscoveryConfig GetServiceConfig(this IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(ConfigurationBinder));

            return new ServiceDiscoveryConfig()
            {
                ServiceDiscoveryServerURL = configuration.GetValue<Uri>("ServiceConfig:serviceDiscoveryServerURL"),
                Service = new ServiceConfig()
                {
                    Id = configuration.GetValue<string>("ServiceConfig:Service:Id"),
                    Name = configuration.GetValue<string>("ServiceConfig:Service:Name"),
                    UrlAddress = configuration.GetValue<Uri>("ServiceConfig:Service:UrlAddress")
                }
            };
        }
    }
}
