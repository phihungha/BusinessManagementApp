using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.ViewModels.Utils;
using CommunityToolkit.Mvvm.Input;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels.DetailsVMs
{
    public class OrderDetailsVM : ViewModelBase
    {
        #region Dependencies

        private OrdersRepo ordersRepo;

        #endregion Dependencies
        #region Input properties

        private DateTime CreationTime = DateTime.Now;

        private DateTime CompletionTime = DateTime.Now.AddDays(14);

        private Customer selectedCustomers = new();

        public Customer SelectedCustomers
        {
            get => selectedCustomers;
            set => SetProperty(ref selectedCustomers, value);
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

        public ICommand SelectCustomers { get; }
        public ICommand Save { get; private set; }
        public ICommand Cancel { get; private set; }
        public ICommand Complete { get; private set; }
        public ICommand Terminate { get; private set; }
        public ICommand Return { get; private set; }

        #endregion Commands for buttons

        public OrderDetailsVM(OrdersRepo ordersRepo)
        {
            this.ordersRepo = ordersRepo;
            SelectCustomers = new RelayCommand(ExecuteSelectCustomers);
            Save = new AsyncRelayCommand(SaveOrder);
            Terminate = new AsyncRelayCommand(TerminateOrder);
            Return = new AsyncRelayCommand(ReturnOrder);
            Complete = new AsyncRelayCommand(CompleteOrder);
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Orders)
                );
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
                TimeSpan timeSpan = CompletionTime - DateTime.Now;
                if (timeSpan.Days < 30)
                {
                    CanReturn = true;
                }
            }

        }

        private void ExecuteSelectCustomers()
        {
            var introMessage = "Select customer for order";
            WorkspaceNavUtils.NavigateToWithExtraAndBackstack(WorkspaceViewName.SelectCustomers, introMessage);
        }
        public override void OnBack(object? extra = null)
        {
            if (extra == null)
            {
                throw new ArgumentException(nameof(extra));
            }
            SelectedCustomers = (Customer)extra;
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
            SelectedCustomers = order.Customer;
            Address = order.Address;
            Status = order.Status;
            EmployeeName = order.EmployeeInCharge.Name;
            NetPrice = order.NetPrice;
            TotalPrice = order.TotalPrice;
            TotalAmount = order.TotalAmount;
            VatRate = order.VATRate;
            CreationTime = order.CreationTime;
            CompletionTime = order.CompletionTime;
        }

        private async Task SaveOrder()
        {
            ValidateAllProperties();
            if (HasErrors)
                return;

            var order = new Order()
            {
                Customer = SelectedCustomers,
                Address = Address,
                Status = Status,
                TotalAmount = TotalAmount,
                TotalPrice = TotalPrice,
                NetPrice = NetPrice,
                VATRate = VatRate
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
