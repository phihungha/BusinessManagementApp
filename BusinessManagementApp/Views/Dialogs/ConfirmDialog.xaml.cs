using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;

namespace BusinessManagementApp.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for ConfirmDialog.xaml
    /// </summary>
    public partial class ConfirmDialog : Window
    {
        public string MessageTitle { get; }
        public string MessageContent { get; }
        public bool IsConfirmed { get; private set; } = false;

        public ICommand Confirm { get; }
        public ICommand Cancel { get; }

        public ConfirmDialog(string messageTitle, string messageContent)
        {
            InitializeComponent();

            MessageTitle = messageTitle;
            MessageContent = messageContent;

            Confirm = new RelayCommand(() =>
            {
                IsConfirmed = true;
                Close();
            });
            Cancel = new RelayCommand(() => Close());

            DataContext = this;
        }
    }
}