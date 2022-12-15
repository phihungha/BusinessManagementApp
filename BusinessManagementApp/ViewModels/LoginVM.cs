using System.Reactive.Linq;
using System.Threading.Tasks;
using BusinessManagementApp.ViewModels.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model.Auth;

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

        private bool invalidLogin = false;

        public bool InvalidLogin
        {
            get => invalidLogin;
            set => SetProperty(ref invalidLogin, value);
        }

        public ICommand Login { get; }

        private readonly IAuthRemote authRemote;

        public LoginVM(IAuthRemote authRemote)
        {
            this.authRemote = authRemote;
            Login = new AsyncRelayCommand(CheckLogin);
        }

        private async Task CheckLogin()
        {
            //Default user: admin - 113
            var response = await this.authRemote.Login(new LoginRequest()
            {
                Username = UserName,
                Password = Password,
            });
            if (response.IsAuthenticated)
            {
                MainWindowNavUtils.NavigateTo(MainWindowViewName.Workspace);
            }
            else
            {
                InvalidLogin = true;
            }
        }
    }
}