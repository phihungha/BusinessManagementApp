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
    public class PositionDetailsVM : ViewModelBase
    {
        // Declare dependencies such as repositories here.

        #region Dependencies

        private PositionsRepo positionsRepo;

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

        private decimal supplementSalary = 100000;

        public decimal SupplementSalary
        {
            get => supplementSalary;
            set => SetProperty(ref supplementSalary, value);
        }

        private bool canManageAll = false;

        public bool CanManageAll
        {
            get => canManageAll;
            set => SetProperty(ref canManageAll, value);
        }

        private bool canManageOrders = false;

        public bool CanManageOrders
        {
            get => canManageOrders;
            set => SetProperty(ref canManageOrders, value);
        }

        private bool canManageSales = false;

        public bool CanManageSales
        {
            get => canManageSales;
            set => SetProperty(ref canManageSales, value);
        }

        private bool canViewSales = false;

        public bool CanViewSales
        {
            get => canViewSales;
            set => SetProperty(ref canViewSales, value);
        }

        private bool canViewHr = false;

        public bool CanViewHr
        {
            get => canViewHr;
            set => SetProperty(ref canViewHr, value);
        }

        private bool canManageHr = false;

        public bool CanManageHr
        {
            get => canManageHr;
            set => SetProperty(ref canManageHr, value);
        }

        //private List<Permission> permissions = new List<Permission>();
        //public List<Permission> Permissions
        //{
        //    get => permissions;
        //    set => SetProperty(ref permissions, value);
        //}

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
        public PositionDetailsVM(PositionsRepo positionsRepo)
        {
            this.positionsRepo = positionsRepo;

            Save = new AsyncRelayCommand(SavePosition);
            Delete = new RelayCommand(DeletePosition);
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Positions)
                );
        }

        // Load data from repositories here.
        // An object passed when navigating to this screen is also received here.
        public override async void LoadData(object? id = null)
        {
            //
            BusyIndicatorUtils.SetBusyIndicator(true);
            if (id != null)
            {
                IsEditMode = true;
                await LoadPosition((int)id);
            }

            CanSave = true;
            BusyIndicatorUtils.SetBusyIndicator(false);
        }

        private async Task LoadPosition(int id)
        {
            Position position = await positionsRepo.GetPosition(id);
            Id = position.Id;
            Name = position.Name;
            Description = position.Description;
            SupplementSalary = position.SupplementSalary;
            CanManageAll = position.CanManageAll;
            CanManageHr = position.CanManageHr;
            CanViewHr = position.CanViewHr;
            CanViewSales = position.CanViewSales;
            CanManageSales = position.CanManageSales;
            CanManageOrders = position.CanManageOrders;
        }

        private async Task SavePosition()
        {
            BusyIndicatorUtils.SetBusyIndicator(true);
            ValidateAllProperties();
            if (HasErrors)
                return;

            var position = new Position()
            {
                Id = Id,
                Name = Name,
                Description = Description,
                SupplementSalary = SupplementSalary,
                CanManageAll = CanManageAll,
                CanManageHr = CanManageHr,
                CanViewHr = CanViewHr,
                CanViewSales = CanViewSales,
                CanManageOrders = CanManageOrders,
                CanManageSales = CanManageSales
            };

            if (IsEditMode)
            {
                await positionsRepo.UpdatePosition(Id, position);
            }
            else
            {
                await positionsRepo.AddPosition(position);
            }
            BusyIndicatorUtils.SetBusyIndicator(false);
            // Navigate back to list screen
            WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Positions);
        }

        private void DeletePosition()
        {
            ConfirmDialog dialog = new ConfirmDialog(
                "Delete position",
                "Do you want to delete this position?\n" +
                "This action cannot be undone!");
            dialog.Closed += async (sender, eventArgs) =>
            {
                if (dialog.IsConfirmed)
                {
                    BusyIndicatorUtils.SetBusyIndicator(true);
                    await positionsRepo.DeletePosition(Id);
                    BusyIndicatorUtils.SetBusyIndicator(false);
                    WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Positions);
                }
            };
            dialog.Show();
        }
    }
}