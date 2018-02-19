using System;
using System.Web;
using BenStull.HttpRequestTelemetry.Domain.HttpRequest;

namespace BenStull.HttpRequestTelemetry.AspNetHttpModule.HttpRequest
{
    /// <summary>
    /// Implements IHttpResponseInformation by wrapping a System.Web.HttpContextBase object
    /// </summary>
    public class HttpRequestInformation : IHttpRequestInformation
    {
        public DateTime RequestStartTime { get; }

        public HttpRequestInformation(HttpContextBase httpContext)
        {
            RequestStartTime = httpContext.Timestamp;
        }
    }
}
