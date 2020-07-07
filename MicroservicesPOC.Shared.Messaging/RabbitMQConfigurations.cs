namespace MicroservicesPOC.Shared.Messaging
{
    using System;

    public class RabbitMQConfigurations
    {
        public string Username { get; set; } = "guest";

        public string Password { get; set; } = "guest";

        public string Hostname { get; set; } = "localhost";

        public int Port { get; set; } = 5672;

        public string VirtualHost { get; set; } = "/";

        public TimeSpan RequestTimeout { get; set; }

        public TimeSpan PublishConfirmTimeout { get; set; }

        public TimeSpan RecoveryInterval { get; set; }

        public bool PersistentDeliveryMode { get; set; }

        public bool AutoCloseConnection { get; set; }

        public bool AutomaticRecovery { get; set; }

        public bool TopologyRecovery { get; set; }

        public Exchange Exchange { get; set; }

        public Queue Queue { get; set; }
    }

    public class Exchange
    {
        public string Name { get; set; }

        public bool Durable { get; set; }

        public bool AutoDelete { get; set; }

        public string Type { get; set; }
    }

    public class Queue
    {
        public string Prefix { get; set; }

        public bool AutoDelete { get; set; }

        public bool Durable { get; set; }

        public bool Exclusive { get; set; }
    }
}
