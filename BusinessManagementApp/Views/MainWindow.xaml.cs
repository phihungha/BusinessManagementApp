using BusinessManagementApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace BusinessManagementApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.Current.ServiceProvider.GetRequiredService<MainWindowVM>();
        }
    }
}