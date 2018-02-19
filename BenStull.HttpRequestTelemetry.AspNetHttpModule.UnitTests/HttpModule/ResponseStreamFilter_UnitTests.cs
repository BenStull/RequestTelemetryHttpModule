using System.Collections.Generic;
using System.IO;
using System.Text;
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

        private ResponseStreamFilter GetResponseStreamFilter(MemoryStream originalResponseStream,
            HttpResponseBase response)
        {
            var htmlComposer = new Mock<ITelemetryHtmlComposer>();
            htmlComposer.Setup(a => a.ComposeTelemetryHtml(It.IsAny<IHttpRequestTelemetry>())).Returns("<div></div>");

            return new ResponseStreamFilter(originalResponseStream, new Mock<IHttpRequestTelemetry>().Object,
                new HttpRequestInformation(new Mock<HttpContextBase>().Object), response,
                new List<IHttpResponseTelemetryCollector>(), htmlComposer.Object);
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
            var originalResponseStream = new MemoryStream();
            var responseStreamFilter = GetResponseStreamFilter(originalResponseStream);

            responseStreamFilter.Write(new byte[] {0x0}, 0, 1);

            Assert.Equal(1, originalResponseStream.Length);
        }

        [Fact]
        public void When_WriteCalled_TelemetryHtmlAppendedBeforeBodyCloseTag_lowercase_nospace()
        {
            TelemetryHtmlAppendedBeforeBodyCloseTag_Helper("</body>");
        }

        [Fact]
        public void When_WriteCalled_TelemetryHtmlAppendedBeforeBodyCloseTag_lowercase_space()
        {
            TelemetryHtmlAppendedBeforeBodyCloseTag_Helper("</ body>");
        }

        [Fact]
        public void When_WriteCalled_TelemetryHtmlAppendedBeforeBodyCloseTag_mixedcase_nospace()
        {
            TelemetryHtmlAppendedBeforeBodyCloseTag_Helper("</Body>");
        }

        [Fact]
        public void When_WriteCalled_TelemetryHtmlAppendedBeforeBodyCloseTag_mixedcase_space()
        {
            TelemetryHtmlAppendedBeforeBodyCloseTag_Helper("</ Body>");
        }

        [Fact]
        public void When_WriteCalled_TelemetryHtmlNotAppended_IfNotTextHtmlContentType()
        {
            var originalResponseStream = new MemoryStream();
            var response = new Mock<HttpResponseBase>();
            response.SetupGet(a => a.ContentType).Returns("text/plain");
            response.SetupGet(a => a.ContentEncoding).Returns(Encoding.UTF8);

            var responseStreamFilter = GetResponseStreamFilter(originalResponseStream, response.Object);

            var bodyCloseTagHtml = "</body>";
            var bodyCloseTagHtmlBytes = Encoding.UTF8.GetBytes(bodyCloseTagHtml);

            responseStreamFilter.Write(bodyCloseTagHtmlBytes, 0, bodyCloseTagHtmlBytes.Length);

            Assert.Equal(bodyCloseTagHtmlBytes.Length, originalResponseStream.Length);
        }

        private void TelemetryHtmlAppendedBeforeBodyCloseTag_Helper(string bodyCloseTagHtml)
        {
            var originalResponseStream = new MemoryStream();
            var response = new Mock<HttpResponseBase>();
            response.SetupGet(a => a.ContentType).Returns("text/html");
            response.SetupGet(a => a.ContentEncoding).Returns(Encoding.UTF8);

            var responseStreamFilter = GetResponseStreamFilter(originalResponseStream, response.Object);

            var bodyCloseTagHtmlBytes = Encoding.UTF8.GetBytes(bodyCloseTagHtml);

            responseStreamFilter.Write(bodyCloseTagHtmlBytes, 0, bodyCloseTagHtmlBytes.Length);

            originalResponseStream.Seek(0, SeekOrigin.Begin);
            var modifiedResponse = new StreamReader(originalResponseStream).ReadToEnd();

            Assert.NotEqual(bodyCloseTagHtmlBytes.Length, originalResponseStream.Length);
            Assert.EndsWith(bodyCloseTagHtml, modifiedResponse);
        }
    }
}