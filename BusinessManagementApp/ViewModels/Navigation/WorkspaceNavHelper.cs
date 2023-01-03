using BusinessManagementApp.ViewModels.DetailsVMs;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.ViewModels.Navigation
{
    /// <summary>
    /// Helper class for workspace navigation.
    /// </summary>
    public class WorkspaceNavHelper
    {
        private WorkspaceVM workspaceVM;

        private Stack<NavBackstackItem> navigationBackstack = new();

        public WorkspaceNavHelper(WorkspaceVM workspaceVM)
        {
            this.workspaceVM = workspaceVM;

            WeakReferenceMessenger.Default.Register<WorkspaceNavMessage>(
                    this, (r, m) => HandleNavigationMessage(m));

            WeakReferenceMessenger.Default.Register<WorkspaceBackNavMessage>(
                    this, (r, m) => HandleBackNavigationMessage(m));
        }

        private void HandleNavigationMessage(WorkspaceNavMessage message)
        {
            NavigationMessageContent content = message.Value;

            if (content.TargetViewName == null)
            {
                throw new ArgumentNullException(nameof(content.TargetViewName));
            }
            var viewName = (WorkspaceViewName)content.TargetViewName;

            if (content.SaveOnBackstack)
            {
                var item = new NavBackstackItem(viewName, workspaceVM.CurrentViewVM);
                navigationBackstack.Push(item);
            }

            ViewModelBase viewModel = GetViewModelFromViewName(viewName);
            viewModel.LoadData(content.Extra);
            workspaceVM.CurrentViewVM = viewModel;
        }

        private void HandleBackNavigationMessage(WorkspaceBackNavMessage message)
        {
            NavBackstackItem item = navigationBackstack.Pop();
            NavigationMessageContent content = message.Value;
            item.ViewModel.OnBack(item.nextViewName, content.Extra);
            workspaceVM.CurrentViewVM = item.ViewModel;
        }

        public static ViewModelBase GetViewModelFromViewName(WorkspaceViewName targetViewName)
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

                case WorkspaceViewName.CreateOrderCustomer:
                    return serviceProvider.GetRequiredService<CreateOrderCustomerVM>();

                case WorkspaceViewName.ContractTypes:
                    return serviceProvider.GetRequiredService<ContractTypesVM>();

                case WorkspaceViewName.ContractTypeDetails:
                    return serviceProvider.GetRequiredService<ContractTypeDetailsVM>();

                case WorkspaceViewName.Customers:
                    return serviceProvider.GetRequiredService<CustomersVM>();

                case WorkspaceViewName.CustomerDetails:
                    return serviceProvider.GetRequiredService<CustomerDetailsVM>();

                case WorkspaceViewName.Config:
                    return serviceProvider.GetRequiredService<ConfigVM>();

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

                case WorkspaceViewName.ProductCategories:
                    return serviceProvider.GetRequiredService<ProductCategoriesVM>();

                case WorkspaceViewName.ProductCategoryDetails:
                    return serviceProvider.GetRequiredService<ProductCategoryDetailsVM>();

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

                case WorkspaceViewName.SelectProductOrderItems:
                    return serviceProvider.GetRequiredService<SelectProductOrderItemsVM>();

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