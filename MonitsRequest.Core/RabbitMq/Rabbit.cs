using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitsRequest.Core.RabbitMq
{
    public abstract class Rabbit
    {
        protected IConnection _connection { get; set; }

        public Rabbit()
        {
            try
            {
                this._connection = GetRabbitConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        protected IModel CreateChannel()
        {
            var channel = this._connection.CreateModel();

            IDictionary<string, object> args = new Dictionary<string, object>
            {
                { "x-max-priority", 10 }
            };

            channel.QueueDeclare("HealthCheck", true, false, false, args);
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            return channel;
        }

        public EventingBasicConsumer GetConsumer(IModel channel)
        {

            var consumer = new EventingBasicConsumer(channel);
            var args = new Dictionary<string, object>();

            channel.BasicConsume(
                queue: "HealthCheck",
                autoAck: false,
                arguments: args,
                consumer: consumer);
            return consumer;
        }

        public void AckMessage(BasicDeliverEventArgs eventArgs, IModel channel)
        {
            try
            {
                channel.BasicAck(eventArgs.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void NAckMessage(BasicDeliverEventArgs eventArgs, IModel channel)
        {
            try
            {
                channel.BasicNack(eventArgs.DeliveryTag, multiple: false, requeue: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private IConnection GetRabbitConnection()
        {

            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@35.237.58.96:5672/"),
                AutomaticRecoveryEnabled = true
            };

            var connection = factory.CreateConnection();
            return connection;
        }

        public void CloseConnection()
        {
            if (_connection.IsOpen)
                _connection.Close();
        }

        protected void CloseChannel(IModel channel)
        {
            if (channel.IsOpen)
                channel.Close(200, "Stop");
        }
    }
}
