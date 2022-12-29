using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Model;
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
    public enum SelectProductOrderItemsSearchBy
    {
        [Description("ID")]
        Id,

        [Description("Name")]
        Name,

        [Description("Description")]
        Description
    }

    public class SelectProductOrderItemsVM : ViewModelBase
    {
        public struct Param
        {
            public string Title { get; set; }
            public IEnumerable<OrderItem>? OrderItems { get; set; }
        }

        public class ProductVM : ViewModelBase
        {
            private SelectProductOrderItemsVM parentVM;

            public Product Product { get; }

            private bool isSelected;

            public bool IsSelected
            {
                get => isSelected;
                set
                {
                    SetProperty(ref isSelected, value);
                    if (value)
                    {
                        Quantity = 1;
                    }
                    else
                    {
                        Quantity = 0;
                    }
                }
            }

            private int quantity;

            public int Quantity
            {
                get => quantity;
                set
                {
                    SetProperty(ref quantity, value);
                    OrderedPrice = value * Product.Price;
                }
            }

            private decimal orderedPrice;

            public decimal OrderedPrice
            {
                get => orderedPrice;
                set
                {
                    parentVM.TotalOrderedPrice -= orderedPrice;
                    SetProperty(ref orderedPrice, value);
                    parentVM.TotalOrderedPrice += value;
                }
            }

            public ProductVM(Product product, bool isSelected, int quantity, SelectProductOrderItemsVM parentVM)
            {
                this.parentVM = parentVM;
                Product = product;
                IsSelected = isSelected;
                Quantity = quantity;
            }
        }

        #region Dependencies

        private ProductsRepo productsRepo;

        #endregion Dependencies

        private ObservableCollection<ProductVM> productVMs { get; } = new();

        public ICollectionView ProductsView { get; }

        private List<ProductVM> selectedProductVMs = new();

        public List<ProductVM> SelectedProductVMs
        {
            get => selectedProductVMs;
            set => selectedProductVMs = value;
        }

        #region Searching

        public string SearchText { get; set; } = string.Empty;

        public SelectProductsSearchBy SearchBy { get; set; } = SelectProductsSearchBy.Name;

        #endregion Searching

        #region Display

        private string title = "";

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private decimal totalPrice = 0;

        public decimal TotalOrderedPrice
        {
            get => totalPrice;
            set => SetProperty(ref totalPrice, value);
        }

        #endregion Display

        #region Commands for buttons

        public ICommand Search { get; }
        public ICommand Select { get; private set; }
        public ICommand DeselectAll { get; private set; }
        public ICommand Cancel { get; private set; }

        #endregion Commands for buttons

        public SelectProductOrderItemsVM(ProductsRepo productsRepo)
        {
            this.productsRepo = productsRepo;

            var collectionViewSource = new CollectionViewSource() { Source = productVMs };
            ProductsView = collectionViewSource.View;
            ProductsView.Filter = FilterList;

            Select = new RelayCommand(ReturnOrderItems);
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
            Title = param.Title;
            IEnumerable<OrderItem>? orderItems = param.OrderItems;

            List<Product> products = await productsRepo.GetAvailableProducts();
            foreach (var product in products)
            {
                OrderItem? orderItem = orderItems?.First(i => i.ProductId == product.Id);

                ProductVM productVM;
                if (orderItem != null)
                {
                    productVM = new ProductVM(product, true, orderItem.Quantity, this);
                    selectedProductVMs.Add(productVM);
                }
                else
                {
                    productVM = new ProductVM(product, false, 0, this);
                }

                productVMs.Add(productVM);
            }
        }

        private void ExecuteDeselectAll()
        {
            SelectedProductVMs.ForEach(i => i.IsSelected = false);
            SelectedProductVMs.Clear();
        }

        private void ReturnOrderItems()
        {
            List<OrderItem> items = selectedProductVMs
                .Select(i => new OrderItem()
                {
                    ProductId = i.Product.Id,
                    Quantity = i.Quantity,
                    UnitPrice = i.Product.Price
                }).ToList();
            WorkspaceNavUtils.NavigateBackWithExtra(items);
        }
    }
}