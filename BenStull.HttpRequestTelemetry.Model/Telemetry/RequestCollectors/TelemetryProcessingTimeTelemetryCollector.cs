using System.Globalization;
using BenStull.HttpRequestTelemetry.Domain.HttpRequest;
using BenStull.HttpRequestTelemetry.Domain.HttpResponse;
using BenStull.HttpRequestTelemetry.Domain.Telemetry;

namespace BenStull.HttpRequestTelemetry.Model.Telemetry.RequestCollectors
{
    public class TelemetryProcessingTimeTelemetryCollector : IHttpResponseTelemetryCollector
    {
        public void CollectResponseTelemetry(IHttpRequestInformation requestInformation,
            IHttpResponseInformation responseInformation,
            IHttpRequestTelemetry requestTelemetry)
        {
            var dataPoint = new HttpRequestTelemetryDataPoint
            {
                MetricName = "Telemetry Processing Overhead Time",
                Description = "Total time the agent spent processing telemetry, in milliseconds",
                Unit = "ms",
                Value =
                    requestInformation.TelemetryProcessingOverheadTime.TotalMilliseconds.ToString(CultureInfo
                        .InvariantCulture)
            };

            requestTelemetry.AddDataPoint(dataPoint);
        }
    }
}