using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.BusyIndicator;
using BusinessManagementApp.ViewModels.Navigation;
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
    public class ProductDetailsVM : ViewModelBase
    {
        #region Dependencies

        private ProductsRepo productsRepo;
        private ProductCategoriesRepo productsCategoriesRepo;
        private ProvidersRepo providersRepo;

        #endregion Dependencies

        #region Combobox items

        public ObservableCollection<ProductCategory> Categories { get; } = new();
        public ObservableCollection<Provider> Providers { get; } = new();

        #endregion Combobox items

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

        private string unit = string.Empty;

        public string Unit
        {
            get => unit;
            set => SetProperty(ref unit, value, true);
        }

        private string description = string.Empty;

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value, true);
        }

        private int stock = 0;

        public int Stock
        {
            get => stock;
            set => SetProperty(ref stock, value, true);
        }

        private decimal price = 0;

        public decimal Price
        {
            get => price;
            set => SetProperty(ref price, value, true);
        }

        private ProductCategory productCategory = new();

        public ProductCategory ProductCategory
        {
            get => productCategory;
            set => SetProperty(ref productCategory, value);
        }

        private Provider selectedProvider = new();

        public Provider SelectedProvider
        {
            get => selectedProvider;
            set => SetProperty(ref selectedProvider, value);
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

        public ProductDetailsVM(ProductsRepo productsRepo, ProductCategoriesRepo productsCategoriesRepo, ProvidersRepo providersRepo, SessionsRepo sessionsRepo)
        {
            if (sessionsRepo.CurrentPosition.CanManageSales)
            {
                AllowEdit = true;
            }

            this.productsRepo = productsRepo;
            this.productsCategoriesRepo = productsCategoriesRepo;
            this.providersRepo = providersRepo;
            Save = new AsyncRelayCommand(SaveProduct);
            Delete = new RelayCommand(DeleteProduct);
            Cancel = new RelayCommand(
                () => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Products)
                );
        }

        public override async void LoadData(object? id = null)
        {
            BusyIndicatorUtils.SetBusyIndicator(true);

            Categories.AddRange(await productsCategoriesRepo.GetProductCategories());
            Providers.AddRange(await providersRepo.GetProviders());

            if (id != null)
            {
                if (AllowEdit)
                    IsEditMode = true;
                await LoadProduct((int)id);
            }

            if (AllowEdit)
                CanSave = true;
            BusyIndicatorUtils.SetBusyIndicator(false);
        }

        private async Task LoadProduct(int id)
        {
            Product product = await productsRepo.GetProduct(id);
            Id = product.Id;
            Name = product.Name;
            Unit = product.Unit;
            Description = product.Description;
            Price = product.Price;
            Stock = product.Stock;
            ProductCategory = product.Category;
            SelectedProvider = product.Provider;
        }

        private async Task SaveProduct()
        {
            ValidateAllProperties();
            if (HasErrors)
                return;

            BusyIndicatorUtils.SetBusyIndicator(true);

            var product = new Product()
            {
                Id = Id,
                Name = Name,
                Unit = Unit,
                Description = Description,
                Price = Price,
                Category = ProductCategory,
                Provider = SelectedProvider
            };

            if (IsEditMode)
            {
                await productsRepo.UpdateProduct(Id, product);
            }
            else
            {
                await productsRepo.AddProduct(product);
            }
            BusyIndicatorUtils.SetBusyIndicator(false);
            // Navigate back to list screen
            WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Products);
        }

        private void DeleteProduct()
        {
            ConfirmDialog dialog = new ConfirmDialog(
                 "Delete product",
                 "Do you want to delete this product?\n" +
                 "This action cannot be undone!");
            dialog.Closed += async (sender, eventArgs) =>
            {
                if (dialog.IsConfirmed)
                {
                    BusyIndicatorUtils.SetBusyIndicator(true);
                    await productsRepo.DeleteProduct(Id);
                    BusyIndicatorUtils.SetBusyIndicator(false);
                    WorkspaceNavUtils.NavigateTo(WorkspaceViewName.Products);
                }
            };
            dialog.Show();
        }
    }
}