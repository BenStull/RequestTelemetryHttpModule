using System;
using BenStull.HttpRequestTelemetry.Domain.HttpRequest;
using BenStull.HttpRequestTelemetry.Domain.HttpResponse;
using BenStull.HttpRequestTelemetry.Domain.Telemetry;
using BenStull.HttpRequestTelemetry.Model.Telemetry.RequestCollectors;
using Moq;
using Xunit;

namespace BenStull.HttpRequestTelemetry.Model.UnitTests.Telemetry
{
    public class TotalProcessingTimeTelemetryCollector_UnitTests
    {
        [Fact]
        public void When_CollectCalled_UsesRequestStartTime()
        {
            var collector = new TotalProcessingTimeTelemetryCollector();
            var requestInformation = new Mock<IHttpRequestInformation>();

            var telemetry = new Mock<IHttpRequestTelemetry>();

            collector.CollectResponseTelemetry(requestInformation.Object, new Mock<IHttpResponseInformation>().Object,
                telemetry.Object);

            requestInformation.VerifyGet(a => a.RequestStartTime);
        }

        [Fact]
        public void When_CollectCalled_DataPointIsPositive()
        {
            var collector = new TotalProcessingTimeTelemetryCollector();
            var requestInformation = new Mock<IHttpRequestInformation>();
            requestInformation.SetupGet(a => a.RequestStartTime)
                .Returns(DateTime.Now.Subtract(TimeSpan.FromMilliseconds(1000)));

            var telemetry = new Mock<IHttpRequestTelemetry>();
            string value = null;
            telemetry.Setup(a => a.AddDataPoint(It.IsAny<IHttpRequestTelemetryDataPoint>()))
                .Callback<IHttpRequestTelemetryDataPoint>(a => value = a.Value);

            collector.CollectResponseTelemetry(requestInformation.Object, new Mock<IHttpResponseInformation>().Object,
                telemetry.Object);

            Assert.False(string.IsNullOrEmpty(value));
            Assert.False(value.StartsWith("-"));
        }

        [Fact]
        public void When_CollectCalled_AddsDataPoint()
        {
            var collector = new TotalProcessingTimeTelemetryCollector();
            var requestInformation = new Mock<IHttpRequestInformation>();

            var telemetry = new Mock<IHttpRequestTelemetry>();

            collector.CollectResponseTelemetry(requestInformation.Object, new Mock<IHttpResponseInformation>().Object,
                telemetry.Object);

            telemetry.Verify(a => a.AddDataPoint(It.IsAny<IHttpRequestTelemetryDataPoint>()));
        }
    }
}