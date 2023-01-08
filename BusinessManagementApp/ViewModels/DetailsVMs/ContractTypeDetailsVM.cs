using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.ViewModels.BusyIndicator;
using BusinessManagementApp.ViewModels.Navigation;
using BusinessManagementApp.ViewModels.ValidationAttributes;
using BusinessManagementApp.Views.Dialogs;
using CommunityToolkit.Mvvm.Input;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels.DetailsVMs
{
    public class ContractTypeDetailsVM : ViewModelBase
    {
        // Declare dependencies such as repositories here.

        #region Dependencies

        private ContractTypesRepo contractTypesRepo;

        #endregion Dependencies

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

        private decimal baseSalary = 1000000;

        [BaseSalary]
        public decimal BaseSalary
        {
            get => baseSalary;
            set => SetProperty(ref baseSalary, value, true);
        }

        private int? period = 30;

        [Period]
        public int? Period
        {
            get => period;
            set => SetProperty(ref period, value, true);
        }

        private string description = string.Empty;

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
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
        public ContractTypeDetailsVM(ContractTypesRepo contractTypesRepo, SessionsRepo sessionsRepo)
        {
            if (sessionsRepo.CurrentPosition.CanManageConfig)
            {
                AllowEdit = true;
            }

            this.contractTypesRepo = contractTypesRepo;

            Save = new AsyncRelayCommand(SaveContractType);
            Delete = new RelayCommand(DeleteContractType);
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.ContractTypes)
                );
        }

        // Load data from repositories here.
        // An object passed when navigating to this screen is also received here.
        public override async void LoadData(object? id = null)
        {
            BusyIndicatorUtils.SetBusyIndicator(true);
            if (id != null)
            {
                if (AllowEdit)
                    IsEditMode = true;
                await LoadContractType((int)id);
            }

            if (AllowEdit)
                CanSave = true;
            BusyIndicatorUtils.SetBusyIndicator(false);
        }

        private async Task LoadContractType(int id)
        {
            ContractType contractType = await contractTypesRepo.GetContractType(id);
            Id = contractType.Id;
            Name = contractType.Name;
            BaseSalary = contractType.BaseSalary;
            Period = contractType.Period;
            Description = contractType.Description;
        }

        private async Task SaveContractType()
        {
            ValidateAllProperties();
            if (HasErrors)
                return;

            BusyIndicatorUtils.SetBusyIndicator(true);
            var contracttype = new ContractType()
            {
                Id = Id,
                Name = Name,
                Period = Period,
                BaseSalary = BaseSalary,
                Description = Description,
            };

            if (IsEditMode)
            {
                await contractTypesRepo.UpdateContractType(Id, contracttype);
            }
            else
            {
                await contractTypesRepo.AddContractType(contracttype);
            }
            BusyIndicatorUtils.SetBusyIndicator(false);
            WorkspaceNavUtils.NavigateTo(WorkspaceViewName.ContractTypes);
        }

        private void DeleteContractType()
        {
            ConfirmDialog dialog = new ConfirmDialog(
            "Delete contract type",
            "Do you want to delete this contract type?\n" +
            "This action cannot be undone!");
            dialog.Closed += async (sender, eventArgs) =>
            {
                if (dialog.IsConfirmed)
                {
                    BusyIndicatorUtils.SetBusyIndicator(true);
                    await contractTypesRepo.DeleteContractType(Id);
                    BusyIndicatorUtils.SetBusyIndicator(false);
                    WorkspaceNavUtils.NavigateTo(WorkspaceViewName.ContractTypes);
                }
            };
            dialog.Show();
        }
    }
}