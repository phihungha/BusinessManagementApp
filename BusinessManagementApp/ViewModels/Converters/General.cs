using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace BusinessManagementApp.ViewModels.Converters
{
    [ValueConversion(typeof(bool), typeof(Style))]
    public class BoolToCurrentRecordItemStyle : MarkupExtension, IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? App.Current.FindResource("CurrentRecordItem") : null;
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