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

                case WorkspaceViewName.Config:
                    return "Configuration";

                case WorkspaceViewName.Departments:
                    return "Departments";

                case WorkspaceViewName.EmployeeInfo:
                    return "Employees";

                case WorkspaceViewName.Orders:
                    return "Orders";

                case WorkspaceViewName.Overtime:
                    return "Overtime records";

                case WorkspaceViewName.Overview:
                    return "Overview";

                case WorkspaceViewName.Positions:
                    return "Positions";

                case WorkspaceViewName.Products:
                    return "Products";

                case WorkspaceViewName.ProductCategories:
                    return "Product categories";

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

                case WorkspaceViewName.Vouchers:
                    return App.Current.FindResource("VouchersWhiteIcon");

                case WorkspaceViewName.SalesReport:
                    return App.Current.FindResource("SalesReportWhiteIcon");

                case WorkspaceViewName.Overtime:
                    return App.Current.FindResource("OvertimeRecordsWhiteIcon");

                case WorkspaceViewName.SkillTypes:
                    return App.Current.FindResource("SkillTypesWhiteIcon");

                case WorkspaceViewName.SkillRating:
                    return App.Current.FindResource("SkillRatingWhiteIcon");

                case WorkspaceViewName.SalaryReport:
                    return App.Current.FindResource("SalaryReportWhiteIcon");

                case WorkspaceViewName.VoucherTypes:
                    return App.Current.FindResource("VoucherTypesWhiteIcon");

                case WorkspaceViewName.Products:
                    return App.Current.FindResource("ProductsWhiteIcon");

                case WorkspaceViewName.ProductCategories:
                    return App.Current.FindResource("ProductsWhiteIcon");

                case WorkspaceViewName.Bonuses:
                    return App.Current.FindResource("BonusesWhiteIcon");

                case WorkspaceViewName.BonusTypes:
                    return App.Current.FindResource("BonusTypesWhiteIcon");

                case WorkspaceViewName.Providers:
                    return App.Current.FindResource("ProvidersWhiteIcon");

                case WorkspaceViewName.Positions:
                    return App.Current.FindResource("PositionsWhiteIcon");

                case WorkspaceViewName.Customers:
                    return App.Current.FindResource("CustomersWhiteIcon");

                case WorkspaceViewName.ContractTypes:
                    return App.Current.FindResource("ContractTypesWhiteIcon");

                case WorkspaceViewName.Departments:
                    return App.Current.FindResource("DepartmentsWhiteIcon");

                case WorkspaceViewName.Config:
                    return App.Current.FindResource("SettingsWhiteIcon");

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