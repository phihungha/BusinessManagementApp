using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.ViewModels.Utils;
using BusinessManagementApp.ViewModels.ValidationAttributes;
using BusinessManagementApp.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels.DetailsVMs
{
    public class OrderDetailsVM : ViewModelBase
    {
        #region Dependencies

        private OrdersRepo ordersRepo;

        #endregion Dependencies
        #region Input properties

        private int id = 0;

        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        private string name = string.Empty;

        [Required]
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private OrderStatus status = OrderStatus.Pending;

        public OrderStatus Status
        {
            get => status;
            set => SetProperty(ref status, value, true);
        }

        private Gender gender = Gender.Male;

        public Gender Gender
        {
            get => gender;
            set => SetProperty(ref gender, value, true);
        }

        private DateTime birthDate = new DateTime(2000, 1, 1);

        public DateTime BirthDate
        {
            get => birthDate;
            set => SetProperty(ref birthDate, value, true);
        }

        private string email = string.Empty;

        [EmailAddress]
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value, true);
        }

        private string phoneNumber = string.Empty;

        [PhoneNumber]
        public string PhoneNumber
        {
            get => phoneNumber;
            set => SetProperty(ref phoneNumber, value, true);
        }

        private string customerAddress = string.Empty;

        [Required]
        public string CustomerAddress
        {
            get => customerAddress;
            set => SetProperty(ref customerAddress, value, true);
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
                CanDelete = value;
            }
        }

        private bool canSave = false;

        public bool CanSave
        {
            get => canSave;
            private set => SetProperty(ref canSave, value);
        }

        private bool canDelete = false;

        public bool CanDelete
        {
            get => canDelete;
            private set => SetProperty(ref canDelete, value);
        }

        #endregion Button enable/disable logic
        #region Commands for buttons

        public ICommand Save { get; private set; }
        public ICommand Delete { get; private set; }
        public ICommand Cancel { get; private set; }

        #endregion Commands for buttons

        public OrderDetailsVM(OrdersRepo ordersRepo)
        {
            this.ordersRepo = ordersRepo;

            Save = new AsyncRelayCommand(SaveOrder);
            Delete = new AsyncRelayCommand(DeleteOrder);
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Orders)
                );
        }

        // Load data from repositories here.
        // An object passed when navigating to this screen is also received here.
        public override async void LoadData(object? id = null)
        {           

            if (id != null)
            {
                IsEditMode = true;
                await LoadOrder((int)id);
            }

            CanSave = true;
        }

        private async Task LoadOrder(int id)
        {
            Order order = await ordersRepo.GetOrder(id);
            Name = order.Customer.Name;
            Gender = order.Customer.Gender;
            PhoneNumber = order.Customer.Phone;
            BirthDate = order.Customer.Birthday;
            Email = order.Customer.Email;
            CustomerAddress = order.Customer.Address;
            Address = order.Address;
            Status =order.Status;
            EmployeeName = order.EmployeeInCharge.Name;
            NetPrice = order.NetPrice;
            TotalPrice = order.TotalPrice;
            TotalAmount = order.TotalAmount;
            VatRate = order.VATRate;
            Id = order.Id;
        }

        private async Task SaveOrder()
        {
            ValidateAllProperties();
            if (HasErrors)
                return;

            var order = new Order()
            {
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

        private async Task DeleteOrder()
        {
            await ordersRepo.DeleteOrder(Id);
            WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Orders);
        }
    }
}
