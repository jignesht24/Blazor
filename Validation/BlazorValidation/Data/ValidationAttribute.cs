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

    public class TestClass
    {
        [AllowOneValidation]
        public int TestProperty { get; set; }
    }
}
