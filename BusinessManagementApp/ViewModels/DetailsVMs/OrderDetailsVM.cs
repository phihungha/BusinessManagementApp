using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.BusyIndicator;
using BusinessManagementApp.ViewModels.Navigation;
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

namespace BusinessManagementApp.ViewModels.DetailsVMs
{
    public class OrderDetailsVM : ViewModelBase
    {
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

        #region Dependencies

        private CustomersRepo customersRepo;
        private VouchersRepo vouchersRepo;
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

        private ObservableCollection<Voucher> AppliedVouchers { get; } = new();

        private ObservableCollection<OrderItem> OrderItems { get; } = new();

        public ICollectionView OrderItemsView { get; }

        public ICollectionView VouchersView { get; set; }

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

        private Employee employeeInCharge;

        public Employee EmployeeInCharge
        {
            get => employeeInCharge;
            set => SetProperty(ref employeeInCharge, value, true);
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
            set
            {
                SetProperty(ref totalPrice, value, true);
                NetPrice = value + value * (decimal)VatRate;
            }
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

        private bool readOnly = false;

        public bool ReadOnly
        {
            get => readOnly;
            private set => SetProperty(ref readOnly, value);
        }

        private bool isEditMode = false;

        private bool IsEditMode
        {
            get => isEditMode;
            set
            {
                SetProperty(ref isEditMode, value);
                ReadOnly = value;
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
        public ICommand ApplyVoucher { get; private set; }
        public ICommand DeleteVoucher { get; private set; }

        #endregion Commands for buttons

        public bool AllowEdit { get; } = false;

        public OrderDetailsVM(OrdersRepo ordersRepo,
                              VouchersRepo vouchersRepo,
                              CustomersRepo customersRepo,
                              SessionsRepo sessionsRepo,
                              ConfigRepo configRepo)
        {
            if (sessionsRepo.CurrentPosition.CanManageOrders)
            {
                AllowEdit = true;
            }

            EmployeeInCharge = sessionsRepo.CurrentUser;
            VatRate = configRepo.Config.VATRate;

            this.ordersRepo = ordersRepo;
            this.vouchersRepo = vouchersRepo;
            this.customersRepo = customersRepo;

            var collectionViewSource = new CollectionViewSource() { Source = orderItemVMs };

            DeleteVoucher = new RelayCommand<string>(id => ExcuteDeleteVoucher(id));
            ApplyVoucher = new RelayCommand(ExcuteApplyVoucher);

            OrderItemsView = collectionViewSource.View;
            VouchersView = new CollectionViewSource { Source = AppliedVouchers }.View;

            SelectCustomer = new RelayCommand(ExecuteSelectCustomers);
            SelectProducts = new RelayCommand(ExecuteSelectProducts);

            Save = new AsyncRelayCommand(SaveOrder);
            Return = new AsyncRelayCommand(ReturnOrder);
            Complete = new AsyncRelayCommand(CompleteOrder);

            Terminate = new AsyncRelayCommand(TerminateOrder);
            Cancel = new RelayCommand(() => WorkspaceNavUtils.NavigateBack());
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

        private async void ExcuteDeleteVoucher(string? code)
        {
            if (code == null)
            {
                throw new ArgumentException("Code is null");
            }
            Voucher voucher = await vouchersRepo.GetVoucher(code);
            AppliedVouchers.Remove(voucher);
            VouchersView.Refresh();
            CalculateTotalPrice();
        }

        private async void ExcuteApplyVoucher()
        {
            Voucher voucher = await vouchersRepo.GetVoucher(Voucher);
            if (AppliedVouchers.Count == 0)
            {
                if (DateTime.Now > voucher.ReleaseDate && DateTime.Now < voucher.ExpiryDate)
                {
                    AppliedVouchers.Add(voucher);
                    VouchersView.Refresh();
                }
            }
            else
            {
                foreach (Voucher voucher1 in AppliedVouchers.ToList())
                {
                    if (!(voucher1.Code == voucher.Code))
                    {
                        if (DateTime.Now > voucher.ReleaseDate && DateTime.Now < voucher.ExpiryDate)
                        {
                            AppliedVouchers.Add(voucher);
                            VouchersView.Refresh();
                        }
                    }
                }
            }
            CalculateTotalPrice();
        }

        private void CalculateTotalPrice()
        {
            double discount = 0;
            double temp = (double)NetPrice;
            //No applied product
            foreach (Voucher voucher in AppliedVouchers)
            {
                if (voucher.Type.AppliedProducts.Count == 0)
                {
                    //Check condition of voucher that has  no applied product
                    if (voucher.Type.MinNetPrice <= NetPrice)
                    {
                        if (voucher.Type.DiscountType == DiscountType.Raw)
                        {
                            discount += voucher.Type.DiscountValue;
                        }
                        else
                        {
                            discount += (double)NetPrice * voucher.Type.DiscountValue;
                        }
                    }
                }
                else //Have applied product
                {
                    decimal ProductsNetPrice = 0;
                    //Get Products'Net price to check condition of voucher that has no applied product
                    foreach (OrderItem orderItem in OrderItems)
                    {
                        if (voucher.Type.AppliedProducts.Exists(i => i.Id == orderItem.Product.Id))
                        {
                            ProductsNetPrice += orderItem.UnitPrice * orderItem.Quantity * (decimal)(1 + VatRate);
                        }
                    }
                    //Begin calculate discount money of voucher that has no applied product
                    if (ProductsNetPrice >= voucher.Type.MinNetPrice)
                    {
                        if (voucher.Type.DiscountType == DiscountType.Raw)
                        {
                            foreach (OrderItem orderItem in OrderItems)
                            {
                                if (voucher.Type.AppliedProducts.Exists(i => i.Id == orderItem.Product.Id))
                                {
                                    discount += voucher.Type.DiscountValue;
                                }
                            }
                        }
                        else
                        {
                            foreach (OrderItem orderItem in OrderItems)
                            {
                                if (voucher.Type.AppliedProducts.Exists(i => i.Id == orderItem.Product.Id))
                                {
                                    discount += ((double)orderItem.UnitPrice * orderItem.Quantity * (1 + VatRate)) * voucher.Type.DiscountValue;
                                }
                            }
                        }
                    }
                }
            }
            TotalAmount = temp - discount;
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
                VouchersView = CollectionViewSource.GetDefaultView(AppliedVouchers);
                SetOrderItemsValue();
                CalculateTotalPrice();
            }
            else if (prevViewName == WorkspaceViewName.CreateOrderCustomer)
            {
                SelectedCustomer = (Customer)extra;
            }
            VouchersView = CollectionViewSource.GetDefaultView(AppliedVouchers);
        }

        // Load data from repositories here.
        // An object passed when navigating to this screen is also received here.
        public override async void LoadData(object? id = null)
        {
            BusyIndicatorUtils.SetBusyIndicator(true);
            if (id != null)
            {
                await LoadOrder((int)id);
                if (AllowEdit)
                {
                    IsEditMode = true;
                    SetEnableValue();
                }
            }
            if (AllowEdit) 
                CanSave = true;
            BusyIndicatorUtils.SetBusyIndicator(false);
        }

        private async Task LoadOrder(int id)
        {
            Order order = await ordersRepo.GetOrder(id);
            Id = order.Id;
            SelectedCustomer = order.Customer;
            Address = order.Address;
            Status = order.Status;
            EmployeeInCharge = order.EmployeeInCharge;
            NetPrice = order.NetPrice;
            TotalPrice = order.TotalPrice;
            TotalAmount = order.TotalAmount;
            VatRate = order.VATRate;
            OrderItems.AddRange(order.Items);
            AppliedVouchers.AddRange(order.AppliedVouchers);
            creationTime = order.CreationTime;
            SetOrderItemsValue();
        }

        private async Task SaveOrder()
        {
            BusyIndicatorUtils.SetBusyIndicator(true);
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
                await customersRepo.AddCustomer(SelectedCustomer);
                await ordersRepo.AddOrder(order);
            }
            BusyIndicatorUtils.SetBusyIndicator(false);
            // Navigate back to list screen
            WorkspaceNavUtils.NavigateBack();
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