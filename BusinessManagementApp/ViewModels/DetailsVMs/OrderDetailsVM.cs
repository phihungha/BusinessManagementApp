using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.Utils;
using BusinessManagementApp.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using static BusinessManagementApp.ViewModels.DetailsVMs.OrderDetailsVM;
using static BusinessManagementApp.ViewModels.DetailsVMs.SelectProductOrderItemsVM;

namespace BusinessManagementApp.ViewModels.DetailsVMs
{
    public class OrderDetailsVM : ViewModelBase
    {
        #region Dependencies

        private OrdersRepo ordersRepo;

        #endregion Dependencies

        #region Input properties

        private ObservableCollection<OrderItemVM> orderItemVMs { get; } = new();

        private DateTime creationTime = new DateTime();

        private Customer selectedCustomer = new();

        public Customer SelectedCustomer
        {
            get => selectedCustomer;
            set => SetProperty(ref selectedCustomer, value);
        }

        private ObservableCollection<OrderItem> OrderItems { get; } = new();

        public ICollectionView OrderItemsView { get; }

        public class OrderItemVM : ViewModelBase
        {
            private OrderDetailsVM parentVM;

            public Product Product { get; }         

            private int quantity;

            public int Quantity
            {
                get => quantity;
                set
                {
                    SetProperty(ref quantity, value);
                    OrderedPrice = value * Product.Price;
                }
            }

            private decimal orderedPrice;

            public decimal OrderedPrice
            {
                get => orderedPrice;
                set
                {
                    parentVM.TotalPrice -= orderedPrice;
                    SetProperty(ref orderedPrice, value);
                    parentVM.TotalPrice += value;
                }
            }

            public OrderItemVM(Product product, int quantity, OrderDetailsVM parentVM)
            {
                this.parentVM = parentVM;
                Product = product;
                Quantity = quantity;
            }
        }

        private int id = 0;

        public int Id
        {
            get => id;
            private set => SetProperty(ref id, value);
        }

        private OrderStatus status = OrderStatus.Pending;

        public OrderStatus Status
        {
            get => status;
            set => SetProperty(ref status, value, true);
        }

        private string address = string.Empty;

        [Required]
        public string Address
        {
            get => address;
            set => SetProperty(ref address, value, true);
        }

        private string employeeName = string.Empty;

        public string EmployeeName
        {
            get => employeeName;
            set => SetProperty(ref employeeName, value, true);
        }

        private string voucher = string.Empty;

        public string Voucher
        {
            get => voucher;
            set => SetProperty(ref voucher, value, true);
        }

        private decimal netPrice = 0;

        public decimal NetPrice
        {
            get => netPrice;
            set => SetProperty(ref netPrice, value, true);
        }

        private decimal totalPrice = 0;

        public decimal TotalPrice
        {
            get => totalPrice;
            set => SetProperty(ref totalPrice, value, true);
        }

        private double vatRate = 0;

        public double VatRate
        {
            get => vatRate;
            set => SetProperty(ref vatRate, value, true);
        }

        private double totalAmount = 0;

        public double TotalAmount
        {
            get => totalAmount;
            set => SetProperty(ref totalAmount, value, true);
        }

        #endregion Input properties

        #region Button enable/disable logic

        private bool isEditMode = false;

        private bool IsEditMode
        {
            get => isEditMode;
            set
            {
                SetProperty(ref isEditMode, value);
            }
        }

        private bool canSave = false;

        public bool CanSave
        {
            get => canSave;
            private set => SetProperty(ref canSave, value);
        }

        private bool canComplete = false;

        public bool CanComplete
        {
            get => canComplete;
            private set => SetProperty(ref canComplete, value);
        }

        private bool canCancel = false;

        public bool CanCancel
        {
            get => canCancel;
            private set => SetProperty(ref canCancel, value);
        }

        private bool canReturn = false;

        public bool CanReturn
        {
            get => canReturn;
            private set => SetProperty(ref canReturn, value);
        }

        #endregion Button enable/disable logic

        #region Commands for buttons

        public ICommand SelectProducts { get; }
        public ICommand SelectCustomer { get; }
        public ICommand Save { get; private set; }
        public ICommand Cancel { get; private set; }
        public ICommand Complete { get; private set; }
        public ICommand Terminate { get; private set; }
        public ICommand Return { get; private set; }
        public ICommand CreateCustomer { get; private set; }

        #endregion Commands for buttons

