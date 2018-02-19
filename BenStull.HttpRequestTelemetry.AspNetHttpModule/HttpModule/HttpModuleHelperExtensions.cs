using System.Web;
using BenStull.HttpRequestTelemetry.AspNetHttpModule.HttpRequest;
using BenStull.HttpRequestTelemetry.Domain.HttpRequest;
using BenStull.HttpRequestTelemetry.Domain.Telemetry;

namespace BenStull.HttpRequestTelemetry.AspNetHttpModule.HttpModule
{
    public static class HttpModuleHelperExtensions
    {
        public static IHttpRequestTelemetry GetRequestTelemetryObject(this HttpContextBase context)
        {
            const string itemId = "_benStullHttpRequestTelemetry";

            if (context.Items[itemId] != null)
                return (IHttpRequestTelemetry) context.Items[itemId];

            var requestTelemetry = new Model.Telemetry.HttpRequestTelemetry();
            context.Items[itemId] = requestTelemetry;
            return requestTelemetry;
        }

        public static IHttpRequestInformation GetRequestInformationObject(this HttpContextBase context)
        {
            const string itemId = "_benStullHttpRequestInformation";

            if (context.Items[itemId] != null)
                return (IHttpRequestInformation) context.Items[itemId];

            var requestInformation = new HttpRequestInformation(context);
            context.Items[itemId] = requestInformation;
            return requestInformation;
        }
    }
}