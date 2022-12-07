using System;
using System.Globalization;
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
                case WorkspaceViewName.Bonuses:
                    return "Bonuses";

                case WorkspaceViewName.BonusTypes:
                    return "Bonus types";

                case WorkspaceViewName.Customers:
                    return "Customers";

                case WorkspaceViewName.ContractTypes:
                    return "Contract types";

                case WorkspaceViewName.Departments:
                    return "Departments";

                case WorkspaceViewName.EmployeeInfo:
                    return "Employees";

                case WorkspaceViewName.Orders:
                    return "Orders";

                case WorkspaceViewName.OvertimeRecords:
                    return "Overtime records";

                case WorkspaceViewName.Overview:
                    return "Overview";

                case WorkspaceViewName.Positions:
                    return "Positions";

                case WorkspaceViewName.Products:
                    return "Products";

                case WorkspaceViewName.Providers:
                    return "Providers";

                case WorkspaceViewName.SalaryReport:
                    return "Salary report";

                case WorkspaceViewName.SalesReport:
                    return "Sales report";

                case WorkspaceViewName.SkillRating:
                    return "Skill rating";

                case WorkspaceViewName.SkillTypes:
                    return "Skill types";

                case WorkspaceViewName.Vouchers:
                    return "Vouchers";

                case WorkspaceViewName.VoucherTypes:
                    return "Voucher types";

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