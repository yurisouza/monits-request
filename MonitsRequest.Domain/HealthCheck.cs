using System;

namespace MonitsRequest.Domain
{
    public class HealthCheck
    {
        public Guid HealthCheckKey { get; set; }
        public string Name { get; set; }
        public string Endpoint { get; set; }
        public long IntervalInMilliseconds { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
