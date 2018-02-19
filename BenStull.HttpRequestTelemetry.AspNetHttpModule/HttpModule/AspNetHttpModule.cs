using System;
using System.Collections.Generic;
using System.Web;
using BenStull.HttpRequestTelemetry.Domain.HttpRequest;
using BenStull.HttpRequestTelemetry.Domain.HttpResponse;
using BenStull.HttpRequestTelemetry.Model.HttpResponse;
using BenStull.HttpRequestTelemetry.Model.HttpRequest;
using BenStull.HttpRequestTelemetry.Model.Telemetry;
using BenStull.HttpRequestTelemetry.Model.Telemetry.RequestCollectors;

namespace BenStull.HttpRequestTelemetry.AspNetHttpModule.HttpModule
{
    /// <summary>
    /// ASP.NET Http Module for collecting telemetry data
    /// 
    /// ASP.NET may create multiple instances of this module to be pooled in a single application
    /// 
    /// </summary>
    public class AspNetHttpModule : IHttpModule
    {
        private static IList<IHttpRequestTelemetryCollector> _requestTelemetryCollectors;
        private static IList<IHttpResponseTelemetryCollector> _responseTelemetryCollectors;

        static AspNetHttpModule()
        {
            _requestTelemetryCollectors = new List<IHttpRequestTelemetryCollector>()
            {

            };

            _responseTelemetryCollectors = new List<IHttpResponseTelemetryCollector>()
            {
                new TotalProcessingTimeTelemetryCollector(),
                new ResponseSizeTelemetryCollector(),
                new TelemetryProcessingTimeTelemetryCollector()
            };
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequest;
        }

        public void BeginRequest(object sender, EventArgs e)
        {
            var application = (HttpApplication)sender;
            BeginRequest(new HttpContextWrapper(application.Context));
        }

        public void BeginRequest(HttpContextBase context)
        {
            var requestInformationObject = context.GetRequestInformationObject();

            using (requestInformationObject.StartTelemetryProcessingOverheadBlock())
            {
                var requestTelemetry = context.GetRequestTelemetryObject();
                var response = context.Response;

                new HttpRequestTelemetryCollectorsCollection(_requestTelemetryCollectors).ExecuteCollectors(
                    requestInformationObject, requestTelemetry);

                // All examination and collection of response telemetry is done in the response filter so we can modify the output stream
                context.Response.Filter = new ResponseStreamFilter(response.Filter, requestTelemetry,
                    requestInformationObject, response,
                    new HttpResponseTelemetryCollectorsCollection(_responseTelemetryCollectors),
                    new TelemetryHtmlComposer());
            }
        }

        public void Dispose()
        {
        }
    }
}