using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.ViewModels.Utils;
using BusinessManagementApp.ViewModels.ValidationAttributes;
using CommunityToolkit.Mvvm.Input;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels.DetailsVMs
{
    public class EmployeeInfoDetailsVM : ViewModelBase
    {
        // Declare dependencies such as repositories here.
        #region Dependencies

        private EmployeeRepo employeeRepo;

        #endregion Dependencies

        // Model object
        private Employee employee = new();

        // Properties for inputs on the screen
        #region Input properties

        private string id = "";

        public string Id
        {
            get => id;
            private set => SetProperty(ref id, value);
        }

        private string name = "";

        [Required]
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private string citizenId = string.Empty;

        [CitizenId]
        public string CitizenId
        {
            get => citizenId;
            set => SetProperty(ref citizenId, value, true);
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

        private string education = string.Empty;

        public string Education
        {
            get => education;
            set => SetProperty(ref education, value, true);
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

        private string address = string.Empty;

        [Required]
        public string Address
        {
            get => address;
            set => SetProperty(ref address, value, true);
        }

        #endregion Input properties

        private bool isEditMode = false;

        #region Button enable/disable logic

        private bool canSave = true;

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

        public EmployeeInfoDetailsVM(EmployeeRepo employeeRepo)
        {
            this.employeeRepo = employeeRepo;

            Save = new AsyncRelayCommand(SaveEmployee);
            Delete = new AsyncRelayCommand(DeleteEmployee);
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.EmployeeInfo)
                );
        }

        public override async void ReceiveExtra(object id)
        {
            CanDelete = true;
            isEditMode = true;

            CanSave = false;
            await LoadEmployee((string)id);
            CanSave = true;
        }

        private async Task LoadEmployee(string id)
        {
            employee = await employeeRepo.GetEmployee(id);
            Id = employee.Id;
            Name = employee.Name;
            CitizenId = employee.CitizenId;
            BirthDate = employee.BirthDate;
            Education = employee.Education;
            PhoneNumber = employee.PhoneNumber;
            Email = employee.Email;
            Address = employee.Address;
        }

        private async Task SaveEmployee()
        {
            ValidateAllProperties();
            if (HasErrors)
                return;

            employee.Id = Id;
            employee.Name = Name;
            employee.CitizenId = CitizenId;
            employee.BirthDate = BirthDate;
            employee.PhoneNumber = PhoneNumber;
            employee.Email = Email;
            employee.Address = Address;

            if (isEditMode)
            {
                await employeeRepo.UpdateEmployee(Id, employee);
            }
            else
            {
                await employeeRepo.AddEmployee(employee);
            }
        }

        private async Task DeleteEmployee()
        {
            await employeeRepo.DeleteEmployee(Id);
        }
    }
}