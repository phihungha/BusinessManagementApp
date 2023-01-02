using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model.Auth;
using BusinessManagementApp.Services;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class SessionsRepo
    {
        private readonly IAuthApi api;
        private readonly SessionAuthData session;

        public SessionsRepo(IAuthApi api, SessionAuthData session)
        {
            this.api = api;
            this.session = session;
        }

        public async Task<bool> Authenticate(string userName, string password)
        {
            var request = new LoginRequest()
            {
                UserName = userName,
                Password = password
            };

            LoginResponse response = await api.Login(request);

            if (response.IsAuthenticated)
            {
                session.AccessToken = response.AccessToken;
                session.RefreshToken = response.RefreshToken;
                return true;
            }

            return false;
        }
    }
}