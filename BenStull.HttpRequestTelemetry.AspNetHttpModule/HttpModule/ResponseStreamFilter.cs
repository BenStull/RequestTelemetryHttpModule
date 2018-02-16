using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using BenStull.HttpRequestTelemetry.Domain.HttpResponse;
using BenStull.HttpRequestTelemetry.Domain.Telemetry;

namespace BenStull.HttpRequestTelemetry.AspNetHttpModule.HttpModule
{
    public class ResponseStreamFilter : Stream
    {
        private readonly HttpResponseBase _httpResponse;
        private readonly Stream _originalResponseStream;
        private readonly IHttpRequestTelemetry _telemetry;
        private readonly IList<IHttpResponseTelemetryCollector> _telemetryCollectors;
        private long _responseBodyLength;
        private StreamWriter _modifiedResponseStreamWriter;

        public ResponseStreamFilter(Stream responseStream, IHttpRequestTelemetry telemetry,
            HttpResponseBase httpResponse, IList<IHttpResponseTelemetryCollector> telemetryCollectors)
        {
            _originalResponseStream = responseStream;
            _telemetry = telemetry;
            _httpResponse = httpResponse;
            _telemetryCollectors = telemetryCollectors;
            _modifiedResponseStreamWriter = new StreamWriter(responseStream);
        }

        public override bool CanRead => false;
        public override bool CanSeek => false;
        public override bool CanWrite => true;
        public override long Length => _originalResponseStream.Length;

        public override long Position
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            _originalResponseStream.Close();
        }
    }
}