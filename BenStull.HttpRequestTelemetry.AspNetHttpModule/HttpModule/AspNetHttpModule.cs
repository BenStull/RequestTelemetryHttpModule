using System;
using System.Collections.Generic;
using System.Web;
using BenStull.HttpRequestTelemetry.Domain.HttpRequest;
using BenStull.HttpRequestTelemetry.Domain.HttpResponse;
using BenStull.HttpRequestTelemetry.Model.Telemetry;

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
            _requestTelemetryCollectors = new List<IHttpRequestTelemetryCollector>();
            _responseTelemetryCollectors = new List<IHttpResponseTelemetryCollector>();
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
            var requestTelemetry = context.GetRequestTelemetryObject();
            var response = context.Response;

            foreach (var collector in _requestTelemetryCollectors)
            {
                collector.CollectResponseTelemetry(requestInformationObject, requestTelemetry);
            }

            // All examination and collection of response telemetry is done in the response filter so we can examine the output stream
            context.Response.Filter = new ResponseStreamFilter(response.Filter, requestTelemetry, requestInformationObject, response, _responseTelemetryCollectors, new TelemetryHtmlComposer());
        }

        public void Dispose()
        {
        }
    }
}