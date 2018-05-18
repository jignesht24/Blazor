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
