using BenStull.HttpRequestTelemetry.Domain.Telemetry;

namespace BenStull.HttpRequestTelemetry.Model.Telemetry
{
    public class TelemetryHtmlComposer : ITelemetryHtmlComposer
    {
        public string ComposeTelemetryHtml(IHttpRequestTelemetry telemetry)
        {
            return string.Empty;
        }
    }
}