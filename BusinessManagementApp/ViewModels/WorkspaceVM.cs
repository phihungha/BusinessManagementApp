using BusinessManagementApp.ViewModels.BusyIndicator;
using BusinessManagementApp.ViewModels.Navigation;
using BusinessManagementApp.ViewModels.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels
{
    public enum WorkspaceViewName
    {
        Bonuses,
        BonusTypes,
        BonusTypeDetails,
        CreateOrderCustomer,
        Config,
        ContractTypes,
        ContractTypeDetails,
        Customers,
        CustomerDetails,
        Departments,
        DepartmentDetails,
        EmployeeInfo,
        EmployeeInfoDetails,
        Overview,
        Overtime,
        OvertimeDetails,
        Orders,
        OrderDetails,
        Positions,
        PositionDetails,
        Products,
        ProductDetails,
        ProductCategories,
        ProductCategoryDetails,
        Providers,
        ProviderDetails,
        SalaryReport,
        SalesReport,
        SelectCustomers,
        SelectProducts,
        SelectProductOrderItems,
        SkillRating,
        SkillRatingDetails,
        SkillTypes,
        SkillTypeDetails,
        VoucherTypes,
        VoucherTypeDetails,
        Vouchers,
        VoucherDetails
    }

    public class WorkspaceVM : ObservableObject, ISupportBusyIndicator
    {
        private ViewModelBase currentViewVM
            = App.Current.ServiceProvider.GetRequiredService<OverviewVM>();

        public ViewModelBase CurrentViewVM
        {
            get => currentViewVM;
            set => SetProperty(ref currentViewVM, value);
        }

        public WorkspaceViewName[] SidebarViewNames { get; } = new[]
        {
            WorkspaceViewName.Overview,
            WorkspaceViewName.Orders,
            WorkspaceViewName.Vouchers,
            WorkspaceViewName.Customers,
            WorkspaceViewName.Products,
            WorkspaceViewName.Providers,
            WorkspaceViewName.SalesReport,

            WorkspaceViewName.EmployeeInfo,
            WorkspaceViewName.SalaryReport,
            WorkspaceViewName.Overtime,
            WorkspaceViewName.Bonuses,
            WorkspaceViewName.SkillRating,

            WorkspaceViewName.ProductCategories,
            WorkspaceViewName.VoucherTypes,
            WorkspaceViewName.BonusTypes,
            WorkspaceViewName.ContractTypes,
            WorkspaceViewName.Positions,
            WorkspaceViewName.Departments,
            WorkspaceViewName.SkillTypes,
            WorkspaceViewName.Config,
        };

        private WorkspaceViewName selectedSidebarViewName = WorkspaceViewName.Overview;

        public WorkspaceViewName SelectedSidebarViewName
        {
            get => selectedSidebarViewName;
            set
            {
                SetProperty(ref selectedSidebarViewName, value);
                CurrentViewVM = WorkspaceNavHelper.GetViewModelFromViewName(value);
            }
        }

        private bool isBusy = false;

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public ICommand Logout { get; }

        public WorkspaceVM()
        {
            var navHelper = new WorkspaceNavHelper(this);
            var busyIndicatorHelper = new BusyIndicatorHelper(this);
            Logout = new RelayCommand(() => MainWindowNavUtils.NavigateTo(MainWindowViewName.Login));
        }
    }
}