﻿using BusinessManagementApp.Data.Model;
using BusinessManagementApp.ViewModels.DetailsVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        }

        // ListBox doesn't support binding with SelectedItems so an event handler is needed
        private void ProductsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext == null)
                return;

            var viewModel = (SelectProductsVM)DataContext;
            viewModel.SelectedProducts = ProductsListBox.SelectedItems.OfType<Product>().ToList();
        }
    }
}