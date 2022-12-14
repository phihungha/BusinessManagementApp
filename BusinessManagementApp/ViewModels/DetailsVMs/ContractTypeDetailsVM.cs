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
    public class ContractTypeDetailsVM : ViewModelBase
    {
        // Declare dependencies such as repositories here.
        #region Dependencies

        private ContractTypesRepo contracttypeRepo;

        #endregion Dependencies


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

        private string baseSalary = string.Empty;
        [BaseSalary]
        public string BaseSalary
        {
            get => baseSalary;
            set => SetProperty(ref baseSalary, value, true);
        }

        private string period = string.Empty;
        [Period]
        public string Period
        {
            get => period;
            set => SetProperty(ref period, value, true);
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
        public ContractTypeDetailsVM(ContractTypesRepo contracttypeRepo)
        {
            this.contracttypeRepo = contracttypeRepo;

            Save = new AsyncRelayCommand(SaveContractType);
            Delete = new AsyncRelayCommand(DeleteContractType);
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.ContractTypes)
                );
        }

        // Load data from repositories here.
        // An object passed when navigating to this screen is also received here.
        public override async void LoadData(object? id = null)
        {

            if (id != null)
            {
                IsEditMode = true;
                await LoadContractType((string)id);
            }

            CanSave = true;
        }

        private async Task LoadContractType(string id)
        {
            ContractType contractType = await contracttypeRepo.GetContractType(id);
            Id = contractType.Id.ToString();
            Name = contractType.Name;
            BaseSalary = contractType.BaseSalary.ToString();
            Period = contractType.Period.ToString();
        }

        private async Task SaveContractType()
        {
            ValidateAllProperties();
            if (HasErrors)
                return;

            var contracttype = new ContractType()
            {
                Id = int.Parse(Id),
                Name = Name,
                Period = int.Parse(Period),
                BaseSalary= decimal.Parse(BaseSalary),
            };

            if (IsEditMode)
            {
                await contracttypeRepo.UpdateContractType(Id, contracttype);
            }
            else
            {
                await contracttypeRepo.AddContractType(contracttype);
            }
        }

        private async Task DeleteContractType()
        {
            await contracttypeRepo.DeleteContractType(Id);
        }
    }
}