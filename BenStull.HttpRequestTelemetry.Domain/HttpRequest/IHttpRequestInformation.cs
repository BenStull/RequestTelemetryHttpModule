using System;
using BenStull.HttpRequestTelemetry.Domain.ScopedStopwatch;

namespace BenStull.HttpRequestTelemetry.Domain.HttpRequest
{
    public interface IHttpRequestInformation
    {
        DateTime RequestStartTime { get; }

        TimeSpan TelemetryProcessingOverheadTime { get; }
        IScopedStopwatch StartTelemetryProcessingOverheadBlock();
    }
}