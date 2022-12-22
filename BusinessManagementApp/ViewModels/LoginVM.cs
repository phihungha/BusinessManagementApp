using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model.Auth;
using BusinessManagementApp.ViewModels.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels
{
    public class LoginVM : ObservableObject
    {
        private readonly IAuthRemote authRemote;
        private readonly ConfigRepo configRepo;

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

        public LoginVM(IAuthRemote authRemote, ConfigRepo configRepo)
        {
            this.authRemote = authRemote;
            this.configRepo = configRepo;
            Login = new AsyncRelayCommand(CheckLogin);
        }

        private async Task CheckLogin()
        {
            //Default user: admin - 113
            var response = await authRemote.Login(new LoginRequest()
            {
                Username = UserName,
                Password = Password,
            });

            if (response.IsAuthenticated)
            {
                MainWindowNavUtils.NavigateTo(MainWindowViewName.Workspace);
                configRepo.LoadConfig();
            }
            else
            {
                InvalidLogin = true;
            }
        }
    }
}