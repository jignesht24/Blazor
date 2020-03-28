namespace BlazorValidation.Data
{
    using System.ComponentModel.DataAnnotations;
    public class ComplexModel
    {
        [ValidateComplexType]
        public EmployeeModel Employee { get; set; }
    }
}
