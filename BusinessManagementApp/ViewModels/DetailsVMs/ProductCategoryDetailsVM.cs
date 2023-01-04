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
    public class ProductCategoryDetailsVM : ViewModelBase
    {
        // Declare dependencies such as repositories here.

        #region Dependencies

        private ProductCategoriesRepo productCategoriesRepo;

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
        public ProductCategoryDetailsVM(ProductCategoriesRepo productCategoriesRepo)
        {
            this.productCategoriesRepo = productCategoriesRepo;

            Save = new AsyncRelayCommand(SaveProductCategory);
            Delete = new RelayCommand(DeleteProductCategory);
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.ProductCategories)
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
                await LoadProductCategory((int)id);
            }

            CanSave = true;
            BusyIndicatorUtils.SetBusyIndicator(false);
        }

        private async Task LoadProductCategory(int id)
        {
            ProductCategory productCategory = await productCategoriesRepo.GetProductCategory(id);
            Id = productCategory.Id;
            Name = productCategory.Name;
            Description = productCategory.Description;
        }

        private async Task SaveProductCategory()
        {
            BusyIndicatorUtils.SetBusyIndicator(true);
            ValidateAllProperties();
            if (HasErrors)
                return;

            var productCategory = new ProductCategory()
            {
                Id = Id,
                Name = Name,
                Description = Description,
            };

            if (IsEditMode)
            {
                await productCategoriesRepo.UpdateProductCategory(Id, productCategory);
            }
            else
            {
                await productCategoriesRepo.AddProductCategory(productCategory);
            }
            BusyIndicatorUtils.SetBusyIndicator(false);
            // Navigate back to list screen
            WorkspaceNavUtils.NavigateTo(WorkspaceViewName.ProductCategories);
        }

        private void DeleteProductCategory()
        {
            ConfirmDialog dialog = new ConfirmDialog(
            "Delete product category",
            "Do you want to delete this product category?\n" +
            "This action cannot be undone!");
            dialog.Closed += async (sender, eventArgs) =>
            {
                if (dialog.IsConfirmed)
                {
                    BusyIndicatorUtils.SetBusyIndicator(true);
                    await productCategoriesRepo.DeleteProductCategory(Id);
                    BusyIndicatorUtils.SetBusyIndicator(false);
                    WorkspaceNavUtils.NavigateTo(WorkspaceViewName.ProductCategories);
                }
            };
            dialog.Show();
        }
    }
}