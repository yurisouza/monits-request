using MonitsRequest.Core.Interfaces.Repository;
using MonitsRequest.Domain;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitsRequest.Core.Services
{
    public class RequestService
    {
        private IHealthCheckResultRepository _healthCheckResultRepository;

        public RequestService(IHealthCheckResultRepository healthCheckResultRepository)
        {
            _healthCheckResultRepository = healthCheckResultRepository;
        }

        public void ProcessRequest(HealthCheck healthCheck)
        {
            var healthCheckResult = new HealthCheckResult()
            {
                HealthCheckKey = healthCheck.HealthCheckKey,
                HealthCheckResultKey = Guid.NewGuid()
            };

            var client = new RestClient(healthCheck.Endpoint);
            var request = new RestRequest(Method.GET);

            healthCheckResult.RequestAt = DateTime.UtcNow;
            var response = client.Execute(request);
            healthCheckResult.ResponseAt = DateTime.UtcNow;

            healthCheckResult.CalculeteResponseTime();
            healthCheckResult.ContentResult = response.Content;
            healthCheckResult.StatusCode = (int)response.StatusCode;

            _healthCheckResultRepository.Insert(healthCheckResult);
        }
    }
}
