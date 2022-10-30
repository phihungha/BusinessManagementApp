using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessManagementApp.Services
{
    public class HttpAuthHandler : DelegatingHandler
    {
        
        private readonly LoginSession _loginSession;
        
        public HttpAuthHandler(LoginSession loginSession)
        {
            _loginSession = loginSession;
        }
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("x-access-token", _loginSession.AccessToken);
            request.Headers.Add("x-refresh-token", _loginSession.RefreshToken);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

    }
}
