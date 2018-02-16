using BenStull.HttpRequestTelemetry.Domain.Telemetry;

namespace BenStull.HttpRequestTelemetry.Domain.HttpResponse
{
    /// <summary>
    /// Collects information about an IHttpResponse and sets the data in IHttpRequestTelemetry
    /// </summary>
    public interface IHttpResponseTelemetryCollector
    {
        void CollectResponseTelemetry(IHttpResponseInformation response, IHttpRequestTelemetry requestTelemetry);
    }
}