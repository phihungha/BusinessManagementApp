using BusinessManagementApp.ViewModels.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Security;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels
{
    public class LoginVM : ObservableObject
    {
        private string userName = "";
        public string UserName
        {
            get => userName; 
            set => SetProperty(ref userName, value);
        }

        private string password = "";
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public ICommand Login { get; }

        public LoginVM() 
        { 
            Login = new RelayCommand(CheckLogin);
        }

        private void CheckLogin()
        {
            if (UserName == "admin" && Password == "1234") 
            {
                MainWindowNavUtils.NavigateTo(MainWindowViewName.Workspace);
            }
        }
    }
}
