using BusinessManagementApp.Data.Model;
using BusinessManagementApp.ViewModels.DetailsVMs;
using System.Linq;
using System.Windows.Controls;

namespace BusinessManagementApp.Views.DetailsViews
{
    /// <summary>
    /// Interaction logic for VoucherTypeDetails.xaml
    /// </summary>
    public partial class VoucherTypeDetails : UserControl
    {
        public VoucherTypeDetails()
        {
            InitializeComponent();
        }
        private void ProductsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext == null)
                return;

            var viewModel = (SelectProductsVM)DataContext;
            viewModel.SelectedProducts = ProductsListBox.SelectedItems.OfType<Product>().ToList();
        }
    }

}
