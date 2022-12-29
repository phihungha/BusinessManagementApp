using BusinessManagementApp.ViewModels.DetailsVMs;
using BusinessManagementApp.ViewModels.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels
{
    public enum WorkspaceViewName
    {
        Bonuses,
        BonusTypes,
        BonusTypeDetails,
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
        Providers,
        ProviderDetails,
        SalaryReport,
        SalesReport,
        SelectCustomers,
        SelectProducts,
        SkillRating,
        SkillRatingDetails,
        SkillTypes,
        SkillTypeDetails,
        VoucherTypes,
        VoucherTypeDetails,
        Vouchers,
        VoucherDetails
    }

    /// <summary>
    /// Contains the content of a navigation message
    /// including name of view to go to and extra message
    /// </summary>
    public struct NavigationMessageContent
    {
        public WorkspaceViewName? TargetViewName { get; set; }
        public object? Extra { get; set; }
        public bool SaveOnBackstack { get; set; }

        /// <summary>
        /// Contains the content of a navigation message
        /// including name of view to go to and extra message
        /// </summary>
        /// <param name="targetViewName">Name of view to go to</param>
        /// <param name="extra">An object containing extra message</param>
        public NavigationMessageContent(WorkspaceViewName? targetViewName, bool saveOnBackstack, object? extra)
        {
            TargetViewName = targetViewName;
            Extra = extra;
            SaveOnBackstack = saveOnBackstack;
        }
    }

    /// <summary>
    /// Message to indicate a navigation request to another view on the workspace view.
    /// </summary>
    public class WorkspaceNavigationMessage : ValueChangedMessage<NavigationMessageContent>
    {
        /// <summary>
        /// Message to indicate a navigation request to another view on the workspace view.
        /// </summary>
        /// <param name="targetViewName">Name of view to go to</param>
        /// <param name="extra">An object containing extra message</param>
        public WorkspaceNavigationMessage(WorkspaceViewName targetViewName, bool saveOnBackstack, object? extra = null)
            : base(new NavigationMessageContent(targetViewName, saveOnBackstack, extra))
        {
        }
    }

    /// <summary>
    /// Message to indicate a navigation request to return to the previous view.
    /// </summary>
    public class WorkspaceBackNavigationMessage : ValueChangedMessage<NavigationMessageContent>
    {
        public WorkspaceBackNavigationMessage(object? extra = null)
            : base(new NavigationMessageContent(null, false, extra))
        {
        }
    }

    public class WorkspaceVM : ObservableObject
    {
        private Stack<ViewModelBase> navigationBackstack = new();

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

            WorkspaceViewName.VoucherTypes,
            WorkspaceViewName.BonusTypes,
            WorkspaceViewName.ContractTypes,
            WorkspaceViewName.Positions,
            WorkspaceViewName.Departments,
            WorkspaceViewName.SkillTypes,
        };

        private WorkspaceViewName selectedSidebarViewName = WorkspaceViewName.Overview;

        public WorkspaceViewName SelectedSidebarViewName
        {
            get => selectedSidebarViewName;
            set
            {
                SetProperty(ref selectedSidebarViewName, value);
                CurrentViewVM = GetViewModelFromViewName(value);
            }
        }

        public ICommand Logout { get; }

        public WorkspaceVM()
        {
            WeakReferenceMessenger
                .Default
                .Register<WorkspaceNavigationMessage>(
                    this, (r, m) => HandleNavigationMessage(m));

            WeakReferenceMessenger
                .Default
                .Register<WorkspaceBackNavigationMessage>(
                    this, (r, m) => HandleBackNavigationMessage(m));

            Logout = new RelayCommand(() => MainWindowNavUtils.NavigateTo(MainWindowViewName.Login));
        }

        private void HandleNavigationMessage(WorkspaceNavigationMessage message)
        {
            NavigationMessageContent content = message.Value;

            if (content.TargetViewName == null)
            {
                throw new ArgumentNullException(nameof(content.TargetViewName));
            }
            var viewName = (WorkspaceViewName)content.TargetViewName;

            if (content.SaveOnBackstack)
            {
                navigationBackstack.Push(CurrentViewVM);
            }

            ViewModelBase viewModel = GetViewModelFromViewName(viewName);
            viewModel.LoadData(content.Extra);
            CurrentViewVM = viewModel;

        }

        private void HandleBackNavigationMessage(WorkspaceBackNavigationMessage message)
        {
            ViewModelBase viewModel = navigationBackstack.Pop();
            NavigationMessageContent content = message.Value;
            viewModel.OnBack(content.Extra);
            CurrentViewVM = viewModel;
        }

        private ViewModelBase GetViewModelFromViewName(WorkspaceViewName targetViewName)
        {
            var serviceProvider = App.Current.ServiceProvider;
            switch (targetViewName)
            {
                case WorkspaceViewName.Bonuses:
                    return serviceProvider.GetRequiredService<BonusesVM>();

                case WorkspaceViewName.BonusTypes:
                    return serviceProvider.GetRequiredService<BonusTypesVM>();

                case WorkspaceViewName.BonusTypeDetails:
                    return serviceProvider.GetRequiredService<BonusTypeDetailsVM>();

                case WorkspaceViewName.ContractTypes:
                    return serviceProvider.GetRequiredService<ContractTypesVM>();

                case WorkspaceViewName.ContractTypeDetails:
                    return serviceProvider.GetRequiredService<ContractTypeDetailsVM>();

                case WorkspaceViewName.Customers:
                    return serviceProvider.GetRequiredService<CustomersVM>();

                case WorkspaceViewName.CustomerDetails:
                    return serviceProvider.GetRequiredService<CustomerDetailsVM>();

                case WorkspaceViewName.Departments:
                    return serviceProvider.GetRequiredService<DepartmentsVM>();

                case WorkspaceViewName.DepartmentDetails:
                    return serviceProvider.GetRequiredService<DepartmentDetailsVM>();

                case WorkspaceViewName.EmployeeInfo:
                    return serviceProvider.GetRequiredService<EmployeesVM>();

                case WorkspaceViewName.EmployeeInfoDetails:
                    return serviceProvider.GetRequiredService<EmployeeDetailsVM>();

                case WorkspaceViewName.Orders:
                    return serviceProvider.GetRequiredService<OrdersVM>();

                case WorkspaceViewName.OrderDetails:
                    return serviceProvider.GetRequiredService<OrderDetailsVM>();

                case WorkspaceViewName.Overtime:
                    return serviceProvider.GetRequiredService<OvertimeVM>();

                case WorkspaceViewName.OvertimeDetails:
                    return serviceProvider.GetRequiredService<OvertimeDetailsVM>();

                case WorkspaceViewName.Overview:
                    return serviceProvider.GetRequiredService<OverviewVM>();

                case WorkspaceViewName.Positions:
                    return serviceProvider.GetRequiredService<PositionsVM>();

                case WorkspaceViewName.PositionDetails:
                    return serviceProvider.GetRequiredService<PositionDetailsVM>();

                case WorkspaceViewName.Products:
                    return serviceProvider.GetRequiredService<ProductsVM>();

                case WorkspaceViewName.ProductDetails:
                    return serviceProvider.GetRequiredService<ProductDetailsVM>();

                case WorkspaceViewName.Providers:
                    return serviceProvider.GetRequiredService<ProvidersVM>();

                case WorkspaceViewName.ProviderDetails:
                    return serviceProvider.GetRequiredService<ProviderDetailsVM>();

                case WorkspaceViewName.SalaryReport:
                    return serviceProvider.GetRequiredService<SalaryReportVM>();

                case WorkspaceViewName.SalesReport:
                    return serviceProvider.GetRequiredService<SalesReportVM>();

                case WorkspaceViewName.SelectCustomers:
                    return serviceProvider.GetRequiredService<SelectCustomersVM>();

                case WorkspaceViewName.SelectProducts:
                    return serviceProvider.GetRequiredService<SelectProductsVM>();

                case WorkspaceViewName.SkillRating:
                    return serviceProvider.GetRequiredService<SkillRatingVM>();

                case WorkspaceViewName.SkillRatingDetails:
                    return serviceProvider.GetRequiredService<SkillRatingDetailsVM>();

                case WorkspaceViewName.SkillTypes:
                    return serviceProvider.GetRequiredService<SkillTypesVM>();

                case WorkspaceViewName.SkillTypeDetails:
                    return serviceProvider.GetRequiredService<SkillTypeDetailsVM>();

                case WorkspaceViewName.Vouchers:
                    return serviceProvider.GetRequiredService<VouchersVM>();

                case WorkspaceViewName.VoucherDetails:
                    return serviceProvider.GetRequiredService<VoucherDetailsVM>();

                case WorkspaceViewName.VoucherTypes:
                    return serviceProvider.GetRequiredService<VoucherTypesVM>();

                case WorkspaceViewName.VoucherTypeDetails:
                    return serviceProvider.GetRequiredService<VoucherTypeDetailsVM>();

                default:
                    throw new ArgumentException("Invalid view model name");
            }
        }
    }
}