### Introduction
The [component] (https://github.com/jignesht24/Blazor/tree/master/Create%20Component) in Blazor derived from BlazorComponent class. The BlazorComponent class contains some specific life cycle method. Current version of the Blazor (0.3.0) has some limited life cycle methods. 

Please remember that Blazor is an experimental project and not yet in Production. This article is only valid of Blazor 0.3.0.

### OnInit() & OnInitAsync()
These methods are called when the component has been initialized. This method triggered when component has received its initial parameters from parent. The OnInit method call first and then execute OnInitAsync method.  Any asynchronous operations, that require the component to re-render on initialization , it should be written in OnInitAsync method.

#### OnInit
```
@page "/"

<h1>Hello, world!</h1>

Welcome to your new app.
<button class="btn btn-primary" onclick="@ButtonClick">Click me</button>

@functions {
    protected override void OnInit()
    {
        Console.WriteLine("OnInit");
    }
}
```
#### OnInitAsync
```
@page "/async"

<h1>Hello, world!</h1>

Welcome to your new app.
<button class="btn btn-primary" onclick="@ButtonClick">Click me</button>

@functions {
    protected override async Task  OnInitAsync()
    {
        Console.WriteLine("OnInitAsync ");
    }
}
```
### OnParametersSet() & OnParametersSetAsync()
These methods are called after the initialization of component. These methods are called each time when new or updated parameters are received from the parent in the render tree. The properties related to parameter are set before this method called.
```
protected override void OnParametersSet()
{
    
}
```
```
protected override async Task  OnParametersSetAsync()
{
    
}
```
### OnAfterRender & OnAfterRenderAsync
These methods are called after the render of component. When this methods are callled, all the elements and child comopent are populated. These methods can be used to bind event listener that required the elements to be render in the DOM. They can also be used to the initialize the Javascript library.
```
protected override void OnAfterRender()
{
    
}
```
```
protected override async Task OnAfterRenderAsync()
{
    
}
```
### SetParameters
This method is triggered before the parameters are set. This methods can be used to modify the parameters before rendering component. If we overide this method, we need to call base.SetParameters method otherwise Incomming parameter cannot be assigned to the properties on the class hence UI cannot be rendered.
```
public override void SetParameters(ParameterCollection parameters)
{
    
    base.SetParameters(parameters);
}
```
Apart from these life cycle methods, Blazor has other two methods: ShouldRender and StateHasChanged. These methods are not part of life cycle but they are used in the life cycle process.

### ShouldRender
This method returns boolean and based on return of this method UI can be re-rendered. It will be called after the component initialization so the component is always initially rendered. 
```
protected override bool ShouldRender()
{
    Console.WriteLine("ShouldRender");
    var renderUI = true;

    return renderUI;
}
```

### StateHasChanged
This method used to notify the component that its state has changed. This method is called after any life cycle method. It can be also invoked manually to re-render the UI. This method can not be override. 
