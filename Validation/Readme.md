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

| Attribute  | Description |
| ------------- | ------------- |
| CreditCard  | Validate property as per the credit card format  |
| EmailAddress  | Validate property as per the email format  |
| Compare  | Compare two model properties  |
| Phone  | Validate property as per the phone number format  |
| Range  | Validate the property value falls in specified range  |
| RegularExpression  | Validate the property value match specified regular expression  |
| Required  | Validate the property value is not null  |
| StringLength  | Validate the property value must with in specified length limit  |
| Uri  | Validate property as per the url format  |
| Remote  | Validate the property value by calling a specified server method  |

You can define the form in a Blazor app using "EditForm" component. The Blazor engine only validates the input model's property value that is defined in "EditForm" component. The Blazor provides a DataAnnotationsValidator compoment that tells the Blazor engine to validate a model using data annotation. Blazor provides two components: ValidationSummary and ValidationMessage to display a model validation error on screen.
 
The Blazor EditForm component provides OnValidSubmit event callback that is triggered when a form is successfully submitted without any validation error. OnInvalidSubmit event callback is triggered when a form has been submitted with a validation error.
 
Blazor also provides a set of built-in input components that receive and validate the user input. These components validate the content when they are changed or a form is submitted.
 
Follow the built-in component provided by Blazor.

| Component  | Render as |
| ------------- | ------------- |
| InputText  |  \<input>  |
| InputCheckbox  |  <input type=\"checkbox\">  |
| InputNumber  |  <input type=\"number\">  |
| InputTextArea  |  <textarea>  |
| InputSelect  |   \<select>  |
| InputDate  |  <input type=\"date\">  | 

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

Using ValidationMessage component, you can show a validation message for individual components. This component has a "for" attribute that is used to specify the field for the validation using lambda expression.

```
@page "/editemployeeindividual"  
@using BlazorValidation.Data;  
  
<h1>Add/Edit Employee</h1>  
  
<EditForm Model="@employee" OnValidSubmit="HandleValidSubmit">  
    <DataAnnotationsValidator />  
    <div class="row content">  
        <div class="col-md-2"><label for="Name">Name</label></div>  
        <div class="col-md-3"><InputText id="name" @bind-Value="employee.Name" /></div>  
        <div class="col-md-5"><ValidationMessage For="@(() => employee.Name)" /> </div>  
        </div>  
    <div class="row content">  
        <div class="col-md-2"><label for="EmailAddress">Email</label></div>  
        <div class="col-md-3"><InputText id="EmailAddress" @bind-Value="employee.EmailAddress" /></div>  
        <div class="col-md-5"><ValidationMessage For="@(() => employee.EmailAddress)" /> </div>  
    </div>  
    <div class="row content">  
        <div class="col-md-2"><label for="PhoneNumer">Phone</label></div>  
        <div class="col-md-3"><InputText id="PhoneNumer" @bind-Value="employee.PhoneNumer" /></div>  
        <div class="col-md-5"><ValidationMessage For="@(() => employee.PhoneNumer)" /> </div>  
    </div>  
    <div class="row content">  
        <div class="col-md-2"><label for="CreditCardNumer">Credit Card Numer</label></div>  
        <div class="col-md-3"><InputText id="CreditCardNumer" @bind-Value="employee.CreditCardNumer" /></div>  
        <div class="col-md-5"><ValidationMessage For="@(() => employee.CreditCardNumer)" /> </div>  
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

## Does Blazor support custom validation attribute?

Yes, Blazor also supports the custom validation attribute. The custom validation attribute must inherit from ValidationAttribute and needs to implement the overridable method "IsValid". This method returns ValidationResult and based on this the Blazor engine decides whether input value is valid or not.

#### Example
 
In the following example, custom validation attribute is created that only allows one(1). The "IsValid" method returns ValidationResult.Success if value is one; else it returns an error message.

```
namespace BlazorValidation.Data  
{  
    using System;  
    using System.ComponentModel.DataAnnotations;  
    public class AllowOneValidationAttribute : ValidationAttribute  
    {  
        protected override ValidationResult IsValid(object value,  
        ValidationContext validationContext)  
        {  
  
            int valueInt = Convert.ToInt32(value);  
            if (valueInt == 1)  
            {  
                return ValidationResult.Success;  
            }  
  
            return new ValidationResult("Error: only one is allowed in this property.",  
                new[] { validationContext.MemberName });  
        }  
    }  
}
```

## Validate the complex property

The DataAnnotationsValidator component only validates top-level properties of the model. It does not validate collection or complex-type properties of model class. The Blazor provides ObjectGraphDataAnnotationsValidator to validate the entire model object including collection and complex-type properties.
 
This component is defined in Microsoft.AspNetCore.Blazor.DataAnnotations.Validation package and currently it is in an experimental stage. To use ObjectGraphDataAnnotationsValidator component, you need to install said package from NuGet.

```
<EditForm Model="@model" OnValidSubmit="HandleValidSubmit">  
    <ObjectGraphDataAnnotationsValidator />  
    ...  
</EditForm>
```

All complex and collection type model properties must be decorated with ValidateComplexType attribute.

```
namespace BlazorValidation.Data  
{  
    using System.ComponentModel.DataAnnotations;  
    public class ComplexModel  
    {  
        [ValidateComplexType]  
        public EmployeeModel Employee { get; set; }  
    }  
}
```

### Summary

Blazor supports data annotation validation. The data annotation validation attributes are easy to use and help us to reduce code size as well as re-usability. Using ObjectGraphDataAnnotationsValidator component, you can also validate complex type or collection type model, however it is in the experimental stage. 
