using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitsRequest.Core.RabbitMq
{
    public interface IQueueConsumer
    {
        void ProcessMessage(string obj, BasicDeliverEventArgs eventArgs, IModel channel);
    }
}
