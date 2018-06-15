using BlazorDemoApp.Shared;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Layouts;
using Microsoft.AspNetCore.Blazor.RenderTree;
using Microsoft.Extensions.Logging;

namespace BlazorDemoApp.Pages
{
    [Route("/code")]
    [Layout(typeof(MainLayout))]
    public class MyTestClass : BlazorComponent
    {
        [Inject]
        protected ILogger<MyTestClass> logger { get; set; }
        public string Title { get; set; } = "Component created";
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(1, "h2");
            builder.AddContent(2, Title);
            builder.CloseElement();
        }

        protected override void OnInit()
        {
            logger.LogInformation("OnInit");
        }

        public void LogInfo(string message)
        {
            logger.LogDebug(message);
        }
    }
}
