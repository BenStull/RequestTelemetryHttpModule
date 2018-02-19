using System.Collections.Generic;
using BenStull.HttpRequestTelemetry.Domain.Telemetry;

namespace BenStull.HttpRequestTelemetry.Model.Telemetry
{
    public class HttpRequestTelemetry : IHttpRequestTelemetry
    {
        public IList<IHttpRequestTelemetryDataPoint> TelemetryDataPoints { get; } = new List<IHttpRequestTelemetryDataPoint>();

        public void AddDataPoint(IHttpRequestTelemetryDataPoint dataPoint)
        {
            TelemetryDataPoints.Add(dataPoint);
        }
    }
}