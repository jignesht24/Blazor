### Introduction
Earlier version of the Blazor supports the limited number of events. It only supported onclick and onchange event. The current version of the Blazor provides pretty rich event handling. In current version of Blazor, you can access the most of the DOM events with the HTML element. The value of the attribute is treated as an event handler. 

Following is Razor syntax to define the event in Blazor component
```
@on{DOM EVENT}="{DELEGATE}" 
```
Blazor also supports asynchronous delegate event handler that returns the Task. The delegate event handler of the Blazor is automatically triggers a UI render event, so there is no need to manually call StateHasChanged every time on the event. The EventCallback<T> class is exposed as a parameter to the component so, it can easily notify consumers when something happened. 

##### Example
In the following example, the "IncrementCount" method is called every time when button is clicked.

```
@page "/counter"

<h1>Counter</h1>

<p>Current count: @currentCount</p>

<button class="btn btn-primary" @onclick="IncrementCount">Click me</button>
@code {
    private int currentCount = 0;
    private void IncrementCount()
    {
        currentCount++;
    }
}
```

### Event arguments
Blazor also provides the event argument if event supported an event arguments in the event method definition. For example, MouseEventArgs is provides the mouse coordinates when user move mouse pointer in the UI. 

Following table contains supported Event arguments

Event | Class | DOM event |
| ------------- | ------------- | ------------- |
Clipboard | ClipboardEventArgs | oncut, oncopy, onpaste
Drag | DragEventArgs | ondrag, ondragstart, ondragenter, ondragleave, ondragover, ondrop, ondragend
Error | ErrorEventArgs | onerror
Event | EventArgs | onactivate, onbeforeactivate, onbeforedeactivate, ondeactivate, onfullscreenchange, onfullscreenerror, oncanplay, oncanplaythrough, oncuechange, ondurationchange, onemptied, etc. 
Focus | FocusEventArgs | onfocus, onblur, onfocusin, onfocusout
Input | ChangeEventArgs | onchange, oninput
Keyboard | KeyboardEventArgs | onkeydown, onkeypress, onkeyup
Mouse | MouseEventArgs | onclick, oncontextmenu, ondblclick, onmousedown, onmouseup, onmouseover, onmousemove, onmouseout
Mouse pointer | PointerEventArgs | onpointerdown, onpointerup, onpointercancel, onpointermove, onpointerover, onpointerout, onpointerenter, onpointerleave, ongotpointercapture, onlostpointercapture
Mouse wheel | WheelEventArgs | onwheel, onmousewheel
Progress | ProgressEventArgs | onabort, onload, onloadend, onloadstart, onprogress, ontimeout
Touch | TouchEventArgs	ontouchstart, ontouchend, ontouchmove, ontouchenter, ontouchleave, ontouchcancel


##### Example
In the following example, MouseClick method used MouseEventArgs and this method show the mouse coordinates of button when it clicked.

```
@page "/mouseclick"

<h3>Mouse Coordinates</h3>
<button class="btn btn-primary" @onclick="MouseClick">Button 1</button>

<br />
<br />

<button class="btn btn-primary" @onclick="MouseClick">Button 2</button>

<p>@mouseCoordinatesString</p>

@code {
    private string mouseCoordinatesString;

    private void MouseClick(MouseEventArgs e)
    {
        mouseCoordinatesString = $"Mouse coordinates: {e.ScreenX}:{e.ScreenY}";
    }
}
```

### Define delegate using Lambda expressions
You can also define event delegate using the Lambda expressions. It is very useful to define small inline function with event handler. 

In the following Example, I have defined inline function to increment the counter. 

```
@page "/lambdaexpressions"

<h3>Lambda Expression Example</h3>

<button @onclick="@(e => currentCount++)">
    Click Me
</button>
<p> Current Count:@currentCount </p>


@code {
    private int currentCount = 0;
}
```

You can also pass additional information as a parameter of the method along with event argument. In following example, I have send button number along with MouseEventArgs argument. 

```
@page "/lambdaexpressions"

<h3>Lambda Expression Example</h3>

<button class="btn btn-primary" @onclick="@(e => MouseClick(e, 1))">Button 1</button>

<br />
<br />

<button class="btn btn-primary" @onclick="@(e => MouseClick(e, 2))">Button 2</button>
<p>@mouseCoordinatesString</p>

@code {

    private string mouseCoordinatesString;

    private void MouseClick(MouseEventArgs e, int buttonNumber)
    {
        mouseCoordinatesString = $"Mouse coordinates of button {buttonNumber}: {e.ScreenX}:{e.ScreenY}";
    }
}
```

