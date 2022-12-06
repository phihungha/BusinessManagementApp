using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BusinessManagementApp.ViewModels.ValidationAttributes
{
    public sealed class PhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new("Invalid phone number");
            }

            string phoneNumber = (string)value;
            if (phoneNumber.Length == 10 && phoneNumber.All(char.IsDigit))
            {
                return ValidationResult.Success;
            }
            return new("Invalid phone number");
        }
    }
}