        public OrderDetailsVM(OrdersRepo ordersRepo)
        {
            this.ordersRepo = ordersRepo;

            var collectionViewSource = new CollectionViewSource() { Source = orderItemVMs };
            OrderItemsView = collectionViewSource.View;
            CreateCustomer = new RelayCommand(ExecuteCreateCustomer);
            SelectCustomer = new RelayCommand(ExecuteSelectCustomers);
            SelectProducts = new RelayCommand(ExecuteSelectProducts);
            Save = new AsyncRelayCommand(SaveOrder);
            Terminate = new AsyncRelayCommand(TerminateOrder);
            Return = new AsyncRelayCommand(ReturnOrder);
            Complete = new AsyncRelayCommand(CompleteOrder);
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Orders)
                );
        }

        private void SetOrderItemsValue()
        {
            orderItemVMs.Clear();
            foreach (var orderitem in OrderItems)
            {
                    OrderItemVM orderItemVM;
                    orderItemVM = new OrderItemVM(orderitem.Product, orderitem.Quantity, this);
                    orderItemVMs.Add(orderItemVM);
            }
        }

        private void CalculatePrice()
        {
            TotalPrice = 0;
            foreach(OrderItem orderItem in OrderItems)
            {
                TotalPrice += (orderItem.UnitPrice * orderItem.Quantity);
            }
        }

        private void SetEnableValue()
        {
            if (Status == OrderStatus.Pending)
            {
                CanComplete = true;
                CanCancel = true;
            }
            if (Status == OrderStatus.Completed)
            {
                TimeSpan timeSpan = DateTime.Now - creationTime;
                if (timeSpan.Days < 30)
                {
                    CanReturn = true;
                }
            }
        }

        private void ExecuteCreateCustomer()
        {
            var introMessage = "Create customer for order";
            WorkspaceNavUtils.NavigateToWithExtraAndBackstack(WorkspaceViewName.CreateOrderCustomer, introMessage);
        }

        private void ExecuteSelectCustomers()
        {
            var introMessage = "Select customer for order";
            WorkspaceNavUtils.NavigateToWithExtraAndBackstack(WorkspaceViewName.SelectCustomers, introMessage);
        }

        public override void OnBack(WorkspaceViewName prevViewName, object? extra = null)
        {
            if (extra == null)
            {
                return;
            }

            if (prevViewName == WorkspaceViewName.SelectCustomers)
            {
                SelectedCustomer = (Customer)extra;
            }
            else if (prevViewName == WorkspaceViewName.SelectProductOrderItems)
            {              
                OrderItems.ClearAndAddRange((List<OrderItem>)extra);
                SetOrderItemsValue();
            }
            else if(prevViewName == WorkspaceViewName.CreateOrderCustomer)
            {
                SelectedCustomer = (Customer)extra;
            }
        }

        // Load data from repositories here.
        // An object passed when navigating to this screen is also received here.
        public override async void LoadData(object? id = null)
        {
            if (id != null)
            {
                IsEditMode = true;
                await LoadOrder((int)id);
                SetEnableValue();
            }
            CanSave = true;
        }

        private async Task LoadOrder(int id)
        {
            Order order = await ordersRepo.GetOrder(id);
            Id = order.Id;
            SelectedCustomer = order.Customer;
            Address = order.Address;
            Status = order.Status;
            EmployeeName = order.EmployeeInCharge.Name;
            NetPrice = order.NetPrice;
            TotalPrice = order.TotalPrice;
            TotalAmount = order.TotalAmount;
            VatRate = order.VATRate;
            OrderItems.AddRange(order.Items);
            creationTime = order.CreationTime;
            SetOrderItemsValue();
        }

        private async Task SaveOrder()
        {
            ValidateAllProperties();
            if (HasErrors)
                return;

            var order = new Order()
            {
                Customer = SelectedCustomer,
                Address = Address,
                Status = Status,
                TotalAmount = TotalAmount,
                TotalPrice = TotalPrice,
                NetPrice = NetPrice,
                VATRate = VatRate,
                Items = OrderItems.ToList()
            };

            if (IsEditMode)
            {
                await ordersRepo.UpdateOrder(Id, order);
            }
            else
            {
                await ordersRepo.AddOrder(order);
            }

            // Navigate back to list screen
            WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Orders);
        }

        private void ExecuteSelectProducts()
        {
            var param = new SelectProductOrderItemsVM.Param()
            {
                Title = "Select products for the order",
                OrderItems = OrderItems.ToList()
            };
            WorkspaceNavUtils.NavigateToWithExtraAndBackstack(WorkspaceViewName.SelectProductOrderItems, param);
        }

        private async Task TerminateOrder()
        {
            Status = OrderStatus.Canceled;
            await SaveOrder();
        }

        private async Task ReturnOrder()
        {
            Status = OrderStatus.Returned;
            await SaveOrder();
        }

        private async Task CompleteOrder()
        {
            Status = OrderStatus.Completed;
            await SaveOrder();
        }
    }
}