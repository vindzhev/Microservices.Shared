namespace MicroservicesPOC.Shared.API.Extensions
{
    using System;
    using System.Linq;

    using Consul;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Hosting.Server.Features;

    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ConsulInitializationExtentions
    {
        public static IServiceCollection AddConsulConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConsulClient, ConsulClient>(x => new ConsulClient(setup => setup.Address = new Uri(configuration.GetValue<string>("ConsulConfig:Host"))));

            return services;
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app, IConfiguration configuration)
        {
            IConsulClient consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            ILogger logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("AppExtensions");
            IHostApplicationLifetime lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            if (!(app.Properties["server.Features"] is FeatureCollection features))
                return app;

            IServerAddressesFeature serverAddresses = features.Get<IServerAddressesFeature>();
            if (serverAddresses.Addresses.Count == 0)
            {
                Microsoft.AspNetCore.Http.HttpContext httpContext = app.ApplicationServices.GetService<Microsoft.AspNetCore.Http.HttpContext>();
                serverAddresses.Addresses.Add($"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.PathBase}");
            }

            string address = serverAddresses.Addresses.First();

            Uri uri = new Uri(address);
            string serviceName = configuration.GetValue<string>("ConsulConfig:ServiceName");

            AgentServiceRegistration agentServiceRegistration = new AgentServiceRegistration()
            {
                ID = $"{serviceName}:{uri.Port}.{Environment.MachineName}",
                Name = serviceName,
                Address = uri.Host,
                Port = uri.Port
            };

            logger.LogInformation("Registering with Consul");
            consulClient.Agent.ServiceDeregister(agentServiceRegistration.ID).ConfigureAwait(true);
            consulClient.Agent.ServiceRegister(agentServiceRegistration).ConfigureAwait(true);

            lifetime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("Unregistering from Consul");
                consulClient.Agent.ServiceDeregister(agentServiceRegistration.ID).ConfigureAwait(true);
            });

            return app;
        }
    }
}
