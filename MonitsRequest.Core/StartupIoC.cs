using Microsoft.Extensions.Configuration;
using MonitsRequest.Core.Interfaces.Repository;
using MonitsRequest.Core.RabbitMq;
using MonitsRequest.Core.RabbitMq.Implementation;
using MonitsRequest.Core.Repositories;
using MonitsRequest.Core.Services;
using MySql.Data.MySqlClient;
using SimpleInjector;
using System.Data;

namespace MonitsRequest.Core
{
    public class StartupIoC
    {
        public static void Register(Container container)
        {
            container.Register<IDbConnection>(() =>
            {
                return new MySqlConnection("Server=35.232.135.192;Database=rawhostc_monitsdb;Uid=rawhostc_appdb;Pwd=BCzOY7QEFb4xDhbiBB; convert zero datetime=True; Allow User Variables=true");
            }, Lifestyle.Singleton);
            container.Register<IHealthCheckResultRepository, HealthCheckResultRepository>(Lifestyle.Singleton);
            container.Register<RabbitConsumer>(Lifestyle.Singleton);
            container.Register<HealthCheckConsumer>(Lifestyle.Singleton);
            container.Register<RequestService>(Lifestyle.Singleton);
            container.Register<ReadMessageService>(Lifestyle.Singleton);
        }
    }
}
