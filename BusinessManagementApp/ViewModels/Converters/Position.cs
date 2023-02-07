using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace BusinessManagementApp.ViewModels.Converters
{
    [ValueConversion(typeof(DateTime?), typeof(string))]
    public class PositionEndDateToString : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Band-aid fix: Backend returns incorrect date for current position
            return (value == null || (DateTime)value == new DateTime(3000, 2, 1)) 
                    ? "Now" : ((DateTime)value).ToString("d");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}