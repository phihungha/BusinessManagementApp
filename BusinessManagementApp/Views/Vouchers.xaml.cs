using BusinessManagementApp.Data.Model;
using BusinessManagementApp.ViewModels;
using System.Linq;
using System.Windows.Controls;

namespace BusinessManagementApp.Views
{
    /// <summary>
    /// Interaction logic for Vouchers.xaml
    /// </summary>
    public partial class Vouchers : UserControl
    {
        public Vouchers()
        {
            InitializeComponent();
        }

        private void VouchersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext == null)
                return;
            var viewModel = (VouchersVM)DataContext;
            viewModel.SelectedVouchers = VouchersDataGrid.SelectedItems.OfType<Voucher>().ToList();
        }
    }
}
