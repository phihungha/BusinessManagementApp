using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.ViewModels.Utils;
using BusinessManagementApp.ViewModels.ValidationAttributes;
using CommunityToolkit.Mvvm.Input;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels.DetailsVMs
{
    public class CustomerDetailsVM : ViewModelBase
    {
        #region Dependencies

        private CustomersRepo customersRepo;

        #endregion Dependencies
        #region Input properties

        private string id = string.Empty;

        public string Id
        {
            get => id;
            private set => SetProperty(ref id, value);
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

        private string address = string.Empty;

        [Required]
        public string Address
        {
            get => address;
            set => SetProperty(ref address, value, true);
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

        public CustomerDetailsVM(CustomersRepo customersRepo)
        {
            this.customersRepo = customersRepo;

            Save = new AsyncRelayCommand(SaveCustomer);
            Delete = new AsyncRelayCommand(DeleteCustomer);
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Customers)
                );
        }

        public override async void LoadData(object? id = null)
        {
            if (id != null)
            {
                IsEditMode = true;
                await LoadCustomer((string)id);
            }

            CanSave = true;
        }

        private async Task LoadCustomer(string id)
        {
            Customer customer = await customersRepo.GetCustomer(id);
            Id = customer.Id;
            Name = customer.Name;
            Birthday = customer.Birthday;
            Email = customer.Email;
            Gender = customer.Gender;
            Phone = customer.Phone;
            Address = customer.Address;
        }

        private async Task SaveCustomer()
        {
            ValidateAllProperties();
            if (HasErrors)
                return;

            var customer = new Customer()
            {
                Id = Id,
                Name = Name,
                Gender = Gender,
                Birthday = Birthday,
                Phone = Phone,
                Email = Email,
                Address = Address,
            };

            if (IsEditMode)
            {
                await customersRepo.UpdateCustomer(Id, customer);
            }
            else
            {
                await customersRepo.AddCustomer(customer);
            }

            // Navigate back to list screen
            WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Customers);
        }

        private async Task DeleteCustomer()
        {
            await customersRepo.DeleteCustomer(Id);
            WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Customers);
        }
    }
}
