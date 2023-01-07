using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.BusyIndicator;
using BusinessManagementApp.ViewModels.Filters;
using BusinessManagementApp.ViewModels.Navigation;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels
{
    public class OrdersVM : ViewModelBase
    {
        private OrdersRepo ordersRepo;

        private ObservableCollection<Order> orders { get; } = new();

        public ICollectionView OrdersView { get; }

        #region Filters

        private OrderContainsFilter containsFilter;
        private TimeRangeFilter<Order> creationTimeFilter;
        private TimeRangeFilter<Order> completionTimeFilter;
        private OrderStatusFilter statusFilter;

        #endregion Filters

        #region Filter properties

        public string SearchText
        {
            get => containsFilter.SearchText;
            set
            {
                SetProperty(containsFilter.SearchText, value, containsFilter,
                            (obj, val) => obj.SearchText = val);
                OrdersView.Refresh();
            }
        }

        public OrderInfoSearchBy SearchBy
        {
            get => containsFilter.SearchBy;
            set
            {
                SetProperty(containsFilter.SearchBy, value, containsFilter,
                            (obj, val) => obj.SearchBy = val);
                OrdersView.Refresh();
            }
        }

        public bool StatusFilterEnabled
        {
            get => statusFilter.IsEnabled;
            set
            {
                SetProperty(statusFilter.IsEnabled, value, statusFilter,
                            (obj, val) => obj.IsEnabled = val);
                OrdersView.Refresh();
            }
        }

        public OrderStatus StatusFilterOption
        {
            get => statusFilter.Status;
            set
            {
                SetProperty(statusFilter.Status, value, statusFilter,
                            (obj, val) => obj.Status = val);
                OrdersView.Refresh();
            }
        }

        public bool CreationStartTimeFilterEnabled
        {
            get => creationTimeFilter.StartTimeFilterEnabled;
            set
            {
                SetProperty(creationTimeFilter.StartTimeFilterEnabled, value, creationTimeFilter,
                            (obj, val) => obj.StartTimeFilterEnabled = val);
                OrdersView.Refresh();
            }
        }

        public DateTime CreationStartTimeFilterOption
        {
            get => creationTimeFilter.StartTime;
            set
            {
                SetProperty(creationTimeFilter.StartTime, value, creationTimeFilter,
                            (obj, val) => obj.StartTime = val);
                OrdersView.Refresh();
            }
        }

        public bool CreationEndTimeFilterEnabled
        {
            get => creationTimeFilter.EndTimeFilterEnabled;
            set
            {
                SetProperty(creationTimeFilter.EndTimeFilterEnabled, value, creationTimeFilter,
                            (obj, val) => obj.EndTimeFilterEnabled = val);
                OrdersView.Refresh();
            }
        }

        public DateTime CreationEndTimeFilterOption
        {
            get => creationTimeFilter.EndTime;
            set
            {
                SetProperty(creationTimeFilter.EndTime, value, creationTimeFilter,
                            (obj, val) => obj.EndTime = val);
                OrdersView.Refresh();
            }
        }

        public bool CompletionStartTimeFilterEnabled
        {
            get => completionTimeFilter.StartTimeFilterEnabled;
            set
            {
                SetProperty(completionTimeFilter.StartTimeFilterEnabled, value, completionTimeFilter,
                            (obj, val) => obj.StartTimeFilterEnabled = val);
                OrdersView.Refresh();
            }
        }

        public DateTime CompletionStartTimeFilterOption
        {
            get => completionTimeFilter.StartTime;
            set
            {
                SetProperty(completionTimeFilter.StartTime, value, completionTimeFilter,
                            (obj, val) => obj.StartTime = val);
                OrdersView.Refresh();
            }
        }

        public bool CompletionEndTimeFilterEnabled
        {
            get => completionTimeFilter.EndTimeFilterEnabled;
            set
            {
                SetProperty(completionTimeFilter.EndTimeFilterEnabled, value, completionTimeFilter,
                            (obj, val) => obj.EndTimeFilterEnabled = val);
                OrdersView.Refresh();
            }
        }

        public DateTime CompletionEndTimeFilterOption
        {
            get => completionTimeFilter.EndTime;
            set
            {
                SetProperty(completionTimeFilter.EndTime, value, completionTimeFilter,
                            (obj, val) => obj.EndTime = val);
                OrdersView.Refresh();
            }
        }

        #endregion Filter properties

        public ICommand AddOrder { get; }
        public ICommand Search { get; }
        public ICommand Edit { get; }

        public bool AllowAdd { get; } = false;

        public OrdersVM(OrdersRepo ordersRepo, SessionsRepo sessionsRepo)
        {
            if (sessionsRepo.CurrentPosition.CanManageOrders)
            {
                AllowAdd = true;
            }

            this.ordersRepo = ordersRepo;

            containsFilter = new OrderContainsFilter() { IsEnabled = true };
            completionTimeFilter = new TimeRangeFilter<Order>(order => order.CompletionTime, containsFilter);
            creationTimeFilter = new TimeRangeFilter<Order>(order => order.CreationTime, completionTimeFilter);
            statusFilter = new OrderStatusFilter(creationTimeFilter) { IsEnabled = true };

            var collectionViewSource = new CollectionViewSource() { Source = orders };
            OrdersView = collectionViewSource.View;
            OrdersView.Filter = FilterList;

            AddOrder = new RelayCommand(() => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.OrderDetails));
            Search = new RelayCommand(() => OrdersView.Refresh());
            Edit = new RelayCommand<int?>(id => OpenDetailsView(id));

            LoadData();
        }

        private bool FilterList(object item)
        {
            var order = (Order)item;
            return statusFilter.Filter(order);
        }

        private void OpenDetailsView(int? id)
        {
            if (id == null)
            {
                throw new ArgumentException("ID is null");
            }

            // Navigate to details screen
            WorkspaceNavUtils.NavigateToWithExtra(WorkspaceViewName.OrderDetails, id);
        }

        private async void LoadData()
        {
            BusyIndicatorUtils.SetBusyIndicator(true);
            orders.AddRange(await ordersRepo.GetOrders());
            BusyIndicatorUtils.SetBusyIndicator(false);
        }
    }
}