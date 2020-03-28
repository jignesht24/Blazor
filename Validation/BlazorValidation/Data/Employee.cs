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
