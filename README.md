[![Build status](https://ci.appveyor.com/api/projects/status/y861916tctiqj89g?svg=true)](https://ci.appveyor.com/project/BenStull/requesttelemetryhttpmodule)

# Request Telemetry HttpModule
An ASP.NET Http Module that collects information and metrics about requests/responses and displays it within the returned HTML page

## What is this module?
This project leverages the [IHttpModule](https://docs.microsoft.com/en-us/dotnet/api/system.web.ihttpmodule) interface to hook into the ASP.NET pipeline in order to collect telemetry data about all HTTP requests and responses.

If the response is text/html, the module will inject a basic HTML table that lists all telemetry metrics that have been collected.

This module can give insight into specific requests/responses, as well as the pattern of traffic that your app is encountering.

## Installation Instructions

1. Download the zip file from [latest release](/releases/latest)

### Option 1: Machine-wide on IIS
Run the [Install-HttpTelemetryModule.ps1](./BenStull.HttpRequestTelemetry.AspNetHttpModule/Install-HttpTelemetryModule.ps1) script on the IIS server in an elevated cmd prompt.

### Option 2: Single app (IIS-hosted)
1. Add the following to the modules node under system.Webserver in your web.config
```xml
    <modules>
      <remove name="HttpRequestTelemetry" />
      <add name="HttpRequestTelemetry" type="BenStull.HttpRequestTelemetry.AspNetHttpModule.HttpModule.AspNetHttpModule,BenStull.HttpRequestTelemetry.AspNetHttpModule,Version=1.0.0.0,Culture=neutral,PublicKeyToken=96b62749fde600bc" preCondition="integratedMode,managedHandler" />
    </modules>
```

2. Add the following to the httpModules node under system.web in your web.config
```xml
    <httpModules>
      <add name="HttpRequestTelemetry" type="BenStull.HttpRequestTelemetry.AspNetHttpModule.HttpModule.AspNetHttpModule,BenStull.HttpRequestTelemetry.AspNetHttpModule,Version=1.0.0.0,Culture=neutral,PublicKeyToken=96b62749fde600bc" />
    </httpModules>
```

See the [Demo project web.config](./BenStull.HttpRequestTelemetry.AspNetHttpModule.Demo/Web.config) for an example

## Currently Tracked Metrics
- Number of HTTP requests made since app was loaded
- Time to process response
- Overhead time that the module spent collecting the telemetry data
- Largest response body size returned for any request since the app was loaded
- Smallest response body size returned for any request since the app was loaded
- Average response body size returned for all requests since the app was loaded

## Can I add my own metrics?
Absolutely.  To collect a metric about the HTTP Request, implement a new [IHttpRequestTelemetryCollector](./BenStull.HttpRequestTelemetry.Domain/HttpRequest/IHttpRequestTelemetryCollector.cs) and add it to the collection in the [AspNetHttpModule](./BenStull.HttpRequestTelemetry.AspNetHttpModule/HttpModule/AspNetHttpModule.cs)

To add a metric about the HTTP Response, implement a new [IHttpResponseTelemetryCollector](./BenStull.HttpRequestTelemetry.Domain/HttpResponse/IHttpResponseTelemetryCollector.cs) and add it to the collection in the [AspNetHttpModule](./BenStull.HttpRequestTelemetry.AspNetHttpModule/HttpModule/AspNetHttpModule.cs).

You may need to expose additional data about the HTTP request or response on the [HttpRequestInformation](./BenStull.HttpRequestTelemetry.AspNetHttpModule/HttpRequest/HttpRequestInformation.cs) or [HttpResponseInformation](./BenStull.HttpRequestTelemetry.AspNetHttpModule/HttpResponse/HttpResponseInformation.cs) objects.

## Can this be used on ASP.NET/OWIN self-host?

This project has been designed in an extensible way that should make it easy to adapt to any .NET HTTP pipeline.  Just replace the [BenStull.HttpRequestTelemetry.AspNetHttpModule](./BenStull.HttpRequestTelemetry.AspNetHttpModule) assembly with a similar assembly that impelments the same interfaces and hooks into your HTTP pipeline.  If you'd like to collaborate on this, please add a feature request to the project or send me a message.
