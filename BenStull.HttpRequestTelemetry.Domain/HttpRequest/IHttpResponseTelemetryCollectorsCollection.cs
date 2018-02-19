using BenStull.HttpRequestTelemetry.Domain.Telemetry;

namespace BenStull.HttpRequestTelemetry.Domain.HttpRequest
{
    /// <summary>
    ///  Executes telemetry collectors.  Will not execute if previously executed.
    /// </summary>
    public interface IHttpRequestTelemetryCollectorsCollection
    {
        void ExecuteCollectors(IHttpRequestInformation requestInformation, IHttpRequestTelemetry requestTelemetry);
    }
}