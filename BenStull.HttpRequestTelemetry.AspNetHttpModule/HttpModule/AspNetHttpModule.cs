using System;
using System.Collections.Generic;
using System.Web;
using BenStull.HttpRequestTelemetry.Domain.HttpResponse;

namespace BenStull.HttpRequestTelemetry.AspNetHttpModule.HttpModule
{
    /// <summary>
    /// ASP.NET Http Module for collecting telemetry data
    /// </summary>
    public class AspNetHttpModule : IHttpModule
    {
        private static IList<IHttpResponseTelemetryCollector> _responseTelemetryCollectors;

        static AspNetHttpModule()
        {
            _responseTelemetryCollectors = new List<IHttpResponseTelemetryCollector>();
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequest;
        }

        public void BeginRequest(object sender, EventArgs e)
        {
            var application = (HttpApplication) sender;
            var response = application.Response;
            
            // All examination and collection of response telemetry is done in the response filter so we can examine the output stream
            application.Response.Filter = new ResponseStreamFilter(response.Filter, new Model.Telemetry.HttpRequestTelemetry(), new HttpResponseWrapper(response), _responseTelemetryCollectors);
        }

        public void Dispose()
        {
        }
    }
}