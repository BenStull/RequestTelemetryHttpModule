using System;
using System.Globalization;
using BenStull.HttpRequestTelemetry.Domain.HttpRequest;
using BenStull.HttpRequestTelemetry.Domain.HttpResponse;
using BenStull.HttpRequestTelemetry.Domain.Telemetry;

namespace BenStull.HttpRequestTelemetry.Model.Telemetry.RequestCollectors
{
    /// <summary>
    ///     Tracks telemetry around response body size
    ///     Should be called when request processing is complete
    /// </summary>
    public class ResponseSizeTelemetryCollector : IHttpResponseTelemetryCollector
    {
        private long _totalRequests;
        private long _totalData;
        private long _maxResponseSizeEncountered = long.MinValue;
        private long _minResponseSizeEncountered = long.MaxValue;

        private readonly object _syncObj = new object();

        public void CollectResponseTelemetry(IHttpRequestInformation requestInformation,
            IHttpResponseInformation responseInformation,
            IHttpRequestTelemetry requestTelemetry)
        {
            long totalRequests;
            long totalData;
            long minResponseSizeEncountered;
            long maxResponseSizeEncountered;

            // Capture and update the neweste metrics based on this request
            lock (_syncObj)
            {
                ++_totalRequests;
                totalRequests = _totalRequests;

                _totalData += responseInformation.ResponseBodySizeInBytes;
                totalData = _totalData;

                _maxResponseSizeEncountered = Math.Max(_maxResponseSizeEncountered,
                    responseInformation.ResponseBodySizeInBytes);
                maxResponseSizeEncountered = _maxResponseSizeEncountered;

                _minResponseSizeEncountered = Math.Min(_minResponseSizeEncountered,
                    responseInformation.ResponseBodySizeInBytes);
                minResponseSizeEncountered = _minResponseSizeEncountered;
            }

            AddTotalRequestsTelemetry(totalRequests, requestTelemetry);
            AddCurrentRequestResponseSizeTelemetry(responseInformation.ResponseBodySizeInBytes, requestTelemetry);
            AddMaxEncounteredTelemetry(maxResponseSizeEncountered, requestTelemetry);
            AddMinEncounteredTelemetry(minResponseSizeEncountered, requestTelemetry);
            AddAverageResponseSizeTelemetry(totalData, totalRequests, requestTelemetry);
        }

        private void AddTotalRequestsTelemetry(long numberOfRequests, IHttpRequestTelemetry requestTelemetry)
        {
            var dataPoint = new HttpRequestTelemetryDataPoint {
                MetricName = "Total Requests",
                Description = "Total number of requests the server has processed",
                Value = numberOfRequests.ToString()
            };

            requestTelemetry.AddDataPoint(dataPoint);
        }

        private void AddCurrentRequestResponseSizeTelemetry(long responseInformationResponseBodySizeInBytes, IHttpRequestTelemetry requestTelemetry)
        {
            var dataPoint = new HttpRequestTelemetryDataPoint {
                MetricName = "Response Body Size",
                Description = "Size of response body for this request",
                Unit = "bytes",
                Value = responseInformationResponseBodySizeInBytes.ToString()
            };

            requestTelemetry.AddDataPoint(dataPoint);
        }

        private void AddMaxEncounteredTelemetry(long maxResponseSizeEncountered, IHttpRequestTelemetry requestTelemetry)
        {
            var dataPoint = new HttpRequestTelemetryDataPoint {
                MetricName = "Maximum Response Body Size",
                Description = "Max size of response body out of all requests encountered so far",
                Unit = "bytes",
                Value = maxResponseSizeEncountered.ToString()
            };

            requestTelemetry.AddDataPoint(dataPoint);
        }

        private void AddMinEncounteredTelemetry(long minResponseSizeEncountered, IHttpRequestTelemetry requestTelemetry)
        {
            var dataPoint = new HttpRequestTelemetryDataPoint {
                MetricName = "Minimum Response Body Size",
                Description = "Min size of response body out of all requests encountered so far",
                Unit = "bytes",
                Value = minResponseSizeEncountered.ToString(CultureInfo.InvariantCulture)
            };

            requestTelemetry.AddDataPoint(dataPoint);
        }

        private void AddAverageResponseSizeTelemetry(long totalData, long totalRequests, IHttpRequestTelemetry requestTelemetry)
        {
            var dataPoint = new HttpRequestTelemetryDataPoint {
                MetricName = "Average Response Body Size",
                Description = "Average size of response body for all requests so far",
                Unit = "bytes",
                Value = Math.Floor((double)totalData / totalRequests).ToString(CultureInfo.InvariantCulture)
            };

            requestTelemetry.AddDataPoint(dataPoint);
        }
    }
}