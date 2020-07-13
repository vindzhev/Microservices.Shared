namespace MicroservicesPOC.Shared.API.ServiceDiscovery
{
    using Consul;
    
    using System.Threading;
    using System.Threading.Tasks;
    
    using Microsoft.Extensions.Hosting;

    public class ServiceDiscoveryHostedService : IHostedService
    {
        private string _registrationId;
        private readonly IConsulClient _consulClient;
        private readonly ServiceDiscoveryConfig _serviceConfig;

        public ServiceDiscoveryHostedService(IConsulClient consulClient, ServiceDiscoveryConfig serviceConfig)
        {
            this._consulClient = consulClient;
            this._serviceConfig = serviceConfig;
        }

        public async Task StartAsync(CancellationToken cancelationToken)
        {
            this._registrationId = $"{this._serviceConfig.Service.Name}-{this._serviceConfig.Service.Id}";
            var registration = new AgentServiceRegistration
            {
                ID = this._registrationId,
                Name = this._serviceConfig.Service.Name,
                Address = this._serviceConfig.Service.UrlAddress.Host,
                Port = this._serviceConfig.Service.UrlAddress.Port
            };

            await this._consulClient.Agent.ServiceDeregister(this._registrationId, cancelationToken);
            await this._consulClient.Agent.ServiceRegister(registration, cancelationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken) =>
            await this._consulClient.Agent.ServiceDeregister(this._registrationId, cancellationToken);
    }
}
