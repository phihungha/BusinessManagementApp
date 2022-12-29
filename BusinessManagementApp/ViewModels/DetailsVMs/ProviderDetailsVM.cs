using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.ViewModels.Utils;
using CommunityToolkit.Mvvm.Input;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels.DetailsVMs
{
    public class ProviderDetailsVM : ViewModelBase
    {
        // Declare dependencies such as repositories here.
        #region Dependencies

        private ProvidersRepo providersRepo;

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

        // Declare dependencies (e.g repositories) as constructor parameters
        // Go into Startup.cs to add new depencencies if needed
        public ProviderDetailsVM(ProvidersRepo providersRepo)
        {
            this.providersRepo = providersRepo;


            Save = new AsyncRelayCommand(SaveProvider);
            Delete = new AsyncRelayCommand(DeleteProvider);
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Providers)
                );
        }

        // Load data from repositories here.
        // An object passed when navigating to this screen is also received here.
        public override async void LoadData(object? id = null)
        {


            if (id != null)
            {
                IsEditMode = true;
                await LoadProvider((int)id);
            }

            CanSave = true;
        }

        private async Task LoadProvider(int id)
        {
            Provider provider = await providersRepo.GetProvider(id);
            Id = provider.Id;
            Name = provider.Name;
            Description = provider.Description;
        }

        private async Task SaveProvider()
        {
            ValidateAllProperties();
            if (HasErrors)
                return;

            var provider = new Provider()
            {
                Id = Id,
                Name = Name,
                Description = Description,
            };

            if (IsEditMode)
            {
                await providersRepo.UpdateProvider(Id, provider);
            }
            else
            {
                await providersRepo.AddProvider(provider);
            }

            // Navigate back to list screen
            WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Providers);
        }

        private async Task DeleteProvider()
        {
            await providersRepo.DeleteProvider(Id);
            WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Providers);
        }
    }
}