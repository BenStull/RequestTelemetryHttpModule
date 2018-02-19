using System;
using System.Diagnostics;
using System.Web;
using BenStull.HttpRequestTelemetry.Domain.HttpRequest;
using BenStull.HttpRequestTelemetry.Domain.ScopedStopwatch;
using BenStull.HttpRequestTelemetry.Model.Util;

namespace BenStull.HttpRequestTelemetry.AspNetHttpModule.HttpRequest
{
    /// <summary>
    /// Implements IHttpResponseInformation by wrapping a System.Web.HttpContextBase object
    /// </summary>
    public class HttpRequestInformation : IHttpRequestInformation
    {
        public DateTime RequestStartTime { get; }

        private readonly Stopwatch _telemetryOverheadTimer = new Stopwatch();

        public TimeSpan TelemetryProcessingOverheadTime => _telemetryOverheadTimer.Elapsed;

        public IScopedStopwatch StartTelemetryProcessingOverheadBlock()
        {
            return new ScopedStopwatch(_telemetryOverheadTimer);
        }

        public HttpRequestInformation(HttpContextBase httpContext)
        {
            RequestStartTime = httpContext.Timestamp;
        }
    }
}