### Event Callback
Using event callback, you can expose events across the components. The most common scenario is to execute parent component method from child component's button click. The EventCallback<T> class is exposed as a parameter to the component so, it can easily notify consumers when something happened. The public property of type EventCallback<T> is declared and decorated with the Parameter attribute. 

Following example demonstrates how a child component 's button click event handler is setup to received envent callback on parent component. Here, I am using MouseEventArgs for even callback type. When child component's button click then parent component's "EventFromChild" method called. As describe earlier, callback method does not required to call StateHasChanged as it automatically called when child component event trigger.

```
@*ChildComponent.razor*@
<h4>Child Component</h4>

<button @onclick="OnClickCallback">
    Click Me
</button>

@code {

    [Parameter]
    public EventCallback<MouseEventArgs> OnClickCallback { get; set; }
}
```

```
@*ParentComponent.razor*@
@page "/eventcallback"

<h3>Parent Component</h3>

<ChildComponent OnClickCallback="@EventFromChild" />

<p>@message</p>

@code {
    private string message;

    private void EventFromChild(MouseEventArgs e)
    {
        message = $"Mouse coordinates of child component button - ({e.ScreenX}:{e.ScreenY})";
    }
}
```

The EventCallback also allows asynchronous delegates and weakly typed (allows to pass any type of argument). It is recommended to use strongly typed. 

### Differences between Blazor's EventCallback<T> and .NET events 
There are few differences between Blazor's EventCallback<T> and .NET events 
* Blazor's EventCallback<T> is a single-cast event (it can assign to a single value or single method) handler but .NET event is multi-cast 
* Blazor's EventCallback<T> is a readonly structure so, it cannot be null but .NET Event are classes. 
* Blazor's EventCallback<T> can be asynchronous but standard .NET events are synchronous

Apart from this, Blazor's EventCallback<T> can be set using Razormarkup and provides automatic state change detection 

### Prevent default actions
Sometimes, you want to prevent an event's default action. For example, anchor's default action is to navigate to the url set in the "href" attribute. Now, you want to suppress the default behaviour but perform event. The Blazor provides  @on{DOM EVENT}:preventDefault directive attribute to prevent the default action for an event.

In the following example, the default action of anchor tag is suppressed but still execute DOM event.

```
@page "/preventDefault"

<h3>Prevent Default Example</h3>

<p>

    <a href="https://www.c-sharpcorner.com/"
       @onclick="KeyHandler"
       @onclick:preventDefault="true">
        Click Here
    </a>
</p>
<p>@count</p>

@code {
    private int count = 0;

    private void KeyHandler()
    {
        count++;
    }
}
```

If you are not specified value with @on{DOM EVENT}:preventDefault attribute, it will set to true. Also, you can provide the value of this attribute using expression.  

### Stop event propagation
It is also possible to stop event propagation with Blazor. The  @on{DOM EVENT}:stopPropagation directive attribute is used to stop event propagation. It stops event bubbling to the parent tag.

In this following example, there are two <div> element inside the parent <div> element. The first <div>'s event does not stop to propagate event to parent but second <div> event stop event propagation to parent.

```
@page "/stopeventpropagation"
<h3>Stop Event Propagation</h3>

<input @bind="stopPropagation" type="checkbox" name="stopPropagation" />
<label for="stopPropagation"> Stop Propagation</label>
<br>

<div class="m-1 p-1 border" id="primary-div" @onclick="OnClickParentDiv">
    <p>
        Parent Div
    </p>
    <div class="m-1 p-1 border" @onclick="OnClickChildDiv">
        Child div doesn't stop event propagation when clicked.
    </div>

    <div class="m-1 p-1 border" @onclick="OnClickChildDiv"
         @onclick:stopPropagation="stopPropagation">
        Child div stops event propagation when clicked.
    </div>
</div>

<p>
    @message
</p>

@code {

    private bool stopPropagation= true;

    private string message;

    private void OnClickParentDiv() =>
        message = $"The parent div was Clicked. {DateTime.Now}";

    private void OnClickChildDiv() =>
        message = $"A child div was Clicked. {DateTime.Now}";
}
```

### Summary 
The Current version of the Blazor provides rich event handling. It supports almost all DOM events. 


