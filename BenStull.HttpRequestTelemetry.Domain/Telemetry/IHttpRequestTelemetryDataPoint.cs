namespace BenStull.HttpRequestTelemetry.Domain.Telemetry
{
    public interface IHttpRequestTelemetryDataPoint
    {
        string MetricName { get; }
        string Description { get; }
        string Value { get; }
        string Unit { get; }
    }
}