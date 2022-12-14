using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Markup;

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

            decimal baseSalary = 0;
            decimal.TryParse((string)value, out baseSalary); 
            if (baseSalary > 0)
            {
                return ValidationResult.Success;
            }
            return new("Invalid base salary");
        }
    }
}