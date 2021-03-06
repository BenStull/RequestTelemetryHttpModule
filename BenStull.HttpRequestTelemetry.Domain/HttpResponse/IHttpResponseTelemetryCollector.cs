﻿using BenStull.HttpRequestTelemetry.Domain.HttpRequest;
using BenStull.HttpRequestTelemetry.Domain.Telemetry;

namespace BenStull.HttpRequestTelemetry.Domain.HttpResponse
{
    /// <summary>
    ///     Collects information about an IHttpResponse and sets the data in IHttpRequestTelemetry
    ///     Thread safety: should handle concurrency
    /// </summary>
    public interface IHttpResponseTelemetryCollector
    {
        void CollectResponseTelemetry(IHttpRequestInformation requestInformation,
            IHttpResponseInformation responseInformation, IHttpRequestTelemetry requestTelemetry);
    }
}