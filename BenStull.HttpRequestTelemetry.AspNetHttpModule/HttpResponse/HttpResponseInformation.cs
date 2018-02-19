using System.Web;
using BenStull.HttpRequestTelemetry.Domain.HttpResponse;

namespace BenStull.HttpRequestTelemetry.AspNetHttpModule.HttpResponse
{
    /// <summary>
    ///     Implements IHttpResponseInformation by wrapping a System.Web.HttpResponseBase object
    /// </summary>
    public class HttpResponseInformation : IHttpResponseInformation
    {
        public HttpResponseInformation(HttpResponseBase response, long responseBodySizeInBytes)
        {
            ResponseBodySizeInBytes = responseBodySizeInBytes;
        }

        public long ResponseBodySizeInBytes { get; }
    }
}