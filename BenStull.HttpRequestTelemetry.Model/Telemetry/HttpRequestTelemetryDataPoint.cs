using BenStull.HttpRequestTelemetry.Domain.Telemetry;

namespace BenStull.HttpRequestTelemetry.Model.Telemetry
{
    public class HttpRequestTelemetryDataPoint : IHttpRequestTelemetryDataPoint
    {
        public string MetricName { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }
    }
}