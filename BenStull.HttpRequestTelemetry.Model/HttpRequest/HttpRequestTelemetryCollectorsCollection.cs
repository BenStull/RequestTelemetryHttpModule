using System.Collections.Generic;
using BenStull.HttpRequestTelemetry.Domain.HttpRequest;
using BenStull.HttpRequestTelemetry.Domain.Telemetry;

namespace BenStull.HttpRequestTelemetry.Model.HttpRequest
{
    public class HttpRequestTelemetryCollectorsCollection : IHttpRequestTelemetryCollectorsCollection
    {
        private readonly IList<IHttpRequestTelemetryCollector> _collectors;
        private bool _telemetryCollectorsExecuted;

        public HttpRequestTelemetryCollectorsCollection(IList<IHttpRequestTelemetryCollector> collectors)
        {
            _collectors = collectors;
        }

        public void ExecuteCollectors(IHttpRequestInformation requestInformation, IHttpRequestTelemetry telemetry)
        {
            if (_telemetryCollectorsExecuted)
                return;

            foreach (var collector in _collectors)
            {
                collector.CollectRequestTelemetry(requestInformation, telemetry);
            }

            _telemetryCollectorsExecuted = true;
        }
    }
}