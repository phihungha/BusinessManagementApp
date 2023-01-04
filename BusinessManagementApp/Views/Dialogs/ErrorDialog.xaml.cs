using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;

namespace BusinessManagementApp.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for ErrorDialog.xaml
    /// </summary>
    public partial class ErrorDialog : Window
    {
        public string MessageTitle { get; }
        public string MessageContent { get; }

        public ICommand Ok { get; }

        public ErrorDialog(string messageTitle, string messageContent)
        {
            InitializeComponent();
            MessageTitle = messageTitle;
            MessageContent = messageContent;
            Ok = new RelayCommand(() => Close());

            DataContext = this;
        }
    }
}