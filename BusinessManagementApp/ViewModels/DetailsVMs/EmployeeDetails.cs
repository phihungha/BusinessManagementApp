using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.ViewModels.Utils;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.ValidationAttributes;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels.DetailsVMs
{
    public class EmployeeDetails : ViewModelBase
    {
        // Declare dependencies such as repositories here.
        #region Dependencies

        private EmployeeRepo employeeRepo;
        private DepartmentsRepo departmentsRepo;

        #endregion Dependencies

        #region Combobox items
        public ObservableCollection<Department> Departments { get; } = new();
        #endregion

        // Properties for inputs on the screen
        // Remember to declare validation attributes when appropriate.
        // List of validation attributes: https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-7.0
        // Check ViewModels/ValidationAttributes.cs for custom validation attributes.
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

        private Department department = new();

        public Department Department
        {
            get => department;
            set => SetProperty(ref department, value);
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

        // Declare dependencies (e.g repositories) as constructor parameters
        // Go into Startup.cs to add new depencencies if needed
        public EmployeeDetails(EmployeeRepo employeeRepo, DepartmentsRepo departmentsRepo)
        {
            this.employeeRepo = employeeRepo;
            this.departmentsRepo = departmentsRepo;

            Save = new AsyncRelayCommand(SaveEmployee);
            Delete = new AsyncRelayCommand(DeleteEmployee);
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.EmployeeInfo)
                );
        }

        // Load data from repositories here.
        // An object passed when navigating to this screen is also received here.
        public override async void LoadData(object? id = null)
        {
            Departments.AddRange(await departmentsRepo.GetDepartments());

            if (id != null)
            {
                IsEditMode = true;
                await LoadEmployee((string)id);
            }

            CanSave = true;
        }

        private async Task LoadEmployee(string id)
        {
            Employee employee = await employeeRepo.GetEmployee(id);
            Id = employee.Id;
            Name = employee.Name;
            CitizenId = employee.CitizenId;
            BirthDate = employee.BirthDate;
            Education = employee.Education;
            PhoneNumber = employee.PhoneNumber;
            Email = employee.Email;
            Address = employee.Address;
            Department = employee.Department;
        }

        private async Task SaveEmployee()
        {
            ValidateAllProperties();
            if (HasErrors)
                return;

            var employee = new Employee()
            {
                Id = Id,
                Name = Name,
                CitizenId = CitizenId,
                BirthDate = BirthDate,
                PhoneNumber = PhoneNumber,
                Email = Email,
                Address = Address,
                Department = Department
            };

            if (IsEditMode)
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