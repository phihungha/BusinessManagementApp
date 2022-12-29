using BusinessManagementApp.ViewModels.DetailsVMs;
using System.Linq;
using System.Windows.Controls;

namespace BusinessManagementApp.Views.DetailsViews
{
    /// <summary>
    /// Interaction logic for SelectProductOrderItems.xaml
    /// </summary>
    public partial class SelectProductOrderItems : UserControl
    {
        public SelectProductOrderItems()
        {
            InitializeComponent();
        }

        // ListBox doesn't support binding with SelectedItems so an event handler is needed
        private void ProductsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext == null)
                return;

            var viewModel = (SelectProductOrderItemsVM)DataContext;
            viewModel.SelectedProductVMs = ProductsListBox.SelectedItems.OfType<SelectProductOrderItemsVM.ProductVM>().ToList();
        }
    }
}