using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BusinessManagementApp.ViewModels.ValidationAttributes
{
    public sealed class CitizenIdAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new("Invalid citizen ID");
            }

            string citizenId = (string)value;
            if (citizenId.Length == 12 && citizenId.All(char.IsDigit))
            {
                return ValidationResult.Success;
            }
            return new("Invalid citizen ID");
        }
    }
}