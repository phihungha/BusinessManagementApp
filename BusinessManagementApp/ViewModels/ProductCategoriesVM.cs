﻿using BusinessManagementApp.Data;
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
    public enum ProductCategoryInfoSearchBy
    {
        [Description("ID")]
        Id,

        [Description("Name")]
        Name,
    }

    public class ProductCategoriesVM : ViewModelBase
    {
        private ProductCategoriesRepo productCategoriesRepo;

        private ObservableCollection<ProductCategory> productCategories { get; } = new();

        public ICollectionView ProductCategoriesView { get; }

        public string SearchText { get; set; } = string.Empty;

        public ProductCategoryInfoSearchBy SearchBy { get; set; } = ProductCategoryInfoSearchBy.Name;

        public ICommand AddProductCategory { get; }
        public ICommand Search { get; }
        public ICommand Edit { get; }

        public bool AllowAdd { get; } = false;

        // Declare dependencies (e.g repositories) to use as constructor parameters
        // Go into Startup.cs to add new depencencies if needed
        public ProductCategoriesVM(ProductCategoriesRepo productCategoriesRepo, SessionsRepo sessionsRepo)
        {
            if (sessionsRepo.CurrentPosition.CanManageConfig)
            {
                AllowAdd = true;
            }

            this.productCategoriesRepo = productCategoriesRepo;

            // DataGrid accesses the ObservableCollection of model objects
            // indirectly via a ICollectionView to support filtering.
            var collectionViewSource = new CollectionViewSource() { Source = productCategories };
            ProductCategoriesView = collectionViewSource.View;
            ProductCategoriesView.Filter = FilterList;

            AddProductCategory = new RelayCommand(() => WorkspaceNavUtils.NavigateTo(WorkspaceViewName.ProductCategoryDetails));
            Search = new RelayCommand(() => ProductCategoriesView.Refresh());
            Edit = new RelayCommand<int?>(id => OpenDetailsView(id));

            LoadData();
        }

        private bool FilterList(object item)
        {
            var productCategory = (ProductCategory)item;
            if (SearchText == null)
                return true;
            switch (SearchBy)
            {
                case ProductCategoryInfoSearchBy.Name:
                    return productCategory.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

                case ProductCategoryInfoSearchBy.Id:
                    return productCategory.Id.ToString().Contains(SearchText, StringComparison.InvariantCultureIgnoreCase);

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

            WorkspaceNavUtils.NavigateToWithExtra(WorkspaceViewName.ProductCategoryDetails, id);
        }

        private async void LoadData()
        {
            BusyIndicatorUtils.SetBusyIndicator(true);
            productCategories.AddRange(await productCategoriesRepo.GetProductCategories());
            BusyIndicatorUtils.SetBusyIndicator(false);
        }
    }
}