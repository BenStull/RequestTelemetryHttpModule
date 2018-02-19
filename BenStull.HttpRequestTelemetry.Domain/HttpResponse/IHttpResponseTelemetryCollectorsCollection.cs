using BenStull.HttpRequestTelemetry.Domain.HttpRequest;
using BenStull.HttpRequestTelemetry.Domain.Telemetry;

namespace BenStull.HttpRequestTelemetry.Domain.HttpResponse
{
    public interface IHttpResponseTelemetryCollectorsCollection
    {
        /// <summary>
        ///  Executes telemetry collectors.  Will not execute if previously executed.
        /// </summary>
        void ExecuteCollectors(IHttpRequestInformation requestInformation,
            IHttpResponseInformation responseInformation, IHttpRequestTelemetry requestTelemetry);
    }
}