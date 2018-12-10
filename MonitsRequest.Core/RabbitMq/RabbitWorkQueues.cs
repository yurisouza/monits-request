using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitsRequest.Core.RabbitMq
{
    public class RabbitWorkQueues
    {
        public IModel Channel { get; set; }
        public EventingBasicConsumer Consumer { get; set; }
    }
}
