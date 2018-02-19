using BenStull.HttpRequestTelemetry.Domain.Telemetry;

namespace BenStull.HttpRequestTelemetry.Domain.HttpRequest
{
    /// <summary>
    /// Collects information about an IHttpRequest and sets the data in IHttpRequestTelemetry
    /// 
    /// Thread safety: should handle concurrency
    /// 
    /// </summary>
    public interface IHttpRequestTelemetryCollector
    {
        void CollectResponseTelemetry(IHttpRequestInformation requestInformation, IHttpRequestTelemetry requestTelemetry);
    }
}