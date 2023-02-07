using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.BusyIndicator;
using BusinessManagementApp.ViewModels.Navigation;
using BusinessManagementApp.ViewModels.ValidationAttributes;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels.DetailsVMs
{
    public class OrderDetailsVM : ViewModelBase
    {
        public class OrderItemVM : ViewModelBase
        {
            private OrderDetailsVM parentVM;

            private OrderItem orderItem;

            public int OrderId
            {
                get => orderItem.OrderId;
            }

            public Product Product
            {
                get => orderItem.Product;
            }

            public decimal UnitPrice
            {
                get => orderItem.UnitPrice;
            }

            public int Quantity
            {
                get => orderItem.Quantity;
                set
                {
                    if (value == 0)
                        return;
                    SetProperty(orderItem.Quantity, value, orderItem,
                                (obj, val) => obj.Quantity = val);
                    OrderedPrice = value * UnitPrice;
                }
            }

            private decimal orderedPrice = 0;

            public decimal OrderedPrice
            {
                get => orderedPrice;
                private set
                {
                    parentVM.TotalPrice -= orderedPrice;
                    SetProperty(ref orderedPrice, value);
                    parentVM.TotalPrice += value;
                }
            }

            public OrderItemVM(OrderItem item, OrderDetailsVM parentVM)
            {
                this.parentVM = parentVM;
                orderItem = item;
                OrderedPrice = item.UnitPrice * item.Quantity;
            }
        }

        #region Dependencies

        private CustomersRepo customersRepo;
        private VouchersRepo vouchersRepo;
        private OrdersRepo ordersRepo;

        #endregion Dependencies

        #region Customer input properties

        private string customerId = string.Empty;

        public string CustomerId
        {
            get => customerId;
            private set => SetProperty(ref customerId, value);
        }

        private string name = string.Empty;

        [Required]
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private Gender gender = Gender.Male;

        public Gender Gender
        {
            get => gender;
            set => SetProperty(ref gender, value, true);
        }

        private DateTime birthday = new DateTime(2000, 1, 1);

        public DateTime Birthday
        {
            get => birthday;
            set => SetProperty(ref birthday, value, true);
        }

        private string email = string.Empty;

        [EmailAddress]
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value, true);
        }

        private string phone = string.Empty;

        [PhoneNumber]
        public string Phone
        {
            get => phone;
            set => SetProperty(ref phone, value, true);
        }

        private string customeraddress = string.Empty;

        [Required]
        public string CustomerAddress
        {
            get => customeraddress;
            set => SetProperty(ref customeraddress, value, true);
        }

        #endregion Customer input properties

        #region Input properties

        private DateTime creationTime = new DateTime();

        private List<OrderItem> orderItems = new();

        public ObservableCollection<OrderItemVM> OrderItemVMs { get; } = new();

        public ObservableCollection<Voucher> AppliedVouchers { get; } = new();

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

        private string voucherCode = string.Empty;

        public string VoucherCode
        {
            get => voucherCode;
            set => SetProperty(ref voucherCode, value, true);
        }

        private decimal totalPrice = 0;

        public decimal TotalPrice
        {
            get => totalPrice;
            set
            {
                SetProperty(ref totalPrice, value);
                CalculateNetPrice(value);
            }
        }

        private decimal netPrice = 0;

        public decimal NetPrice
        {
            get => netPrice;
            set
            {
                SetProperty(ref netPrice, value);
                TotalAmount = value + value * (decimal)VatRate / 100;
            }
        }

        private double vatRate = 0;

        public double VatRate
        {
            get => vatRate;
            set => SetProperty(ref vatRate, value);
        }

        private decimal totalAmount = 0;

        public decimal TotalAmount
        {
            get => totalAmount;
            set
            {
                SetProperty(ref totalAmount, value);
            }
        }

        #endregion Input properties

        #region Button enable/disable logic

        private bool isEnable = true;

        public bool IsEnable
        {
            get => isEnable;
            private set => SetProperty(ref isEnable, value);
        }

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

            DeleteVoucher = new RelayCommand<string>(id => ExecuteDeleteVoucher(id));
            ApplyVoucher = new RelayCommand(ExecuteApplyVouchers);

            SelectCustomer = new RelayCommand(ExecuteSelectCustomers);
            SelectProducts = new RelayCommand(ExecuteSelectProducts);

            Save = new AsyncRelayCommand(SaveOrder);
            Return = new AsyncRelayCommand(ReturnOrder);
            Complete = new AsyncRelayCommand(CompleteOrder);

            Terminate = new AsyncRelayCommand(CancelOrder);
            Cancel = new RelayCommand(() => WorkspaceNavUtils.NavigateBack());
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
                }
            }

            if (AllowEdit)
                CanSave = true;
            if (IsEditMode == true)
                IsEnable = false;
            BusyIndicatorUtils.SetBusyIndicator(false);
        }

        public override void OnBack(WorkspaceViewName prevViewName, object? extra = null)
        {
            if (extra == null)
            {
                return;
            }

            if (prevViewName == WorkspaceViewName.SelectCustomers)
            {
                var customer = (Customer)extra;
                CustomerId = customer.Id;
                Name = customer.Name;
                Gender = customer.Gender;
                Birthday = customer.Birthday;
                CustomerAddress = customer.Address;
                Email = customer.Email;
                Phone = customer.Phone;
            }
            else if (prevViewName == WorkspaceViewName.SelectProductOrderItems)
            {
                orderItems = (List<OrderItem>)extra;
                CreateOrderItemVMs();
            }
        }

        private async Task LoadOrder(int id)
        {
            Order order = await ordersRepo.GetOrder(id);

            Id = order.Id;

            Name = order.Customer.Name;
            Gender = order.Customer.Gender;
            Birthday = order.Customer.Birthday;
            CustomerAddress = order.Customer.Address;
            Email = order.Customer.Email;
            Phone = order.Customer.Phone;

            Address = order.Address;

            EmployeeInCharge = order.EmployeeInCharge;
            creationTime = order.CreationTime;

            orderItems = order.Items;
            CreateOrderItemVMs();

            AppliedVouchers.AddRange(order.AppliedVouchers);

            TotalPrice = order.TotalPrice;
            NetPrice = order.NetPrice;
            VatRate = order.VATRate;
            TotalAmount = order.TotalAmount;

            Status = order.Status;
            EnableStatusButtons();
        }

        private void CreateOrderItemVMs()
        {
            OrderItemVMs.Clear();
            TotalPrice = 0;
            foreach (var item in orderItems)
            {
                OrderItemVM orderItemVM = new OrderItemVM(item, this);
                OrderItemVMs.Add(orderItemVM);
            }
        }

        private void EnableStatusButtons()
        {
            if (Status == OrderStatus.Pending)
            {
                CanComplete = true;
                CanCancel = true;
            }

            if (Status == OrderStatus.Completed)
            {
                TimeSpan timeSinceCreation = DateTime.Now - creationTime;
                if (timeSinceCreation.Days < 30)
                {
                    CanReturn = true;
                }
            }
            if(Status == OrderStatus.Completed || Status == OrderStatus.Canceled || Status == OrderStatus.Returned)
            {
                CanSave = false;
            }
        }

        private async void ExecuteApplyVouchers()
        {
            Voucher voucher = await vouchersRepo.GetVoucher(VoucherCode);
            if (DateTime.Now >= voucher.ReleaseDate &&
                DateTime.Now <= voucher.ExpiryDate &&
                !AppliedVouchers.Contains(voucher))
            {
                AppliedVouchers.Add(voucher);
            }
            CalculateNetPrice(TotalPrice);
        }

        private async void ExecuteDeleteVoucher(string code)
        {
            Voucher voucher = await vouchersRepo.GetVoucher(code);
            AppliedVouchers.Remove(voucher);
            CalculateNetPrice(TotalPrice);
        }

        private void CalculateNetPrice(decimal totalPrice)
        {
            decimal discountPrice = 0;
            foreach (Voucher voucher in AppliedVouchers)
            {
                VoucherType voucherType = voucher.Type;

                if (voucherType.AppliedProducts.Count == 0)
                {
                    discountPrice += CalcDiscountForEntireOrder(voucherType, totalPrice);
                }
                else
                {
                    discountPrice += CalcDiscountPerOrderItems(voucherType);
                }
            }
            NetPrice = totalPrice - discountPrice;
        }

        private decimal CalcDiscountForEntireOrder(VoucherType voucherType, decimal totalPrice)
        {
            if (totalPrice >= voucherType.MinNetPrice)
            {
                return CalcDiscountPriceFromVoucher(voucherType, totalPrice);
            }
            return 0;
        }

        private decimal CalcDiscountPerOrderItems(VoucherType voucherType)
        {
            decimal appliedItemsTotalPrice = 0;
            decimal discountPrice = 0;

            foreach (OrderItem item in orderItems)
            {
                if (voucherType.AppliedProducts.Exists(i => i.Id == item.Product.Id))
                {
                    decimal totalItemsPrice = item.UnitPrice * item.Quantity;
                    appliedItemsTotalPrice += totalItemsPrice;
                    discountPrice += CalcDiscountPriceFromVoucher(voucherType, totalItemsPrice);
                }
            }

            if (appliedItemsTotalPrice < voucherType.MinNetPrice)
                return 0;

            return discountPrice;
        }

        private decimal CalcDiscountPriceFromVoucher(VoucherType voucherType, decimal originalPrice)
        {
            decimal discountValue = voucherType.DiscountValue;
            if (voucherType.DiscountType == DiscountType.Raw)
            {
                return discountValue;
            }
            return originalPrice * discountValue / 100;
        }

        private void ExecuteSelectCustomers()
        {
            var introMessage = "Select customer for order";
            WorkspaceNavUtils.NavigateToWithExtraAndBackstack(
                WorkspaceViewName.SelectCustomers, introMessage);
        }

        private void ExecuteSelectProducts()
        {
            var param = new SelectProductOrderItemsVM.Param()
            {
                Title = "Select products for order",
                OrderItems = orderItems.ToList()
            };
            WorkspaceNavUtils.NavigateToWithExtraAndBackstack(
                WorkspaceViewName.SelectProductOrderItems, param);
        }

        private async Task SaveOrder()
        {
            ValidateAllProperties();
            if (HasErrors)
                return;

            BusyIndicatorUtils.SetBusyIndicator(true);

            var customer = new Customer()
            {
                Id = CustomerId,
                Name = Name,
                Gender = Gender,
                Birthday = Birthday,
                Address = CustomerAddress,
                Email = Email,
                Phone = Phone,
            };

            var order = new Order()
            {
                Customer = customer,
                EmployeeInCharge = EmployeeInCharge,
                Address = Address,
                Status = Status,
                VATRate = VatRate,
                Items = orderItems,
                AppliedVouchers = AppliedVouchers.ToList()
            };

            if (IsEditMode)
            {
                await ordersRepo.UpdateOrder(Id, order);
            }
            else
            {
                await ordersRepo.AddOrder(order);
            }

            BusyIndicatorUtils.SetBusyIndicator(false);
            WorkspaceNavUtils.NavigateBack();
        }

        private async Task CancelOrder()
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