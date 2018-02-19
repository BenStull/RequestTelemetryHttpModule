namespace BenStull.HttpRequestTelemetry.Domain.HttpResponse
{
    /// <summary>
    ///     Contains all http response information required for collecting telemetry
    /// </summary>
    public interface IHttpResponseInformation
    {
        long ResponseBodySizeInBytes { get; }
    }
}