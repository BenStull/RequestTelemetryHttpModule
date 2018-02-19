using System.Collections.Generic;
using BenStull.HttpRequestTelemetry.Domain.Telemetry;
using BenStull.HttpRequestTelemetry.Model.Telemetry;
using Moq;
using Xunit;

namespace BenStull.HttpRequestTelemetry.Model.UnitTests.Telemetry
{
    public class TelemetryHtmlComposer_UnitTests
    {
        [Fact]
        public void When_ComposeTelemetryHtml_WithNoTelemetry_ReturnsValidHtml()
        {
            var htmlComposer = new TelemetryHtmlComposer();

            var telemetryMock = new Mock<IHttpRequestTelemetry>();
            telemetryMock.SetupGet(a => a.TelemetryDataPoints).Returns(new List<IHttpRequestTelemetryDataPoint>());

            var html = htmlComposer.ComposeTelemetryHtml(telemetryMock.Object);

            Assert.False(string.IsNullOrEmpty(html));
        }
    }
}