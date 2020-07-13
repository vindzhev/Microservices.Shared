namespace MicroservicesPOC.Shared.API.ServiceDiscovery
{
    using System;

    public class ServiceConfig
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Uri UrlAddress { get; set; }
    }

    public class ServiceDiscoveryConfig
    {
        public Uri ServiceDiscoveryServerURL{ get; set; }

        public ServiceConfig Service { get; set; }
    }
}
