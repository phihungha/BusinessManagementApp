﻿using BusinessManagementApp.Data.Model;
using BusinessManagementApp.ViewModels.DetailsVMs;
using System.Linq;
using System.Windows.Controls;

namespace BusinessManagementApp.Views.DetailsViews
{
    /// <summary>
    /// Interaction logic for SelectProducts.xaml
    /// </summary>
    public partial class SelectProducts : UserControl
    {
        public SelectProducts()
        {
            InitializeComponent();
            Loaded += SelectProducts_Loaded;
        }

        private void SelectProducts_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ProductsListBox.Focus();
        }

        // ListBox doesn't support binding with SelectedItems so an event handler is needed
        private void ProductsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext == null)
                return;

            var viewModel = (SelectProductsVM)DataContext;
            viewModel.SelectedProductVMs = ProductsListBox.SelectedItems.OfType<SelectProductsVM.ProductVM>().ToList();
        }
    }
}
