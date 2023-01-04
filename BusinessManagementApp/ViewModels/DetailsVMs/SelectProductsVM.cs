using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.ViewModels.Navigation;
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
        public struct Param
        {
            public string Title { get; set; }
            public List<Product>? SelectedProducts { get; set; }
        }

        public class ProductVM : ViewModelBase
        {
            public Product Product { get; }

            private bool isSelected;

            public bool IsSelected
            {
                get => isSelected;
                set => SetProperty(ref isSelected, value);
            }

            public ProductVM(Product product, bool isSelected)
            {
                Product = product;
                IsSelected = isSelected;
            }
        }

        #region Dependencies

        private ProductsRepo productsRepo;

        #endregion Dependencies

        private ObservableCollection<ProductVM> productVMs { get; } = new();

        public ICollectionView ProductsView { get; }

        public string SearchText { get; set; } = string.Empty;

        public SelectProductsSearchBy SearchBy { get; set; } = SelectProductsSearchBy.Name;

        private List<ProductVM> selectedProductVMs = new();

        public List<ProductVM> SelectedProductVMs
        {
            get => selectedProductVMs;
            set => selectedProductVMs = value;
        }

        private string title = "";

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        #region Commands for buttons

        public ICommand Search { get; }
        public ICommand Select { get; private set; }
        public ICommand DeselectAll { get; private set; }
        public ICommand Cancel { get; private set; }

        #endregion Commands for buttons

        public SelectProductsVM(ProductsRepo productsRepo)
        {
            this.productsRepo = productsRepo;

            var collectionViewSource = new CollectionViewSource() { Source = productVMs };
            ProductsView = collectionViewSource.View;
            ProductsView.Filter = FilterList;

            Select = new RelayCommand(ReturnSelectedProducts);
            DeselectAll = new RelayCommand(ExecuteDeselectAll);
            Search = new RelayCommand(() => ProductsView.Refresh());
            Cancel = new RelayCommand(() => WorkspaceNavUtils.NavigateBack());
        }

        private bool FilterList(object item)
        {
            var product = ((ProductVM)item).Product;

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

            var param = (Param)input;
            List<Product>? selectedProducts = param.SelectedProducts;

            List<Product> products = await productsRepo.GetProducts();
            foreach (var product in products)
            {
                Product? orderItem = selectedProducts?.Find(i => i.Id == product.Id);

                ProductVM productVM;
                if (orderItem != null)
                {
                    productVM = new ProductVM(product, true);
                    selectedProductVMs.Add(productVM);
                }
                else
                {
                    productVM = new ProductVM(product, false);
                }

                productVMs.Add(productVM);
            }
        }

        private void ExecuteDeselectAll()
        {
            SelectedProductVMs.ForEach(i => i.IsSelected = false);
            SelectedProductVMs.Clear();
        }

        private void ReturnSelectedProducts()
        {
            List<Product> items = selectedProductVMs
                .Select(i => i.Product)
                .ToList();
            WorkspaceNavUtils.NavigateBackWithExtra(items);
        }
    }
}