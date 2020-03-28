## Validation In Blazor Apps

### Introduction 
Input validation is very important for any application, as it prevents users from posting unwanted or erroneous data to the server. Blazor supports form and validation using data annotation. One of the key advantages of using data annotation validators is that they enable us to perform validation simply by adding one or more attributes on a model's property.
 
Blazor uses ASP.net Core validation attributes that are defined under System.ComponentModel.DataAnnotations namespace. There are many built-in data annotation attributes provided by .net core framework. You can also build your own data validation attribute (custom validation attribute).

#### Example

```
 namespace BlazorValidation.Data  
{  
    using System.ComponentModel.DataAnnotations;  
    public class Employee  
    {  
        ...  
        [Required]  
        [StringLength(50)]  
        public string Name { get; set; }  
        ...  
        ...  
    }  
}
```
Blazor app supports the following built-in validation attributes.

You can define the form in a Blazor app using "EditForm" component. The Blazor engine only validates the input model's property value that is defined in "EditForm" component. The Blazor provides a DataAnnotationsValidator compoment that tells the Blazor engine to validate a model using data annotation. Blazor provides two components: ValidationSummary and ValidationMessage to display a model validation error on screen.
 
The Blazor EditForm component provides OnValidSubmit event callback that is triggered when a form is successfully submitted without any validation error. OnInvalidSubmit event callback is triggered when a form has been submitted with a validation error.
 
Blazor also provides a set of built-in input components that receive and validate the user input. These components validate the content when they are changed or a form is submitted.
 
Follow the built-in component provided by Blazor.

#### Example

In the following example, Employee model has various properties and is decorated with a built-in validation data annotation attribute. 

```
namespace BlazorValidation.Data  
{  
    using System.ComponentModel.DataAnnotations;  
    public class EmployeeModel  
    {  
        public int Id { get; set; }  
        [Required]  
        [StringLength(50)]  
        public string Name { get; set; }  
        [Required]  
        [EmailAddress]  
        public string EmailAddress { get; set; }  
        [Required]  
        [Phone]  
        public string PhoneNumer { get; set; }  
        [Required]  
        [CreditCard]  
        public string CreditCardNumer { get; set; }  
  
    }  
}
```

Employee Razor page contains the EditForm component. Under the EditForm component, DataAnnotationsValidator and ValidationSummary component are defined. So, the Blazor engine will validate the inputs using data annotation and list down all form validation as a summary on the submit button click.

```
@page "/editemployee"  
@using BlazorValidation.Data;  
  
<h1>Add/Edit Employee</h1>  
  
<EditForm Model="@employee" OnValidSubmit="HandleValidSubmit">  
    <DataAnnotationsValidator />  
    <ValidationSummary />  
    <div class="row content">  
        <div class="col-md-2"><label for="Name">Name</label></div>  
        <div class="col-md-3"><InputText id="name" @bind-Value="employee.Name" /></div>  
    </div>  
    <div class="row content">  
        <div class="col-md-2"><label for="EmailAddress">Email</label></div>  
        <div class="col-md-3"><InputText id="EmailAddress" @bind-Value="employee.EmailAddress" /></div>  
    </div>  
    <div class="row content">  
        <div class="col-md-2"><label for="PhoneNumer">Phone</label></div>  
        <div class="col-md-3"><InputText id="PhoneNumer" @bind-Value="employee.PhoneNumer" /></div>  
    </div>  
    <div class="row content">  
        <div class="col-md-2"><label for="CreditCardNumer">Credit Card Numer</label></div>  
        <div class="col-md-3"><InputText id="CreditCardNumer" @bind-Value="employee.CreditCardNumer" /></div>  
    </div>  
    <div class="row content">  
        <button type="submit">Submit</button>  
    </div>  
  
</EditForm>  
  
@code {  
    EmployeeModel employee = new EmployeeModel();     
    private void HandleValidSubmit()  
    {  
        Console.WriteLine("OnValidSubmit");  
    }  
} 
```
