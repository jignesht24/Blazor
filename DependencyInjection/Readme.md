## Introduction

The Dependency Injection is a design pattern that helps create a loosely coupled application design. It provides greater maintainability, testability, and reusability. In this pattern, the dependencies that are required to complete the task are provided from the outer world rather than created inside the class. There are various ways to inject the dependencies: Constructor, Setter (property based) and interfaced based injection.
 
The Blazor supports Dependency injection in both the Blazor server and Blazor WebAssembly app. The Blazor provides built-in services, and you can also build a custom service and use it with a component by injecting them via DI.
 
### Service lifetime

The services configure in the Blazor app with a specific service lifetime. Basically, there are three lifetimes exist: Scoped, Singleton and Transient but all three lifetimes are not supported by Blazor. Some of them supported in Blazor server app and some of them supported in Blazor WebAssembly app. The following are the service lifetimes:
 
#### Scoped

The service instance is created per request. It is not supported in the Blazor WebAssembly app. In the Blazor server app, scoped of the service is per connection so, scoped services behave like a Singleton service.

#### Singleton

It creates and shares the service instance through the application life. Both the Blazor server and WebAssembly app support this lifetime of service.
 
#### Transient

The service instance is created every time when it requested.
 
### Built-in services

The Blazor provides built-in services (referred to as framework services). The framework services are automatically added to the app’s service collection with a predefined service lifetime. The following are framework services:
 
#### HttpClient

This service provides the method for sending and receiving HTTP requests and responses. This service is added as a Singleton service in the Blazor server app.
 
#### IJSRuntime

This service provides a method for JavaScript interop call. The single instance of service created and shared per application in Blazor WebAssembly app and service instance created and shared per connection in the Blazor server app.
 
#### NavigationManager

This service provides a method to work with URI and navigation state. The single instance of service created and shared per application in Blazor WebAssembly app and service instance created and shared per connection in the Blazor server app.
 
### Register custom service

The service in ASP.net core Blazor is a simple class that contains specifics methods. To use custom service in component, first, you need to register custom service in the ConfigureServies method of the Startup class. The IServiceCollection contains a list of service descriptor objects and a custom service needs to be added to this service collection by using the "Add{lifetime} method (for example, the service is added as a singleton lifetime in the service collection using the AddSingleton method.

```
public void ConfigureServices(IServiceCollection services)  
{  
      services.AddSingleton<IHelloWorldService, HelloWorldService>();  
      services.AddSingleton<ComplexService>();  
}  
```

### Inject service into component
 
Using @inject directive or inject attribute, you can inject services to the component. You can also inject multiple different services by using multiple @inject directives.
 
Syntex
```
@inject ServiceType ServiceInstanceName 
```
In the following example, HelloWorldService is injected into a component using @inject directive.
```
@page "/testdata"  
@inject IHelloWorldService HelloWorldService  
...  
@code {  
    private string name;  
    protected override async Task OnInitializedAsync()  
    {  
        name = HelloWorldService.GetName();  
...  
...  
    }  
}  
```

Using inject attribute, you can inject a service into the component 's code behind or class-only component. In the following example,  a service is injected into the component 's code behind using the inject attribute.
```
namespace BlazorWebAssemblyApp.Pages  
{  
    using BlazorWebAssemblyApp.Service;  
    using Microsoft.AspNetCore.Components;  
    public class EmployeeComponent : ComponentBase  
    {  
        [Inject]  
        protected IHelloWorldService helloWorldService { get; set; }  
    }  
} 
```

When services are not registered to the service container, the Blazor component is not able to find service and the Blazor framework raise an exception as the following.
 
The custom service might require to use additional service resources. You can inject the additional services as dependency injection. Here you must use Constructor injection. The required service is automatically added in parameter when the service instance created. It is not allowed to use inject attributes to inject service dependency within another service.
 
In the following example, the HttpClient default service is injected into the ComplexService.
```
public class ComplexService  
{  
    public ComplexService(HttpClient httpClient)  
    {  
  
    }  
    public string GetName()  
    {  
        return "Jignesh Trivedi";  
    }  
}  
```

### Summary

Dependency injection design pattern allows us to create a loosely coupled application so that it provides greater maintainability, testability and also reusability. There is a built-in support of dependency injection in Blazor. In this article, we learned about how to inject and use a DI service in the Blazor app and component.
