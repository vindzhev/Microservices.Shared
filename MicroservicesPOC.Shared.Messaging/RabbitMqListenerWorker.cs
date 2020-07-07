namespace MicroservicesPOC.Shared.Messaging
{
    using System;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    using Newtonsoft.Json;

    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    
    public class RabbitMqListenerWorker<T> : BackgroundService where T : INotification
    {
        private IModel _channel;
        private IConnection _connection;

        private readonly IServiceProvider _services;

        private readonly string _routingKey;
        private readonly RabbitMQConfigurations _options;

        public RabbitMqListenerWorker(IOptions<RabbitMQConfigurations> rabbitMqOptions, IServiceProvider services)
        {
            this._services = services;
            this._routingKey = typeof(T).Name.ToLower();
            this._options = rabbitMqOptions.Value;

            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            ConnectionFactory factory = new ConnectionFactory { HostName = this._options.Hostname, UserName = this._options.Username, Password = this._options.Password };

            this._connection = factory.CreateConnection();
            this._connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

            this._channel = _connection.CreateModel();
            this._channel.ExchangeDeclare(exchange: this._options.Exchange.Name, "topic", durable: true, autoDelete: false, arguments: null);
            this._channel.QueueDeclare(queue: $"{this._options.Queue.Prefix}{this._routingKey}", durable: true, exclusive: false, autoDelete: false, arguments: null);
            this._channel.QueueBind(queue: $"{this._options.Queue.Prefix}{this._routingKey}", exchange: this._options.Exchange.Name, routingKey: this._routingKey, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(this._channel);
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;
            consumer.Received += (channel, envelope) =>
            {
                string content = Encoding.UTF8.GetString(envelope.Body.ToArray());
                T notification = JsonConvert.DeserializeObject<T>(content);

                using (var scope = this._services.CreateScope())
                {
                    var internalBus = scope.ServiceProvider.GetService<IMediator>();
                    internalBus.Publish(notification).Wait();
                }

                this._channel.BasicAck(envelope.DeliveryTag, false);
            };

            this._channel.BasicConsume(queue: $"{this._options.Queue.Prefix}{this._routingKey}", autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

        public override void Dispose()
        {
            this._channel.Close();
            this._connection.Close();

            base.Dispose();
        }
    }
}
