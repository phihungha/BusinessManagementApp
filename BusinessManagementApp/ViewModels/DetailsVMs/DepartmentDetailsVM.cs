using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.BusyIndicator;
using BusinessManagementApp.ViewModels.Navigation;
using BusinessManagementApp.ViewModels.ValidationAttributes;
using BusinessManagementApp.Views.Dialogs;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels.DetailsVMs
{
    public class DepartmentDetailsVM : ViewModelBase
    {
        // Declare dependencies such as repositories here.

        #region Dependencies

        private EmployeeRepo employeeRepo;
        private DepartmentsRepo departmentsRepo;

        #endregion Dependencies

        #region Combobox items

        public ObservableCollection<Employee> Employees { get; } = new();

        #endregion Combobox items

        // Properties for inputs on the screen
        // Remember to declare validation attributes when appropriate.
        // List of validation attributes: https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-7.0
        // Check ViewModels/ValidationAttributes.cs for custom validation attributes.

        #region Input properties

        private int id = 0;

        public int Id
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

        private string phoneNumber = string.Empty;

        [PhoneNumber]
        public string PhoneNumber
        {
            get => phoneNumber;
            set => SetProperty(ref phoneNumber, value, true);
        }

        private string description = string.Empty;

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value, true);
        }

        private Employee head = new();

        public Employee Head
        {
            get => head;
            set => SetProperty(ref head, value);
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

        public bool AllowEdit { get; } = false;

        // Declare dependencies (e.g repositories) as constructor parameters
        // Go into Startup.cs to add new depencencies if needed
        public DepartmentDetailsVM(EmployeeRepo employeeRepo, DepartmentsRepo departmentsRepo, SessionsRepo sessionsRepo)
        {
            if (sessionsRepo.CurrentPosition.CanManageConfig)
            {
                AllowEdit = true;
            }

            this.employeeRepo = employeeRepo;
            this.departmentsRepo = departmentsRepo;

            Save = new AsyncRelayCommand(SaveDepartment);
            Delete = new RelayCommand(DeleteDepartment);
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Departments)
                );
        }

        // Load data from repositories here.
        // An object passed when navigating to this screen is also received here.
        public override async void LoadData(object? id = null)
        {
            BusyIndicatorUtils.SetBusyIndicator(true);
            Employees.AddRange(await employeeRepo.GetEmployees());

            if (id != null)
            {
                if (AllowEdit)
                    IsEditMode = true;
                await LoadDepartment((int)id);
            }
            if (AllowEdit)
                CanSave = true;
            BusyIndicatorUtils.SetBusyIndicator(false);
        }

        private async Task LoadDepartment(int id)
        {
            Department department = await departmentsRepo.GetDepartment(id);
            Id = department.Id;
            Name = department.Name;
            Description = department.Description;
            Head = department.Head;
            PhoneNumber = department.PhoneNumber;
        }

        private async Task SaveDepartment()
        {
            ValidateAllProperties();
            if (HasErrors)
                return;

            BusyIndicatorUtils.SetBusyIndicator(true);

            var department = new Department()
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Head = Head,
                PhoneNumber = PhoneNumber
            };

            if (IsEditMode)
            {
                await departmentsRepo.UpdateDepartment(Id, department);
            }
            else
            {
                await departmentsRepo.AddDepartment(department);
            }
            BusyIndicatorUtils.SetBusyIndicator(false);
            WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Departments);
        }

        private void DeleteDepartment()
        {
            ConfirmDialog dialog = new ConfirmDialog(
                "Delete department",
                "Do you want to delete this department?\n" +
                "This action cannot be undone!");
            dialog.Closed += async (sender, eventArgs) =>
            {
                if (dialog.IsConfirmed)
                {
                    BusyIndicatorUtils.SetBusyIndicator(true);
                    await departmentsRepo.DeleteDepartment(Id);
                    BusyIndicatorUtils.SetBusyIndicator(false);
                    WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Departments);
                }
            };
            dialog.Show();
        }
    }
}