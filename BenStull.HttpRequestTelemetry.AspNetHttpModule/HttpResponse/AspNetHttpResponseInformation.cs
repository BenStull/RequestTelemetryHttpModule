using BenStull.HttpRequestTelemetry.Domain.HttpResponse;

namespace BenStull.HttpRequestTelemetry.AspNetHttpModule.HttpResponse
{
    /// <summary>
    /// Implements IHttpResponseInformation by wrapping a System.Web.HttpResponseBase object
    /// </summary>
    public class AspNetHttpResponseInformation : IHttpResponseInformation
    {
        public long ResponseBodySizeInBytes { get; }

        public AspNetHttpResponseInformation(System.Web.HttpResponseBase response, long responseBodySizeInBytes)
        {
            ResponseBodySizeInBytes = responseBodySizeInBytes;
        }
    }
}
