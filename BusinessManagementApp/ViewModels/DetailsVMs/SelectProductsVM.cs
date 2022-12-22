using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.Utils;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels.DetailsVMs
{
    public enum SelectProductsSearchBy
    {
        [Description("ID")]
        Id,

        [Description("Name")]
        Name,

        [Description("Description")]
        Description
    }

    public class SelectProductsVM : ViewModelBase
    {
        #region Dependencies

        private ProductsRepo productsRepo;

        #endregion Dependencies

        private ObservableCollection<Product> products { get; } = new();

        public ICollectionView ProductsView { get; }

        public string SearchText { get; set; } = string.Empty;

        public SelectProductsSearchBy SearchBy { get; set; } = SelectProductsSearchBy.Name;

        private List<Product> selectedProducts = new();

        public List<Product> SelectedProducts
        {
            get => selectedProducts;
            set
            {
                selectedProducts = value;
                CalculateTotalPrice();
            }
        }

        private string title = "";

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private decimal totalPrice = 0;

        public decimal TotalPrice
        {
            get => totalPrice;
            set => SetProperty(ref totalPrice, value);
        }

        #region Commands for buttons

        public ICommand Search { get; }
        public ICommand Select { get; private set; }
        public ICommand Cancel { get; private set; }

        #endregion Commands for buttons

        public SelectProductsVM(ProductsRepo productsRepo)
        {
            this.productsRepo = productsRepo;

            var collectionViewSource = new CollectionViewSource() { Source = products };
            ProductsView = collectionViewSource.View;
            ProductsView.Filter = FilterList;

            Select = new RelayCommand(() => WorkspaceNavUtils.NavigateBackWithExtra(SelectedProducts));
            Search = new RelayCommand(() => ProductsView.Refresh());
            Cancel = new RelayCommand(() => WorkspaceNavUtils.NavigateBack());
        }

        private bool FilterList(object item)
        {
            var product = (Product)item;

            if (SearchText == null)
            {
                return true;
            }

            switch (SearchBy)
            {
                case SelectProductsSearchBy.Name:
                    return product.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case SelectProductsSearchBy.Id:
                    return product.Id.ToString() == SearchText;

                case SelectProductsSearchBy.Description:
                    return product.Description.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                default:
                    return false;
            }
        }

        public override async void LoadData(object? input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            Title = (string)input;
            products.AddRange(await productsRepo.GetAvailableProducts());
        }

        private void CalculateTotalPrice()
        {
            TotalPrice = SelectedProducts.Sum(i => i.Price);
        }

        private void ReturnSelectedProducts()
        {
            WorkspaceNavUtils.NavigateBackWithExtra(SelectedProducts);
        }
    }
}