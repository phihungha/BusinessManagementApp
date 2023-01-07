using BusinessManagementApp.Data;
using BusinessManagementApp.ViewModels.Navigation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessManagementApp.ViewModels
{
    public class LoginVM : ObservableObject
    {
        private readonly SessionsRepo sessionsRepo;
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

        private bool displayLoginBtn = true;

        public bool DisplayLoginBtn
        {
            get => displayLoginBtn;
            set => SetProperty(ref displayLoginBtn, value);
        }

        private bool isLoggingIn = false;

        public bool IsLoggingIn
        {
            get => isLoggingIn;
            set
            {
                SetProperty(ref isLoggingIn, value);
                DisplayLoginBtn = !value;
            }
        }

        public ICommand Login { get; }

        public LoginVM(SessionsRepo sessionsRepo, ConfigRepo configRepo)
        {
            this.sessionsRepo = sessionsRepo;
            this.configRepo = configRepo;
            Login = new AsyncRelayCommand(CheckLogin);
        }

        private async Task CheckLogin()
        {
            IsLoggingIn = true;
            if (await sessionsRepo.Authenticate(UserName, Password))
            {
                MainWindowNavUtils.NavigateTo(MainWindowViewName.Workspace);
                await configRepo.LoadConfig();
            }
            else
            {
                InvalidLogin = true;
            }
            IsLoggingIn = false;
        }
    }
}