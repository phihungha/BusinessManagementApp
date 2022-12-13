using BusinessManagementApp.ViewModels.DetailsVMs;
using BusinessManagementApp.ViewModels.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels
{
    public enum WorkspaceViewName
    {
        Bonuses,
        BonusTypes,
        ContractTypes,
        ContractTypeDetails,
        Customers,
        CustomerDetails,
        Departments,
        DepartmentDetails,
        EmployeeInfo,
        EmployeeInfoDetails,
        Overview,
        OvertimeRecords,
        OvertimeRecordDetails,
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
        SelectOrderItem,
        SkillRating,
        SkillTypes,
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
        public WorkspaceViewName TargetViewName { get; set; }
        public object? Extra { get; set; }

        /// <summary>
        /// Contains the content of a navigation message
        /// including name of view to go to and extra message
        /// </summary>
        /// <param name="targetViewName">Name of view to go to</param>
        /// <param name="extra">An object containing extra message</param>
        public NavigationMessageContent(WorkspaceViewName targetViewName, object? extra)
        {
            TargetViewName = targetViewName;
            Extra = extra;
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
        public WorkspaceNavigationMessage(WorkspaceViewName targetViewName, object? extra = null)
            : base(new NavigationMessageContent(targetViewName, extra))
        {
        }
    }

    public class WorkspaceVM : ObservableObject
    {
        private ObservableObject? currentViewVM
            = App.Current.ServiceProvider.GetRequiredService<OverviewVM>();

        public ObservableObject? CurrentViewVM
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
            WorkspaceViewName.OvertimeRecords,
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
                    this,
                    (r, m) => HandleNavigationMessageContent(m.Value)
                );

            Logout = new RelayCommand(() => MainWindowNavUtils.NavigateTo(MainWindowViewName.Login));
        }

        private void HandleNavigationMessageContent(NavigationMessageContent content)
        {
            ViewModelBase viewModel = GetViewModelFromViewName(content.TargetViewName);
            if (viewModel != null)
            {
                viewModel.LoadData(content.Extra);
            }
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
                    return serviceProvider.GetRequiredService<EmployeeDetails>();

                case WorkspaceViewName.Orders:
                    return serviceProvider.GetRequiredService<OrdersVM>();

                case WorkspaceViewName.OrderDetails:
                    return serviceProvider.GetRequiredService<OrderDetailsVM>();

                case WorkspaceViewName.OvertimeRecords:
                    return serviceProvider.GetRequiredService<OvertimeRecordsVM>();

                case WorkspaceViewName.OvertimeRecordDetails:
                    return serviceProvider.GetRequiredService<OvertimeRecordDetailsVM>();

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

                case WorkspaceViewName.SelectOrderItem:
                    return serviceProvider.GetRequiredService<SelectOrderItemsVM>();

                case WorkspaceViewName.SkillRating:
                    return serviceProvider.GetRequiredService<SkillRatingVM>();

                case WorkspaceViewName.SkillTypes:
                    return serviceProvider.GetRequiredService<SkillTypesVM>();

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