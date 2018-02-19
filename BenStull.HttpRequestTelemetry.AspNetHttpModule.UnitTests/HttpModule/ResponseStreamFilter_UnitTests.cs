using System.Collections.Generic;
using System.IO;
using System.Web;
using BenStull.HttpRequestTelemetry.AspNetHttpModule.HttpModule;
using BenStull.HttpRequestTelemetry.AspNetHttpModule.HttpRequest;
using BenStull.HttpRequestTelemetry.Domain.HttpResponse;
using BenStull.HttpRequestTelemetry.Domain.Telemetry;
using Moq;
using Xunit;

namespace BenStull.HttpRequestTelemetry.Model.UnitTests.HttpModule
{
    public class ResponseStreamFilter_UnitTests
    {
        private ResponseStreamFilter GetResponseStreamFilter(MemoryStream originalResponseStream)
        {
            return new ResponseStreamFilter(originalResponseStream, new Mock<IHttpRequestTelemetry>().Object,
                new HttpRequestInformation(new Mock<HttpContextBase>().Object), new Mock<HttpResponseBase>().Object,
                new List<IHttpResponseTelemetryCollector>(), new Mock<ITelemetryHtmlComposer>().Object);
        }

        [Fact]
        public void When_CloseCalled_OriginalResponseStreamIsClosed()
        {
            var originalResponseStream = new MemoryStream();
            Assert.True(originalResponseStream.CanWrite);

            var responseStreamFilter = GetResponseStreamFilter(originalResponseStream);

            responseStreamFilter.Close();

            Assert.False(originalResponseStream.CanWrite);
        }

        [Fact]
        public void When_WriteCalled_OriginalResponseStreamWriteIsCalled()
        {
        }
    }
}