using BenStull.HttpRequestTelemetry.Domain.Telemetry;

namespace BenStull.HttpRequestTelemetry.Model.Telemetry
{
    public class HttpRequestTelemetry : IHttpRequestTelemetry
    {
        public uint ProcessingTimeInMilliseconds { get; set; }
        public uint TelemetryProcessingTimeInMilliseconds { get; set; }
        public uint ResponseBodySizeInBytes { get; set; }
        public uint MinimumResponseBodySizeEncounteredInBytes { get; set; }
        public uint MaximumResponseBodySizeEncounteredInBytes { get; set; }
        public uint AverageResponseBodySizeEncounteredInBytes { get; set; }
    }
}