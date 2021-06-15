## CSS isolation in Blazor

### Introduction
CSS (Cascading Style Sheets) is powerful standard for user (developer) to define the look of the application. Many clients side frame work such as Angular supports component wise CSS i.e. you can attach CSS to directly component. Its scope isolate from the rest of the application. Now, Blazor is also supports CSS isolation that helps to avoid styling conflicts among the component.

The CSS isolation in Blazor component is by default enable and to use it create ".razor.css" file matching with component name. This is also referring as a scoped CSS file. For example, if you want to create scoped CSS for "ComponentWiseCss.razor" component then create "ComponentWiseCss.razor.css" file and define component  related CSS.

![alt text](img/structureCssFile.png "")

The style defined in ComponentWiseCss.razor.css is only applied to the rendering of ComponentWiseCss.razor page. It is not applied to the other razor pages.

Example 
```
@*ComponentWiseCss.razor*@
@page "/cssexample"

<h1>CSS EXample</h1>

<div class="col-12 style1">
    <p>Applying Style 1</p>
</div>
<div class="col-12 style2">
    <p>Applying Style 2</p>
</div>
<div class="col-12 style3">
    <p>Applying Style 3</p>
</div>

@code {

}
```
ComponentWiseCss.razor.css

```
body {
}
.style1 {
    background-color: lightgray;
    font-weight:bold;
}
.style2 {
    background-color: lightgreen;
    font-weight: bold;
}
.style3 {
    background-color: black;
    color: yellow;
    font-weight: bold;
}
```

Output
![alt text](img/example1.png "")

### CSS isolation bundling
The CSS isolation happen at build time. The Blazor engine rewrites the CSS and HTML to match markup to component. The rewritten CSS style are bundled into one and saved as static resource. The stylesheet references inside the <head> tag of wwwroot/index.html (Blazor WebAssembly) or Pages/_Host.cshtml (Blazor Server). The name of the stylesheet is in format "{ASSEMBLY NAME}.styles.css" here, {ASSEMBLY NAME} is project name. 

When the Blazor engine bundled the CSS file, it associates unique scope identifiers with each component. The scope identifier is in format "b-<10 character string>". In the following Example, three differnt styles are created for the <div> element. When you look at the page source, you notice that Blazor engine creates unique scope identifiers and associates it with both HTML and CSS. 

```
body[b-qvelpzvlyq] {
}
.style1[b-qvelpzvlyq] {
    background-color: lightgray;
    font-weight:bold;
}
.style2[b-qvelpzvlyq] {
    background-color: lightgreen;
    font-weight: bold;
}
.style3[b-qvelpzvlyq] {
    background-color: black;
    color: yellow;
    font-weight: bold;
}
```
![alt text](img/example2.png "")

### Cascading CSS style to Child Component
The CSS isolation is only applied to the component level by default but you can extend it to the child component using "::deep" attribute in CSS file. This attribute is Blazor attribute so, it only understands and parse by Blazor engine. When "::deep" attribute used in CSS file, Blazor engine also applied scope identifier to all descendants of component. 

In the following example, the "::deep" attribute is assigned to <div> element style but not to paragraph (<p>) element. You can see here, <div> element style are applied to both parent and child component but paragraph (<p>) element style is only applied to parent component. 

ChildComponent.razor
```
<hr />
<p>
    paragraph:: This is child component content
</p>

<div>
    div:: This is child component content
</div>

<hr />

@code {

}
```
ParentComponent.razor
```
@page "/parentcomponent"
<h3>Parent Component</h3>

<div>
    <p>paragraph:: This is Parent Component content</p>
    <ChildComponent></ChildComponent>
    <div>
        div:: This is parent component content
    </div>
</div>

@code {

}
```
ParentComponent.razor.css
```
p {
    color: red
}

::deep div {
    color:orange;
    font-weight:bold;
}
```
Output
![alt text](img/Childcomponentsupport.png "")

The Sass and Less are well known CSS preprocessor. They are useful for improving CSS development. They provide many features such as variables, nesting, inheritance, modules and mixins. The CSS isolation does not natively support the CSS preprocessor but you can integrate CSS preprocessor to Blazor project by compile preprocessor before Blazor engine rewrites the CSS selectors during the build process. For example, you can perform preprocessor compilation  on Before build task in Visual Studio.

### Summary
Blazor provide very beautiful feature called CSS isolation that enable you to create CSS file per component. 
