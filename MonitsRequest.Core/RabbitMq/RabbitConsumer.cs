using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace MonitsRequest.Core.RabbitMq
{
    public class RabbitConsumer : Rabbit
    {
        public void ReadMessage(RabbitWorkQueues workQueues, IQueueConsumer queueConsumer)
        {
            workQueues.Channel = CreateChannel();
            workQueues.Consumer = GetConsumer(workQueues.Channel);

            workQueues.Consumer.Received += (model, ea) => {
                var content = Encoding.UTF8.GetString(ea.Body);

                queueConsumer.ProcessMessage(content, ea, (model as IBasicConsumer).Model);
            };
        }

        public new void CloseChannel(IModel channel) => base.CloseChannel(channel);
    }
}
