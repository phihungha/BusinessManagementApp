﻿using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Data;
using BusinessManagementApp.ViewModels.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessManagementApp.Views;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Data;
using System.Reactive.Linq;
using BusinessManagementApp.Utils;

namespace BusinessManagementApp.ViewModels
{
    public enum OrderInfoSearchBy
    {
        [Description("ID")]
        Id,

        [Description("Name")]
        Name,

        [Description("Address")]
        Address,
    }

    public class OrdersVM : ViewModelBase
    {
        private OrdersRepo ordersRepo;

        private ObservableCollection<Order> orders { get; } = new();

        public ICollectionView OrdersView { get; }

        public string SearchText { get; set; } = string.Empty;

        public OrderInfoSearchBy SearchBy { get; set; } = OrderInfoSearchBy.Name;

        public ICommand AddOrder { get; }
        public ICommand Search { get; }
        public ICommand Edit { get; }

        private DateTime selectedCreation = new DateTime(1, 1, 1);

        public DateTime SelectedCreation
        {
            get => selectedCreation;
            set => SetProperty(ref selectedCreation, value, true);
        }

        private DateTime selectedCompletion = new DateTime(1, 1, 2);

        public DateTime SelectedCompletion
        {
            get => selectedCompletion;
            set => SetProperty(ref selectedCompletion, value, true);
        }

        public OrdersVM(OrdersRepo ordersRepo)
        {
            this.ordersRepo = ordersRepo;

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
            bool kqDate = (order.CreationTime == selectedCreation) && (order.CompletionTime == selectedCompletion);

            switch (SearchBy)
            {
                case OrderInfoSearchBy.Name:
                    return order.Customer.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) && kqDate;

                case OrderInfoSearchBy.Id:
                    return order.Id.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) && kqDate;

                case OrderInfoSearchBy.Address:
                    return order.Address.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase) && kqDate;

                default:
                    return kqDate;
            }
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
            orders.AddRange(await ordersRepo.GetOrders());
        }

    }
}
