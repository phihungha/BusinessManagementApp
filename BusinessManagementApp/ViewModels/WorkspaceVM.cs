using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.BusyIndicator;
using BusinessManagementApp.ViewModels.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
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
        private readonly WorkspaceViewName[] orderViewNames = new[]
        {
            WorkspaceViewName.Overview,
            WorkspaceViewName.Orders
        };

        private readonly WorkspaceViewName[] salesViewNames = new[]
        {
            WorkspaceViewName.Products,
            WorkspaceViewName.Providers,
            WorkspaceViewName.Vouchers,
            WorkspaceViewName.SalesReport
        };

        private readonly WorkspaceViewName[] hrViewNames = new[]
        {
            WorkspaceViewName.EmployeeInfo,
            WorkspaceViewName.Overtime,
            WorkspaceViewName.Bonuses,
            WorkspaceViewName.SalaryReport,
            WorkspaceViewName.SkillRating,
        };

        private readonly WorkspaceViewName[] configViewNames = new[]
        {
            WorkspaceViewName.ProductCategories,
            WorkspaceViewName.VoucherTypes,
            WorkspaceViewName.BonusTypes,
            WorkspaceViewName.ContractTypes,
            WorkspaceViewName.Positions,
            WorkspaceViewName.Departments,
            WorkspaceViewName.SkillTypes,
            WorkspaceViewName.Config,
        };

        private WorkspaceNavHelper navHelper;
        private BusyIndicatorHelper busyIndicatorHelper;

        private ViewModelBase currentViewVM;

        public ViewModelBase CurrentViewVM
        {
            get => currentViewVM;
            set => SetProperty(ref currentViewVM, value);
        }

        public ObservableCollection<WorkspaceViewName> SidebarViewNames { get; } = new();

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

        public Employee CurrentUser { get; }

        public Position CurrentPosition { get; }

        private bool isBusy = false;

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public ICommand Logout { get; }

        public WorkspaceVM(SessionsRepo sessionsRepo)
        {
            navHelper = new WorkspaceNavHelper(this);
            busyIndicatorHelper = new BusyIndicatorHelper(this);
            Logout = new RelayCommand(() => MainWindowNavUtils.NavigateTo(MainWindowViewName.Login));

            CurrentUser = sessionsRepo.CurrentUser;
            CurrentPosition = sessionsRepo.CurrentPosition;

            SetSidebarViewNames();

            // This needed to be done after initiating the helpers so
            // messenger-dependent features such as navigation and busy indicator can work.
            currentViewVM = App.Current.ServiceProvider.GetRequiredService<OverviewVM>();
        }

        private void SetSidebarViewNames()
        {
            if (CurrentPosition.CanViewOrders)
            {
                SidebarViewNames.AddRange(orderViewNames);
            }

            if (CurrentPosition.CanViewCustomers)
            {
                SidebarViewNames.Add(WorkspaceViewName.Customers);
            }

            if (CurrentPosition.CanViewSales)
            {
                SidebarViewNames.AddRange(salesViewNames);
            }

            if (CurrentPosition.CanViewHr)
            {
                SidebarViewNames.AddRange(hrViewNames);
            }

            if (CurrentPosition.CanViewConfig)
            {
                SidebarViewNames.AddRange(configViewNames);
            }
        }
    }
}