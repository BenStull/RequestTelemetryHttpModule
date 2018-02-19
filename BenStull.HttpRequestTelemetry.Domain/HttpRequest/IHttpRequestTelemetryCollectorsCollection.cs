using BenStull.HttpRequestTelemetry.Domain.Telemetry;

namespace BenStull.HttpRequestTelemetry.Domain.HttpRequest
{
    /// <summary>
    ///  Executes request telemetry collectors.  Will not execute if previously executed.
    ///  Request telemetry collectors may be executed as soon as the HTTP request has been received
    /// </summary>
    public interface IHttpRequestTelemetryCollectorsCollection
    {
        void ExecuteCollectors(IHttpRequestInformation requestInformation, IHttpRequestTelemetry requestTelemetry);
    }
}