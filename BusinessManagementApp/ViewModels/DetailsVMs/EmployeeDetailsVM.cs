using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.BusyIndicator;
using BusinessManagementApp.ViewModels.Navigation;
using BusinessManagementApp.ViewModels.ValidationAttributes;
using BusinessManagementApp.Views.Dialogs;
using CommunityToolkit.Mvvm.Input;
using Refit;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels.DetailsVMs
{
    public class EmployeeDetailsVM : ViewModelBase
    {
        // Declare dependencies such as repositories here.

        #region Dependencies

        private EmployeeRepo employeesRepo;
        private DepartmentsRepo departmentsRepo;
        private PositionsRepo positionsRepo;
        private ContractTypesRepo contractTypesRepo;
        private SessionsRepo sessionsRepo;

        #endregion Dependencies

        #region Combobox items

        public ObservableCollection<Department> Departments { get; } = new();
        public ObservableCollection<Position> Positions { get; } = new();
        public ObservableCollection<ContractType> ContractTypes { get; } = new();

        #endregion Combobox items

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

        // Binding combobox directly to non-enum properties will only work
        // when the class implements IEquatable.
        // Check Department class for an example.
        public Department Department
        {
            get => department;
            set => SetProperty(ref department, value);
        }

        private Position? position = new();

        public Position? Position
        {
            get => position;
            set => SetProperty(ref position, value);
        }

        public ObservableCollection<PositionRecord> PositionRecords { get; } = new();

        public ObservableCollection<Contract> Contracts { get; } = new();

        private Contract? contract = new();

        public Contract? Contract
        {
            get => contract;
            set => SetProperty(ref contract, value);
        }

        private ContractType newContractType = new();
        
        public int? NewContractId { get; set; }

        public ContractType NewContractType
        {
            get => newContractType;
            set
            {
                SetProperty(ref newContractType, value);
                NewContractEndDate = CalculateNewContractEndDate(NewContractStartDate, value);
            }
        }

        private DateTime newContractStartDate = DateTime.Now.Date;

        public DateTime NewContractStartDate
        {
            get => newContractStartDate;
            set
            {
                SetProperty(ref newContractStartDate, value);
                NewContractEndDate = CalculateNewContractEndDate(value, NewContractType);
            }
        }

        private DateTime? newContractEndDate = new();

        public DateTime? NewContractEndDate
        {
            get => newContractEndDate;
            set => SetProperty(ref newContractEndDate, value);
        }

        private bool allowLogin = false;

        public bool AllowLogin
        {
            get => allowLogin;
            set => SetProperty(ref allowLogin, value);
        }

        private string userName = "";

        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        private string newPassword = "";

        public string NewPassword
        {
            get => newPassword;
            set => SetProperty(ref newPassword, value);
        }

        #endregion Input properties

        #region Button enable/disable logic

        private bool isEditMode = false;

        public bool IsEditMode
        {
            get => isEditMode;
            private set
            {
                SetProperty(ref isEditMode, value);
                NewContractEditorDisplayed = !value;
            }
        }

        private bool canSave = false;

        public bool CanSave
        {
            get => canSave;
            private set => SetProperty(ref canSave, value);
        }

        private bool newContractEditorDisplayed = true;

        public bool NewContractEditorDisplayed
        {
            get => newContractEditorDisplayed;
            private set => SetProperty(ref newContractEditorDisplayed, value);
        }

        private bool allowRenewContract = false;

        public bool AllowRenewContract
        {
            get => allowRenewContract;
            set => SetProperty(ref allowRenewContract, value);
        }

        private bool allowTerminateCurrentContract = false;

        public bool AllowTerminateCurrentContract
        {
            get => allowTerminateCurrentContract;
            set => SetProperty(ref allowTerminateCurrentContract, value);
        }

        private bool newContractExists = false;

        public bool NewContractExists
        {
            get => newContractExists;
            set => SetProperty(ref newContractExists, value);
        }

        #endregion Button enable/disable logic

        #region Commands for buttons

        public ICommand ToggleNewContractEditor { get; private set; }
        public ICommand CreateFutureContract { get; private set; }
        public ICommand DeleteFutureContract { get; private set; }
        public ICommand RenewCurrentContract { get; private set; }
        public ICommand TerminateCurrentContract { get; private set; }

        public ICommand Save { get; private set; }
        public ICommand Delete { get; private set; }
        public ICommand Cancel { get; private set; }

        #endregion Commands for buttons

        public bool AllowEdit { get; } = false;

        // Declare dependencies (e.g repositories) as constructor parameters
        // Go into Startup.cs to add new depencencies if needed
        public EmployeeDetailsVM(EmployeeRepo employeesRepo,
                                 DepartmentsRepo departmentsRepo,
                                 PositionsRepo positionsRepo,
                                 ContractTypesRepo contractTypesRepo,
                                 SessionsRepo sessionsRepo)
        {
            if (sessionsRepo.CurrentPosition.CanManageHr)
            {
                AllowEdit = true;
            }

            this.employeesRepo = employeesRepo;
            this.departmentsRepo = departmentsRepo;
            this.positionsRepo = positionsRepo;
            this.contractTypesRepo = contractTypesRepo;
            this.sessionsRepo = sessionsRepo;

            ToggleNewContractEditor = new RelayCommand(
                () =>
                {
                    NewContractId = null;
                    NewContractEditorDisplayed = !NewContractEditorDisplayed;
                });
            CreateFutureContract = new AsyncRelayCommand(ExecuteCreateFutureContract);
            DeleteFutureContract = new AsyncRelayCommand(ExecuteDeleteFutureContract);
            RenewCurrentContract = new RelayCommand(ExecuteRenewCurrentContract);
            TerminateCurrentContract = new RelayCommand(ExecuteTerminateCurrentContract);

            Save = new AsyncRelayCommand(SaveEmployee);
            Delete = new RelayCommand(DeleteEmployee);
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.EmployeeInfo)
                );
        }

        // Load data from repositories here.
        // An object passed when navigating to this screen is also received here.
        public override async void LoadData(object? id = null)
        {
            BusyIndicatorUtils.SetBusyIndicator(true);

            Departments.AddRange(await departmentsRepo.GetDepartments());
            Positions.AddRange(await positionsRepo.GetPositions());
            ContractTypes.AddRange(await contractTypesRepo.GetContractTypes());

            NewContractType = ContractTypes.First();

            if (id != null)
            {
                if (AllowEdit)
                    IsEditMode = true;
                await LoadEmployee((string)id);
            }

            if (AllowEdit)
                CanSave = true;

            BusyIndicatorUtils.SetBusyIndicator(false);
        }

        private async Task LoadEmployee(string id)
        {
            Employee employee = await employeesRepo.GetEmployee(id);
            Id = employee.Id;
            Name = employee.Name;
            CitizenId = employee.CitizenId;
            BirthDate = employee.BirthDate;
            Education = employee.Education;
            PhoneNumber = employee.PhoneNumber;
            Email = employee.Email;
            Address = employee.Address;
            Department = employee.Department;
            Position = employee.CurrentPosition;
            PositionRecords.AddRange(employee.PositionRecords);
            Contracts.AddRange(employee.Contracts);
            Contract = employee.CurrentContract;

            if (employee.UserName != null)
            {
                UserName = employee.UserName;
                AllowLogin = true;
            }

            // Edit the latest contract if it is not yet active
            if (!Contracts.First().IsCurrent)
            {
                Contract latestContract = Contracts.First();
                NewContractType = latestContract.Type;
                NewContractStartDate = latestContract.StartDate;
                NewContractExists = true;
            }

            if (employee.CurrentContract != null)
            {
                AllowTerminateCurrentContract = true;

                // Permanent contracts cannot be renewed
                if (employee.CurrentContract.Type.Period != null)
                {
                    AllowRenewContract = true;
                }
            }
        }

        private DateTime? CalculateNewContractEndDate(DateTime startDate, ContractType type)
        {
            if (type.Period != null)
            {
                return startDate.AddDays((double)type.Period);
            }
            return null;
        }

        private async Task ExecuteCreateFutureContract()
        {
            var contract = new Contract()
            {
                EmployeeId = Id,
                CompanyRepresentativeEmployeeId = sessionsRepo.CurrentUser.Id,
                Type = NewContractType,
                StartDate = NewContractStartDate,
                EndDate = NewContractEndDate
            };

            BusyIndicatorUtils.SetBusyIndicator(true);

            if (newContractExists)
            {
                Contracts.ClearAndAddRange(await employeesRepo.UpdateFutureContract(Id, contract));
            }
            else
            {
                Contracts.ClearAndAddRange(await employeesRepo.AddFutureContract(Id, contract));
                NewContractExists = true;
            }

            BusyIndicatorUtils.SetBusyIndicator(false);
        }

        private void ExecuteRenewCurrentContract()
        {
            if (Contract?.EndDate == null)
                throw new InvalidOperationException("Cannot renew a permanent contract. Consider terminate it then create a new one.");

            NewContractType = Contract.Type;
            NewContractStartDate = (DateTime)Contract.EndDate;
            NewContractId = Contracts.Select(c => c.Id).Max();
            if (NewContractId == null && NewContractId == Contract.Id)
                NewContractId = null;
            NewContractEditorDisplayed = !NewContractEditorDisplayed;
        }

        private void ExecuteTerminateCurrentContract()
        {
            var dialog = new ConfirmDialog(
                "Terminate current contract?",
                "Do you want to terminate the current contract. This cannot be undone!");
            dialog.Closed += async (sender, eventArgs) =>
            {
                if (dialog.IsConfirmed)
                {
                    BusyIndicatorUtils.SetBusyIndicator(true);
                    Contracts.ClearAndAddRange(await employeesRepo.TerminateCurrentContract(Id));
                    AllowTerminateCurrentContract = false;
                    BusyIndicatorUtils.SetBusyIndicator(false);
                }
            };
            dialog.Show();
        }

        private async Task ExecuteDeleteFutureContract()
        {
            BusyIndicatorUtils.SetBusyIndicator(true);
            Contracts.ClearAndAddRange(await employeesRepo.DeleteFutureContract(Id));
            NewContractExists = false;
            BusyIndicatorUtils.SetBusyIndicator(false);
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
                Education = Education,
                Email = Email,
                Address = Address,
                Department = Department,
                CurrentPosition = Position,
                UserName = UserName,
                NewPassword = NewPassword
            };

            if (!IsEditMode)
            {
                var newContract = new Contract()
                {
                    CompanyRepresentativeEmployeeId = sessionsRepo.CurrentUser.Id,
                    Type = NewContractType,
                    StartDate = NewContractStartDate,
                    EndDate = NewContractEndDate
                };
                employee.CurrentContract = newContract;
            }

            try
            {
                BusyIndicatorUtils.SetBusyIndicator(true);
                if (IsEditMode)
                {
                    await employeesRepo.UpdateEmployee(Id, employee);
                }
                else
                {
                    await employeesRepo.AddEmployee(employee);
                }
                BusyIndicatorUtils.SetBusyIndicator(false);
                WorkspaceNavUtils.NavigateTo(WorkspaceViewName.EmployeeInfo);
            }
            catch (ApiException err)
            {
                BusyIndicatorUtils.SetBusyIndicator(false);
                if (err.Message.Contains("phone_number"))
                {
                    var dialog = new ErrorDialog(
                        "Phone number already exists",
                        "There is already an employee with this phone number.");
                    dialog.Show();
                }
                else if (err.Message.Contains("citizen_id"))
                {
                    var dialog = new ErrorDialog(
                        "Citizen ID already exists",
                        "There is already an employee with this citizen ID.");
                    dialog.Show();
                }
                else if (err.Message.Contains("username"))
                {
                    var dialog = new ErrorDialog(
                        "User name already exists",
                        "There is already an employee with this user name.");
                    dialog.Show();
                }
                else
                {
                    throw err;
                }
            }
        }

        private void DeleteEmployee()
        {
            var dialog = new ConfirmDialog(
                "Delete employee",
                "Do you want to delete this employee?\n" +
                "This action cannot be undone!");
            dialog.Closed += async (sender, eventArgs) =>
            {
                if (dialog.IsConfirmed)
                {
                    BusyIndicatorUtils.SetBusyIndicator(true);
                    await employeesRepo.DeleteEmployee(Id);
                    BusyIndicatorUtils.SetBusyIndicator(false);
                    WorkspaceNavUtils.NavigateTo(WorkspaceViewName.EmployeeInfo);
                }
            };
            dialog.Show();
        }
    }
}