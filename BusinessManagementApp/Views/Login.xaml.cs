using BusinessManagementApp.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace BusinessManagementApp.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                ((LoginVM)DataContext).Password = ((PasswordBox)sender).Password;
            }
        }
    }
}
