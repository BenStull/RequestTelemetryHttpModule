namespace BenStull.HttpRequestTelemetry.Domain.Telemetry
{
    public interface ITelemetryHtmlComposer
    {
        string ComposeTelemetryHtml(IHttpRequestTelemetry telemetry);
    }
}