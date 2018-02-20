[![Build status](https://ci.appveyor.com/api/projects/status/y861916tctiqj89g?svg=true)](https://ci.appveyor.com/project/BenStull/requesttelemetryhttpmodule)

# Request Telemetry HttpModule
An ASP.NET Http Module that collects information and metrics about requests/responses and displays it within the returned HTML page

## What is this module?
This project leverages the [IHttpModule](https://docs.microsoft.com/en-us/dotnet/api/system.web.ihttpmodule) interface to hook into the ASP.NET pipeline in order to collect telemetry data about all HTTP requests and responses.

If the response's ContentType is text/html, the module will inject a basic HTML table that lists all telemetry metrics that have been collected.

This module can give insight into specific requests/responses, as well as the pattern of traffic that your app is encountering.

## Installation Instructions

This will install the Http Module as a global IIS module that will run for all web sites running on the server.

1. Download the zip file from the latest release
2. Extract the archive to the installation location you prefer
3. Open an elevated powershell session and navigate to the folder where you extracted the files
4. Run Unblock-File .\Install-HttpTelemetryModule.ps1
5. Run the [Install-HttpTelemetryModule.ps1](./BenStull.HttpRequestTelemetry.AspNetHttpModule/Install-HttpTelemetryModule.ps1) powershell script

Note: This script is not signed so you may need to adjust your PowerShell execution policy via [Set-ExecutionPolicy](https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.security/set-executionpolicy?view=powershell-6)

## Uninstall Instructions

This will install the Http Module as a global IIS module that will run for all web sites running on the server.

1. Open an elevated powershell session and navigate to the folder where you extracted the files
2. Run Unblock-File .\Uninstall-HttpTelemetryModule.ps1
3. Run the [Uninstall-HttpTelemetryModule.ps1](./BenStull.HttpRequestTelemetry.AspNetHttpModule/Uninstall-HttpTelemetryModule.ps1) powershell script

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
