using System;

namespace BenStull.HttpRequestTelemetry.Domain.HttpRequest
{
    public interface IHttpRequestInformation
    {
        DateTime RequestStartTime { get; }
    }
}