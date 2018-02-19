using System;
using BenStull.HttpRequestTelemetry.Domain.HttpRequest;
using BenStull.HttpRequestTelemetry.Domain.HttpResponse;
using BenStull.HttpRequestTelemetry.Domain.Telemetry;

namespace BenStull.HttpRequestTelemetry.Model.Telemetry.RequestCollectors
{
    /// <summary>
    ///     Tracks the total processing time telemetry metrics
    ///     Should be called when request processing is complete
    /// </summary>
    public class TotalProcessingTimeTelemetryCollector : IHttpResponseTelemetryCollector
    {
        public void CollectResponseTelemetry(IHttpRequestInformation requestInformation,
            IHttpResponseInformation responseInformation,
            IHttpRequestTelemetry requestTelemetry)
        {
            var dataPoint = new HttpRequestTelemetryDataPoint
            {
                MetricName = "Total Processing Time",
                Description = "Total time server spent processing the http request, in milliseconds",
                Unit = "ms",
                Value = (DateTime.Now - requestInformation.RequestStartTime).Milliseconds.ToString()
            };

            requestTelemetry.AddDataPoint(dataPoint);
        }
    }
}