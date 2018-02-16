using System.Collections.Generic;
using System.IO;
using System.Web;
using BenStull.HttpRequestTelemetry.AspNetHttpModule.HttpModule;
using BenStull.HttpRequestTelemetry.Domain.HttpResponse;
using Moq;
using Xunit;

namespace BenStull.HttpRequestTelemetry.Model.UnitTests.HttpModule
{
    public class ResponseStreamFilter_UnitTests
    {

        [Fact]
        public void When_CloseCalled_OriginalResponseStreamIsClosed()
        {
            var originalResponseStream = new MemoryStream();
            Assert.True(originalResponseStream.CanWrite);

            var responseStreamFilter = GetResponseStreamFilter(originalResponseStream);

            responseStreamFilter.Close();

            Assert.False(originalResponseStream.CanWrite);
        }

        private ResponseStreamFilter GetResponseStreamFilter(MemoryStream originalResponseStream)
        {
            return new ResponseStreamFilter(originalResponseStream, new Telemetry.HttpRequestTelemetry(), new Mock<HttpResponseBase>().Object, new List<IHttpResponseTelemetryCollector>());
        }
    }
}