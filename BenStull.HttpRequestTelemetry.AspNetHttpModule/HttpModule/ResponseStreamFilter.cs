﻿using System;
using System.IO;
using System.Web;
using BenStull.HttpRequestTelemetry.AspNetHttpModule.HttpResponse;
using BenStull.HttpRequestTelemetry.Domain.HttpRequest;
using BenStull.HttpRequestTelemetry.Domain.HttpResponse;
using BenStull.HttpRequestTelemetry.Domain.Telemetry;

namespace BenStull.HttpRequestTelemetry.AspNetHttpModule.HttpModule
{
    public class ResponseStreamFilter : Stream
    {
        private readonly HttpResponseBase _httpResponse;
        private readonly Stream _originalResponseStream;
        private readonly IHttpRequestTelemetry _telemetry;
        private readonly IHttpRequestInformation _requestInformation;
        private readonly HttpContextBase _httpContext;
        private readonly IHttpResponseTelemetryCollectorsCollection _telemetryCollectors;
        private readonly ITelemetryHtmlComposer _telemetryHtmlComposer;
        private long _responseBodyLength;
        private bool _telemetryCollectorsExecuted;

        public ResponseStreamFilter(Stream responseStream, IHttpRequestTelemetry telemetry,
            IHttpRequestInformation requestInformation, HttpResponseBase httpResponse, IHttpResponseTelemetryCollectorsCollection telemetryCollectors, ITelemetryHtmlComposer telemetryHtmlComposer)
        {
            _originalResponseStream = responseStream;
            _telemetry = telemetry;
            _requestInformation = requestInformation;
            _httpResponse = httpResponse;
            _telemetryCollectors = telemetryCollectors;
            _telemetryHtmlComposer = telemetryHtmlComposer;
        }

        public override bool CanRead => false;
        public override bool CanSeek => false;
        public override bool CanWrite => true;
        public override long Length => _originalResponseStream.Length;

        public override long Position
        {

            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void Flush()
        {
            _originalResponseStream.Flush();
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
            _responseBodyLength += count;

            if (string.Equals("text/html", _httpResponse.ContentType, StringComparison.InvariantCultureIgnoreCase))
            {
                ProcessHtmlResponseBody(buffer, offset, count);
            } else {
                _originalResponseStream.Write(buffer, offset, count);
            }
        }

        /// <summary>
        /// Examines html responses and injects telemetry data just before the body close tag
        /// </summary>
        private void ProcessHtmlResponseBody(byte[] buffer, int offset, int count)
        {
            using (_requestInformation.StartTelemetryProcessingOverheadBlock())
            {
                var decoder = _httpResponse.ContentEncoding.GetDecoder();
                var charCount = decoder.GetCharCount(buffer, offset, count);
                var chars = new char[charCount];
                decoder.GetChars(buffer, offset, count, chars, 0);
                var bufferAsText = new string(chars);

                int idxBodyCloseTag;

                if ((idxBodyCloseTag =
                        bufferAsText.LastIndexOf("</body>", StringComparison.InvariantCultureIgnoreCase)) != -1
                    || (idxBodyCloseTag =
                        bufferAsText.LastIndexOf("</ body>", StringComparison.InvariantCultureIgnoreCase)) != -1)
                {
                    // This is the end of the response stream and it's time to collect telemetry about the response
                    ExecuteTelemetryCollectors();

                    var telemetryHtml = _telemetryHtmlComposer.ComposeTelemetryHtml(_telemetry);

                    var modifiedResponseChars = bufferAsText.Insert(idxBodyCloseTag, telemetryHtml).ToCharArray();

                    var encoder = _httpResponse.ContentEncoding.GetEncoder();
                    var modifiedResponseData =
                        new byte[encoder.GetByteCount(modifiedResponseChars, 0, modifiedResponseChars.Length, false)];
                    encoder.GetBytes(modifiedResponseChars, 0, modifiedResponseData.Length, modifiedResponseData, 0,
                        false);

                    _originalResponseStream.Write(modifiedResponseData, 0, modifiedResponseData.Length);
                }
                else
                {
                    _originalResponseStream.Write(buffer, offset, count);
                }
            }
        }

        public override void Close()
        {
            // For non-html responses, we still want to track telemetry
            ExecuteTelemetryCollectors();

            _originalResponseStream.Close();
        }

        private void ExecuteTelemetryCollectors()
        {
            _telemetryCollectors.ExecuteCollectors(_requestInformation, new HttpResponseInformation(_httpResponse, _responseBodyLength), _telemetry);
        }
    }
}