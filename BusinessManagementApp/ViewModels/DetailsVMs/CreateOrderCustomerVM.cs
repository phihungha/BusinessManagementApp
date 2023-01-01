using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Data;
using BusinessManagementApp.ViewModels.Utils;
using BusinessManagementApp.ViewModels.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Reactive.Linq;

namespace BusinessManagementApp.ViewModels.DetailsVMs
{
    public class CreateOrderCustomerVM : ViewModelBase
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
        #region Commands for buttons

        public ICommand Save { get; private set; }
        public ICommand Cancel { get; private set; }

        #endregion Commands for buttons
        public CreateOrderCustomerVM(CustomersRepo customersRepo)
        {
            this.customersRepo = customersRepo;

            Save = new AsyncRelayCommand(SaveCustomer);
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateBack()
                );
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

               // await customersRepo.AddCustomer(customer);


            // Navigate back to list screen
            WorkspaceNavUtils.NavigateBackWithExtra(customer);
        }

    }
}
