using BlazorDemoApp.Shared;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Layouts;
using Microsoft.AspNetCore.Blazor.RenderTree;

namespace BlazorDemoApp.Pages
{
    [Route("/usingattribute")]
    [Layout(typeof(MainLayout))]
    public class LayoutdefinedUsingattribute : BlazorComponent
    {
        public string Title { get; set; } = "Layout page defined using layout Attribute";
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(1, "h2");
            builder.AddContent(2, Title);
            builder.CloseElement();
        }
    }
}
