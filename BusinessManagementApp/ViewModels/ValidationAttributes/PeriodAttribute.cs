﻿using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Markup;

namespace BusinessManagementApp.ViewModels.ValidationAttributes
{
    public sealed class PeriodAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new("Invalid period");
            }

            int period = 0;
            int.TryParse((string)value, out period);
            if (period > 0)
            {
                return ValidationResult.Success;
            }
            return new("Invalid period");
        }
    }
}