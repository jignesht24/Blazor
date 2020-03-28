## Introduction
 
A Route is a URL pattern and Routing is a pattern matching process that monitors the requests and determines what to do with each request.
 
Blazor server app use ASP.net Core Endpoint Routing. Using MapBlazorHub extension method of endpoint routing, ASP.net Core is start to accept the incoming connection for Blazor component. The Blazor client app provides the client-side Routing. The router is configured in Blazor client app in App.cshtml file.
 
Blazor Client app
```
<Router AppAssembly="@typeof(Program).Assembly"/>  
```
Blazor Server app 
```
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)  
{  
....  
....  
....  
  
    app.UseRouting();  
  
    app.UseEndpoints(endpoints =>  
    {  
        endpoints.MapBlazorHub();  
        endpoints.MapFallbackToPage("/_Host");  
    });  
}  
```
The Blazor Server app allows to set fallback route. It operates with low priority in routing matching. The fallback route is only considered when other routes are not matched. The fallback route is usually defined in _Host.cshtml component.
 
### @page directive
 
Using @page directive, you can define the routing in Blazor component. The @page directives are internally converted into RouteAttribute when template is compiled.
```
@page "/route1"  
```
In Blazor, Component can have multiple routes. If we require that component can render from multiple route values, we need to define all routes with multiple @page directives.
```
@page "/multiple"  
@page "/multiple1"  
```
If you have defined class only component, you can use RouteAttribute.
```
using Microsoft.AspNetCore.Components;  
  
namespace BlazorServerApp.Pages  
{  
    [Route("/classonly")]  
    public class ClassOnlyComponent: ComponentBase  
    {  
       ...  
    }  
}  
```
### Route Template
 
The Router mechanism allows to define the routing for each component. The route template is defined in App.razor file. Here, we can define default layout page and default route data.
```
<Router AppAssembly="@typeof(Program).Assembly">  
    <Found Context="routeData">  
        <RouteView RouteData="@routeData" DefaultLayout="@typeof(MainLayout)" />  
    </Found>  
    <NotFound>  
        <LayoutView Layout="@typeof(MainLayout)">  
            <p>Sorry, there's nothing at this address.</p>  
        </LayoutView>  
    </NotFound>  
</Router>  
```
In above code, three components are defined under Router component: Found, NotFound and RouteView. The RouteView component receive the route data and default layout. Blazor routing mechanism render the Found component, if any route matched else render NotFound component. The NotFound component allows us to provide custom message when route or content not found.
 
### Handle Parameter in Route Template
 
Route may have parameter. The parameters can be defined using curly braces within routing template in both @page directive or RouteAttribute. The route parameters are automatically bind with component parameters by matching the name. This matching is case insensitive.
```
<h3>Route Constraints Example</h3>  
@page "/routepara/{Name}"  
  
<h4>Name : @Name </h4>  
  
@code {  
    [Parameter]  
    public string Name { get; set; }  
}  
```
Current version of Blazor does not supports the optional parameter , so you must pass parameter in above example.
 
### Route constraints
 
The Blazor routing is also allows Route constraints. It enforces type matching between route parameter and route data. Current version of Blazor supports few route constraints but might supports many more route constraints in future.
```
<h3>Route Constraints Example</h3>  
@page "/routecons/{Id:guid}"  
  
<h4>Id : @Id </h4>  
  
@code {  
    [Parameter]  
    public Guid Id { get; set; }  
}  
```
Following route constraints are supported by Blazor 

| Constraint | Invariant culture matching | Example |
| ---------- | ---------------------------| ------- |
| int | Yes | {id:int} | 
| long | Yes | {id:long} | 
| float | Yes | {mrp:float} | 
| double | Yes | {mrp:double} | 
| decimal | Yes | {mrp:decimal} | 
| guid | No | {id:guid} | 
| bool | No | {enabled:bool} | 
| datetime | Yes | {birthdate:datetime} | 

NavLink Component
 
Blazor provides NavLink component that generate HTML hyperlink element and it handle the toggle of active CSS class based on NavLink component href match with current URL.
 
There are two options for assign to Match attribute of NavLink component
* NavLinkMatch.All: Active when it matches the entire current URL
* NavLinkMatch.Prefix: It is a default option. It active when it matches any prefix of the current URL
The NavLink component render as the anchor tag. You can also include the target attribute.
 
Programmatically navigate one component to another component
 
Blazor is also allow to navigate from one component to another component programmatically using Microsoft.AspNetCore.Components.NavigationManager. The NavigationManager service provides following events and properties.

| Event / Method | Description |
| ---------- | ---------------------------|
| NavigateTo | It navigates to specified URI. It takes parameter \"forceload\", if it parameter is set to true, client-side routing is bypassed and the browser is forced to load new page | 
| ToAbsoluteUri |  It converts relative URI to absolute URI |
| ToBaseRelativePath | Returns relative URI with base URI prefix |
| NotifyLocationChanged | This event is fired when browser location has changed |
| EnsureInitialized | This method allows derived class to lazy self-initialized |
| Initialize | This method set base URI and current URI before they are used |
| NavigateToCore | Navigate to specified URI. This is abstract method hence it must implement in derived class |

| Properties | Description |
| ---------- | ---------------------------|
| BaseUri | Get and set the current base URI. It allows represent as an absolute URI end with slash | 
| Uri  | Get and set the current URI. It allows represent as an absolute URI | 

To navigate URI using NavigationManager service, you must inject the service using @inject directive in component. Using the NavigateTo method, you can navigate from one component to another component.
```
@page "/navexample"  
@inject NavigationManager UriHelper  
<h3>Navigation Example</h3>  
Navigate to other component <a href="" @onclick="NavigatetoNextComponent">Click here</a>  
  
@code {  
    void NavigatetoNextComponent()  
    {  
        UriHelper.NavigateTo("newcomponent");  
    }  
}  
```
You can also capture query string or query parameter value when redirect to another component. Using QueryHelpers class, we can access query string and query parameter of the component. The QueryHelpers.ParseQuery method extract the value from the query string. This method returns the dictionary of type Dictionary<string, StringValues> that contains all query parameter of the route.
```
<h3>New Component with Parameter</h3>  
@page "/newcomponentwithpara"  
@inject NavigationManager UriHelper  
@using Microsoft.AspNetCore.WebUtilities;  
  
<p> Parameter value : @Name</p>  
  
@code {  
    public string Name { get; set; }  
    protected override void OnInitialized()  
    {  
        var uri = UriHelper.ToAbsoluteUri(UriHelper.Uri);  
        Name = QueryHelpers.ParseQuery(uri.Query).TryGetValue("name", out var type) ? type.First() : "";  
    }  
}  
```
### Summary
 
Blazor provides rich routing. Blazor server app use ASP.net Core Endpoint Routing. It provides almost all features including route parameter, route constraints. It also provides built-in component like NavLink that helps to generate menu item. It provides built-in services that help us to navigate from one component to another component. However, there are some limitation like route parameter does not support optional parameter and it supports limited number of route constraints.

