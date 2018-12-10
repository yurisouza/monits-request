using System;
using System.Collections.Generic;
using System.Text;

namespace MonitsRequest.Domain
{
    public class HealthCheckResult
    {
        public Guid HealthCheckResultKey { get; set; }
        public Guid HealthCheckKey { get; set; }
        public DateTime RequestAt { get; set; }
        public DateTime ResponseAt { get; set; }
        public int StatusCode { get; set; }
        public string ContentResult { get; set; }
        private long ResponseInMilliseconds { get; set; }

        public void CalculeteResponseTime()
        {
            var elapsedTicks = ResponseAt.Ticks - RequestAt.Ticks;
            var elapsedSpan = new TimeSpan(elapsedTicks);
            ResponseInMilliseconds = elapsedSpan.Milliseconds;
        }
    }
}
