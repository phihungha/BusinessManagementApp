using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace BusinessManagementApp.Utils
{
    /// <summary>
    /// Help bind ComboBox into enum values
    /// Source: https://stackoverflow.com/a/12430331/15872481
    /// </summary>
    public static class EnumComboBoxHelper
    {
        public static string Description(this Enum value)
        {
            var attributes = value.GetType()
                ?.GetField(value.ToString())
                ?.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Any())
            {
                var attribute = attributes.First() as DescriptionAttribute;
                if (attribute != null)
                    return attribute.Description;
            }

            // If no description is found, the least we can do is replace underscores with spaces
            // You can add your own custom default formatting logic here
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            return ti.ToTitleCase(ti.ToLower(value.ToString().Replace("_", " ")));
        }

        public static IEnumerable<ValueDescription> GetAllValuesAndDescriptions(Type t)
        {
            if (!t.IsEnum)
                throw new ArgumentException($"{nameof(t)} must be an enum type");

            return Enum.GetValues(t)
                .Cast<Enum>()
                .Select((e) => new ValueDescription() { Value = e, Description = e.Description() })
                .ToList();
        }
    }

    public class ValueDescription
    {
        public object Value { get; set; } = new();
        public object Description { get; set; } = new();
    }
}
