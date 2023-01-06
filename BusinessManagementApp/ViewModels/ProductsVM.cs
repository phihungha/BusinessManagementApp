using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels.BusyIndicator;
using BusinessManagementApp.ViewModels.Navigation;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels
{
    public enum ProductInfoSearchBy
    {
        [Description("ID")]
        Id,

        [Description("Name")]
        Name,

        [Description("Price")]
        Price,

        [Description("Category")]
        Category,

        [Description("Stock")]
        Stock
    }

    public class ProductsVM : ViewModelBase
    {
        private ProductsRepo productsRepo;

        private ObservableCollection<Product> products { get; } = new();

        public ICollectionView ProductsView { get; }

        public string SearchText { get; set; } = string.Empty;

        public ProductInfoSearchBy SearchBy { get; set; } = ProductInfoSearchBy.Name;

        public ICommand AddProduct { get; }
        public ICommand Search { get; }
        public ICommand Edit { get; }

        public bool AllowAdd { get; } = false;

        public ProductsVM(ProductsRepo productsRepo, SessionsRepo sessionsRepo)
        {
            if (sessionsRepo.CurrentPosition.CanManageSales)
            {
                AllowAdd = true;
            }

            this.productsRepo = productsRepo;
            var collectionViewSource = new CollectionViewSource() { Source = products };
            ProductsView = collectionViewSource.View;
            ProductsView.Filter = FilterList;

            AddProduct = new RelayCommand(() => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.ProductDetails));
            Search = new RelayCommand(() => ProductsView.Refresh());
            Edit = new RelayCommand<int?>(id => OpenDetailsView(id));

            LoadData();
        }

        private bool FilterList(object item)
        {
            var product = (Product)item;

            switch (SearchBy)
            {
                case ProductInfoSearchBy.Name:
                    return product.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case ProductInfoSearchBy.Id:
                    return product.Id.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case ProductInfoSearchBy.Price:
                    return product.Price.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case ProductInfoSearchBy.Category:
                    return product.Category.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case ProductInfoSearchBy.Stock:
                    return product.Stock.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                default:
                    return false;
            }
        }

        private void OpenDetailsView(int? id)
        {
            if (id == null)
            {
                throw new ArgumentException("ID is null");
            }

            // Navigate to details screen
            WorkspaceNavUtils.NavigateToWithExtra(WorkspaceViewName.ProductDetails, id);
        }

        private async void LoadData()
        {
            BusyIndicatorUtils.SetBusyIndicator(true);
            products.AddRange(await productsRepo.GetProducts());
            BusyIndicatorUtils.SetBusyIndicator(false);
        }
    }
}