namespace BenStull.HttpRequestTelemetry.Domain.Telemetry
{
    /// <summary>
    /// Contains telemetry information about the app's handling of an http request
    /// </summary>
    public interface IHttpRequestTelemetry
    {
        /// <summary>
        ///     How long it the request took to process this request, in milliseconds
        /// </summary>
        uint ProcessingTimeInMilliseconds { get; set; }

        /// <summary>
        ///     Total time spent collecting telemetry on this request, in milliseconds
        /// </summary>
        uint TelemetryProcessingTimeInMilliseconds { get; set; }

        /// <summary>
        ///     The size of the response body, in bytes
        /// </summary>
        uint ResponseBodySizeInBytes { get; set; }

        /// <summary>
        ///     The minimum response body size any request has had so far, in bytes
        /// </summary>
        uint MinimumResponseBodySizeEncounteredInBytes { get; set; }

        /// <summary>
        ///     The maximum response body size any request has had so far, in bytes
        /// </summary>
        uint MaximumResponseBodySizeEncounteredInBytes { get; set; }

        /// <summary>
        ///     The average response body size across all request so far, in bytes
        /// </summary>
        uint AverageResponseBodySizeEncounteredInBytes { get; set; }
    }
}