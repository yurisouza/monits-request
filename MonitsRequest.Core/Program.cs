using Microsoft.Extensions.Configuration;
using MonitsRequest.Core.Services;
using SimpleInjector;
using System.IO;

namespace MonitsRequest.Core
{
    class Program
    {
        private static Container _container;

        private static void Setup()
        {
            _container = new Container();

            StartupIoC.Register(_container);
            _container.Verify();
        }

        static void Main(string[] args)
        {
            Setup();
            var readMessageService = _container.GetInstance<ReadMessageService>();
            readMessageService.Start();
        }
    }
}
