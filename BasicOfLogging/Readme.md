### Introduction
Logging is a very critical and essential part of any software. It helps us in the investigation of the essence of problems. The .net core framework has built-in support for [logging APIs](https://github.com/jignesht24/Aspnetcore/tree/master/Logging%20with%20.net%20core%202.0) that is able to work logging providers. 

[Blazor](https://github.com/jignesht24/Blazor) is a new experimental .NET web framework using C#/Razor and HTML that runs in the browser with WebAssembly. Blazor enables full stack web development with the consistency, stability, and productivity of .NET. The many changes are going on in this framework and also many new extension are available that work with Blazor.

In this article, we learn blazor extension for logging. Now [logging extension](https://github.com/BlazorExtensions/Logging) for Blazor is available. This extension has implementation for the Microsoft.Extensions.Logging to support the ILogger interface with Blazor code. When this extension is configured, all the log appear in the browser's console. This Blazor extention provide nearly same feature as Microsoft.Extensions.Logging provides for .net core. 

### How to configure provider

Logging extension are not added with default project template. This extension is available on nuget package manager. Using following command, we can add logging provide to our project.

#### Using Package Manager 
```
PM > Install-Package Blazor.Extensions.Logging -Version 0.1.2
```
#### Using .net CLI
```
>dotnet add package Blazor.Extensions.Logging --version 0.1.2
```
Next step is to register this extension service by adding the logging service to BrowserServiceProvider. Using following code snippet, we can configure browser console logger.
```
var serviceProvider = new BrowserServiceProvider(services =>
{
    // Add Blazor.Extensions.Logging.BrowserConsoleLogger
    services.AddLogging(builder => builder
        .AddBrowserConsole() // Register the logger with the ILoggerBuilder
    );
});
```
#### How to Use
With .net core, Logger is available as DI (dependency injection) to the every controller by default. In the Blazor, there is no controller. So we need to inject the logger service to Blazor component. Using following code we can consume the logger in a Blazor component.

##### Using cshtml
```
@using Microsoft.Extensions.Logging;
@inject ILogger<Index> logger
@page "/"

<h1>Test Logger</h1>

@functions {
	protected override async Task OnInitAsync()
	{
	    logger.LogDebug("OnInitAsync");
	}
}
```

##### Outside the cshtml
```
[Inject]
protected ILogger<MyTestClass> logger { get; set; }

public void LogInfo(string message)
{
  logger.LogDebug(message);
}
```
Current version of Blazor and MEL (Microsoft Extensions Logging) is only support the code based configuration and lightweight Microsoft Extensions Configuration based configuration is not supported. 

#### Log Level

Blazor extension for logger is support all the log levels defined in MEL (Microsoft Extensions Logging). Using SetMinimumLevel method of ILoggingBuilder interface, we can set minimum log level. 

Example 

In following example, I have set minimum log level to Information, so log level below to information (i.e. trace & debug) are ignored for logging.
```
using Blazor.Extensions.Logging;
using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace BlazorDemoApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            var serviceProvider = new BrowserServiceProvider(services =>
            {
                // Add Blazor.Extensions.Logging.BrowserConsoleLogger
                services.AddLogging(builder => builder
                .AddBrowserConsole() // Register the logger with the ILoggerBuilder
                .SetMinimumLevel(LogLevel.Information) // Set the minimum log level to Information
            );
            });

            new BrowserRenderer(serviceProvider).AddComponent<App>("app");
        }
    }
}
```