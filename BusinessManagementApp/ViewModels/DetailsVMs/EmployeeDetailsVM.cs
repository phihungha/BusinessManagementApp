using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.Utils;
using BusinessManagementApp.ViewModels.ValidationAttributes;
using CommunityToolkit.Mvvm.Input;
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

        #endregion Dependencies

        private Contract? newContract = null;

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

        private Position position = new();

        public Position Position
        {
            get => position;
            set => SetProperty(ref position, value);
        }

        public ObservableCollection<PositionRecord> PositionRecords { get; } = new();

        public ObservableCollection<Contract> Contracts { get; } = new();

        private Contract contract = new();

        public Contract Contract
        {
            get => contract;
            set => SetProperty(ref contract, value);
        }

        private ContractType newContractType = new();

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

        #endregion Input properties

        #region Button enable/disable logic

        private bool isEditMode = false;

        public bool IsEditMode
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

        private bool newContractEditorDisplayed = false;

        public bool NewContractEditorDisplayed
        {
            get => newContractEditorDisplayed;
            private set => SetProperty(ref newContractEditorDisplayed, value);
        }

        private bool createContractOnSave = false;

        public bool CreateContractOnSave
        {
            get => createContractOnSave;
            set => SetProperty(ref createContractOnSave, value);
        }

        #endregion Button enable/disable logic

        #region Commands for buttons

        public ICommand ToggleNewContractEditor { get; private set; }
        public ICommand CreateNewContract { get; private set; }
        public ICommand RenewCurrentContract { get; private set; }
        public ICommand TerminateCurrentContract { get; private set; }

        public ICommand Save { get; private set; }
        public ICommand Delete { get; private set; }
        public ICommand Cancel { get; private set; }

        #endregion Commands for buttons

        // Declare dependencies (e.g repositories) as constructor parameters
        // Go into Startup.cs to add new depencencies if needed
        public EmployeeDetailsVM(EmployeeRepo employeesRepo,
                                 DepartmentsRepo departmentsRepo,
                                 PositionsRepo positionsRepo,
                                 ContractTypesRepo contractTypesRepo)
        {
            this.employeesRepo = employeesRepo;
            this.departmentsRepo = departmentsRepo;
            this.positionsRepo = positionsRepo;
            this.contractTypesRepo = contractTypesRepo;

            ToggleNewContractEditor = new RelayCommand(
                () => NewContractEditorDisplayed = !NewContractEditorDisplayed
                );
            CreateNewContract = new AsyncRelayCommand(ExecuteCreateNewContract);
            RenewCurrentContract = new RelayCommand(ExecuteRenewCurrentContract);
            TerminateCurrentContract = new AsyncRelayCommand(ExecuteTerminateCurrentContract);

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
            Positions.AddRange(await positionsRepo.GetPositions());
            ContractTypes.AddRange(await contractTypesRepo.GetContractTypes());

            NewContractType = ContractTypes.First();

            if (id != null)
            {
                IsEditMode = true;
                await LoadEmployee((string)id);
            }

            CanSave = true;
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
        }

        private DateTime? CalculateNewContractEndDate(DateTime startDate, ContractType type)
        {
            if (type.Period != null)
            {
                return startDate.AddDays((double)type.Period);
            }
            return null;
        }

        private async Task ExecuteCreateNewContract()
        {
            var contract = new Contract()
            {
                EmployeeId = Id,
                // TODO: Use current logged in user id
                CompanyRepresentativeEmployeeId = "0",
                Type = NewContractType,
                IsCurrent = false,
                StartDate = NewContractStartDate,
                EndDate = NewContractEndDate
            };

            if (IsEditMode)
            {
                Contracts.ClearAndAddRange(await employeesRepo.AddContract(contract));
            }
            else
            {
                newContract = contract;
                CreateContractOnSave = true;
            }
        }

        private void ExecuteRenewCurrentContract()
        {
            if (Contract.EndDate == null)
                throw new InvalidOperationException("Cannot renew a permanent contract. Consider terminate it then create a new one.");

            NewContractType = Contract.Type;
            NewContractStartDate = (DateTime)Contract.EndDate;
            NewContractEditorDisplayed = true;
        }

        private async Task ExecuteTerminateCurrentContract()
        {
            Contracts.ClearAndAddRange(await employeesRepo.TerminateCurrentContract(Id));
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
                Department = Department,
                CurrentPosition = Position
            };

            if (IsEditMode)
            {
                await employeesRepo.UpdateEmployee(Id, employee);
            }
            else
            {
                await employeesRepo.AddEmployee(employee);
                if (newContract != null)
                {
                    await employeesRepo.AddContract(contract);
                }
            }

            // Navigate back to list screen
            WorkspaceNavUtils.NavigateTo(WorkspaceViewName.EmployeeInfo);
        }

        private async Task DeleteEmployee()
        {
            await employeesRepo.DeleteEmployee(Id);
            WorkspaceNavUtils.NavigateTo(WorkspaceViewName.EmployeeInfo);
        }
    }
}