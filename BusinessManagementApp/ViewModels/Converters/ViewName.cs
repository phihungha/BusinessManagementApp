using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BusinessManagementApp.ViewModels.Converters
{
    public class ViewNameToString : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var viewName = (ViewName)value;
            switch (viewName)
            {
                case ViewName.Overview:
                    return "Overview";
                case ViewName.Orders:
                    return "Orders";
                case ViewName.EmployeeInfo:
                    return "Employee info";
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ViewNameToImageSource : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var viewName = (ViewName)value;
            switch (viewName)
            {
                case ViewName.Overview:
                    return App.Current.FindResource("OverviewWhiteIcon");
                case ViewName.Orders:
                    return App.Current.FindResource("OrderWhiteIcon");
                case ViewName.EmployeeInfo:
                    return App.Current.FindResource("UserWhiteIcon");
                default:
                    return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
