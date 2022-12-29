using System.ComponentModel.DataAnnotations;

namespace BusinessManagementApp.ViewModels.ValidationAttributes
{
    public sealed class BaseSalaryAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new("Invalid base salary");
            }

            decimal baseSalary = (decimal)value;
            if (baseSalary > 0)
            {
                return ValidationResult.Success;
            }
            return new("Invalid base salary");
        }
    }
}