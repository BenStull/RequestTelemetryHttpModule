using System.Collections.Generic;

namespace BenStull.HttpRequestTelemetry.Domain.Telemetry
{
    /// <summary>
    /// Contains telemetry information about the app's handling of an http request
    /// </summary>
    public interface IHttpRequestTelemetry
    {
        IList<IHttpRequestTelemetryDataPoint> TelemetryDataPoints { get; }
        void AddDataPoint(IHttpRequestTelemetryDataPoint dataPoint);
    }
}