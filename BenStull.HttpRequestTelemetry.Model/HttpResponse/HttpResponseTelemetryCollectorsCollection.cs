using System.Collections.Generic;
using BenStull.HttpRequestTelemetry.Domain.HttpRequest;
using BenStull.HttpRequestTelemetry.Domain.HttpResponse;
using BenStull.HttpRequestTelemetry.Domain.Telemetry;

namespace BenStull.HttpRequestTelemetry.Model.HttpResponse
{
    public class HttpResponseTelemetryCollectorsCollection : IHttpResponseTelemetryCollectorsCollection
    {
        private readonly IList<IHttpResponseTelemetryCollector> _collectors;
        private bool _telemetryCollectorsExecuted;

        public HttpResponseTelemetryCollectorsCollection(IList<IHttpResponseTelemetryCollector> collectors)
        {
            _collectors = collectors;
        }

        public void ExecuteCollectors(IHttpRequestInformation requestInformation,
            IHttpResponseInformation responseInformation,
            IHttpRequestTelemetry telemetry)
        {
            if (_telemetryCollectorsExecuted)
            {
                return;
            }

            foreach (var collector in _collectors)
            {
                collector.CollectResponseTelemetry(requestInformation, responseInformation, telemetry);
            }

            _telemetryCollectorsExecuted = true;
        }
    }
}