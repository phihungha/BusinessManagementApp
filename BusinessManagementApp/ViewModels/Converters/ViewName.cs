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
            var viewName = (WorkspaceViewName)value;
            switch (viewName)
            {
                case WorkspaceViewName.Overview:
                    return "Overview";
                case WorkspaceViewName.Orders:
                    return "Orders";
                case WorkspaceViewName.EmployeeInfo:
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
            var viewName = (WorkspaceViewName)value;
            switch (viewName)
            {
                case WorkspaceViewName.Overview:
                    return App.Current.FindResource("OverviewWhiteIcon");
                case WorkspaceViewName.Orders:
                    return App.Current.FindResource("OrderWhiteIcon");
                case WorkspaceViewName.EmployeeInfo:
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
