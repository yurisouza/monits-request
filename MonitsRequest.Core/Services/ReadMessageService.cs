using MonitsRequest.Core.RabbitMq;
using MonitsRequest.Core.RabbitMq.Implementation;
using MonitsRequest.Domain;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonitsRequest.Core.Services
{
    public class ReadMessageService : IQueueConsumer
    {
        private readonly List<Task> _tasks;
        private HealthCheckConsumer _healthCheckQueue;
        private RequestService _requestService;

        public ReadMessageService(HealthCheckConsumer healthCheckQueue, RequestService requestService)
        {
            _requestService = requestService;
            _healthCheckQueue = healthCheckQueue;
            _tasks = new List<Task>();
        }

        public void ProcessMessage(string obj, BasicDeliverEventArgs eventArgs, IModel channel)
        {
            var task = Task.Run(() =>
            {
                var healthCheck = JsonConvert.DeserializeObject<HealthCheck>(obj);
                _requestService.ProcessRequest(healthCheck);
                Thread.Sleep((int)healthCheck.IntervalInMilliseconds);
                _healthCheckQueue.NAckMessage(eventArgs, channel);
            });
            _tasks.Add(task);
            task.ContinueWith((antecedent) => _tasks.Remove(antecedent));
        }

        public void Start()
        {
            _healthCheckQueue.ReadMessages(this);
        }
    }
}
