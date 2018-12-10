using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MonitsRequest.Core.RabbitMq.Implementation
{
    public class HealthCheckConsumer
    {
        private readonly RabbitConsumer _consumer;
        private RabbitWorkQueues[] _rabbitWorkQueues;

        public HealthCheckConsumer(RabbitConsumer consumer)
        {
            this._consumer = consumer;
            var maxConsumerWork = 50;
            this._rabbitWorkQueues = new RabbitWorkQueues[maxConsumerWork];
        }

        public void ReadMessages(IQueueConsumer queueConsumer)
        {
            ServicePointManager.DefaultConnectionLimit = _rabbitWorkQueues.Length;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);

            for (int i = 0; i < _rabbitWorkQueues.Length; i++)
            {
                this._rabbitWorkQueues[i] = new RabbitWorkQueues();
                this._consumer.ReadMessage(this._rabbitWorkQueues[i], queueConsumer);
            }
        }

        public void AckMessage(BasicDeliverEventArgs eventArgs, IModel channel)
        {
            this._consumer.AckMessage(eventArgs, channel);
        }

        public void NAckMessage(BasicDeliverEventArgs eventArgs, IModel channel)
        {
            this._consumer.NAckMessage(eventArgs, channel);
        }

        public void CloseChannel()
        {
            foreach (var workQueue in this._rabbitWorkQueues)
            {
                this._consumer.CloseChannel(workQueue.Channel);
            }
        }

        public void CloseConnection()
        {
            this._consumer.CloseConnection();
        }
    }
}
