### Introduction

Just like mordern client framework (such as Angular), Blazor has components at the core part. It uses the combination of Razor, HTML and C# code as a component. Component is base element of the Blazoe application i.e. every page are considered as component in Blazor. There are multiple ways to create components in Blazor. 

#### Create Blazor component Inline method
This is very simple and most common method to create components in blazor. In this method, HTML and code behind are in same page. Most of the demo projects are use this method to demostrate the concepts. The HTML part of the component contains Razor syntax as well as HTML tags and code behind section contains the actual logic. In short, in this method, we can add view markup and logic in same page. The logic is separated by using function block.

Example
```
<h2>@Title</h2>

@functions {
    const string Title = "Component created by using inline method";
} 
```

In function block, we can define all the properties that are used in view markup and the method are bind with control as event. 

#### Create Blazor component with Code behind
In this method of creating component, view markup and C# code logic are in separate files. In the current version of Blazor (0.2.0), this is achieved by using @inherits directive. This directive tell Blazor compiler to derive the class generated from the razor view from class specified with this directive. The class specified with @inherits directive must be inherit from BlazorComponent class and it provides all base functionality for the component. 

Code behind class
```
using Microsoft.AspNetCore.Blazor.Components;

namespace BlazorDemoApp
{
    public class CodebehindClass : BlazorComponent
    {
        public string Title { get; set; } = "Component created by using Code behind method";
    }
}
```
View File
```
@page "/codebehind"

@inherits CodebehindClass

<h2>@Title</h2>
```

Please note that Blazor compiler is generate class for all the view pages with class name is same as page name, so here specified base class cannot have same name as razor view otherwise it would cause a compile time error. 

#### Class Only Component
As we know, all the component in Blazor ends up as class. Event Blazor compiler generates the class for Rezor view. So we can also create the component using C# code. Here class must be inherit from BlazorComponent class. 

In BuildRenderTree method, we can generate page layout using C# code. We can do every thing with page layout that we did in Razor view. 

Example
```
using BlazorDemoApp.Shared;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Layouts;
using Microsoft.AspNetCore.Blazor.RenderTree;

namespace BlazorDemoApp.Pages
{
    [Route("/classonly")]
    [Layout(typeof(MainLayout))]
    public class ClassOnlyComponent : BlazorComponent
    {
        public string Title { get; set; } = "Component created by using Class only method";
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(1, "h2");
            builder.AddContent(2, Title);
            builder.CloseElement();
        }
    }
}
```

#### Summary
Component is core item in Blazor just like mordern client framework (such as Angular). Here I have describe, three methods for creating component in Blazor. Many more methods may add in upcoming release. Currently Blazor is an experimental project and it is not in Production yet.
 
