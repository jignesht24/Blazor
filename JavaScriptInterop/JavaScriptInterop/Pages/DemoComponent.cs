using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace JavaScriptInterop.Pages
{
    public class DemoComponent : BlazorComponent
    {
        public string message = "";

        [JSInvokable("CalledByComponentScript")]
        public static string CalledByComponentScript()
        {
            return "Hello from C#";
        }
        [JSInvokable("CalledByComponentScriptWithParameter")]
        public static string CalledByComponentScript(string name)
        {
            return "Hello, " + name + "!";
        }

        protected void CallJSFunction()
        {
            JSRuntime.Current.InvokeAsync<object>("updateUIExample");
        }

        protected override async Task OnInitAsync()
        {
            //Without parameter 
            await JSRuntime.Current.InvokeAsync<object>("CalledJSFunction");

            //with Parameter
            await JSRuntime.Current.InvokeAsync<object>("CalledJSFunctionWithParameter", "Jignesh Trivedi");

            //Instance method
            await JSRuntime.Current.InvokeAsync<object>("passInstanceOfHelper", new DotNetObjectRef(new MyHelper()));

            //Instance method
            await JSRuntime.Current.InvokeAsync<object>("passInstanceOfHelperUIUpdate", new DotNetObjectRef(new MyHelper(this)));

        }
        public void UpdateUI()
        {
            this.StateHasChanged();
        }
    }

    public class MyHelper
    {

        public MyHelper()
        {

        }
        DemoComponent _component;
        public MyHelper(DemoComponent component)
        {
            _component = component;
        }

        [JSInvokable("InstanceMethod")]
        public string InstanceMethod(string myMessage)
        {
            return myMessage;
        }

        [JSInvokable("UIUpdateExample")]
        public void UIUpdateExample()
        {
            _component.message = "Hello, Reader!";
            _component.UpdateUI();
        }
    }
}
