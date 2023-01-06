using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.ViewModels.BusyIndicator;
using BusinessManagementApp.ViewModels.Navigation;
using BusinessManagementApp.Views.Dialogs;
using CommunityToolkit.Mvvm.Input;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels.DetailsVMs
{
    public class SkillTypeDetailsVM : ViewModelBase
    {
        // Declare dependencies such as repositories here.

        #region Dependencies

        private SkillTypesRepo skillTypesRepo;

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

        public bool AllowEdit { get; } = false;

        // Declare dependencies (e.g repositories) as constructor parameters
        // Go into Startup.cs to add new depencencies if needed
        public SkillTypeDetailsVM(SkillTypesRepo skillTypesRepo, SessionsRepo sessionsRepo)
        {
            if (sessionsRepo.CurrentPosition.CanManageConfig)
            {
                AllowEdit = true;
            }

            this.skillTypesRepo = skillTypesRepo;

            Save = new AsyncRelayCommand(SaveSkillType);
            Delete = new RelayCommand(DeleteSkillType);
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.SkillTypes)
                );
        }

        // Load data from repositories here.
        // An object passed when navigating to this screen is also received here.
        public override async void LoadData(object? id = null)
        {
            BusyIndicatorUtils.SetBusyIndicator(true);
            if (id != null)
            {
                IsEditMode = true;
                await LoadSkillType((int)id);
            }

            CanSave = true;
            BusyIndicatorUtils.SetBusyIndicator(false);
        }

        private async Task LoadSkillType(int id)
        {
            SkillType skillType = await skillTypesRepo.GetSkillType(id);
            Id = skillType.Id;
            Name = skillType.Name;
            Description = skillType.Description;
        }

        private async Task SaveSkillType()
        {
            BusyIndicatorUtils.SetBusyIndicator(true);
            ValidateAllProperties();
            if (HasErrors)
                return;

            var skillType = new SkillType()
            {
                Id = Id,
                Name = Name,
                Description = Description,
            };

            if (IsEditMode)
            {
                await skillTypesRepo.UpdateSkillType(Id, skillType);
            }
            else
            {
                await skillTypesRepo.AddSkillType(skillType);
            }
            BusyIndicatorUtils.SetBusyIndicator(false);
            // Navigate back to list screen
            WorkspaceNavUtils.NavigateTo(WorkspaceViewName.SkillTypes);
        }

        private void DeleteSkillType()
        {
            ConfirmDialog dialog = new ConfirmDialog(
                "Delete skill type",
                "Do you want to delete this skill type?\n" +
                "This action cannot be undone!");
            dialog.Closed += async (sender, eventArgs) =>
            {
                if (dialog.IsConfirmed)
                {
                    BusyIndicatorUtils.SetBusyIndicator(true);
                    await skillTypesRepo.DeleteSkillType(Id);
                    BusyIndicatorUtils.SetBusyIndicator(false);
                    WorkspaceNavUtils.NavigateTo(WorkspaceViewName.SkillTypes);
                }
            };
            dialog.Show();
        }
    }
}