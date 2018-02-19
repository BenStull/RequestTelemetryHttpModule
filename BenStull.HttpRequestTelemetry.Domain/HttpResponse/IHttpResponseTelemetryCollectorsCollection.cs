using BenStull.HttpRequestTelemetry.Domain.HttpRequest;
using BenStull.HttpRequestTelemetry.Domain.Telemetry;

namespace BenStull.HttpRequestTelemetry.Domain.HttpResponse
{
    public interface IHttpResponseTelemetryCollectorsCollection
    {
        /// <summary>
        ///  Executes telemetry collectors.  Will not execute if previously executed.
        ///  Response telemetry collectors should be executed once the request is finished being processed
        /// </summary>
        void ExecuteCollectors(IHttpRequestInformation requestInformation,
            IHttpResponseInformation responseInformation, IHttpRequestTelemetry requestTelemetry);
    }
}