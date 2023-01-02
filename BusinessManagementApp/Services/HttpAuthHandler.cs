using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessManagementApp.Services
{
    /// <summary>
    /// Adds authorization headers to a HTTP request.
    /// </summary>
    public class HttpAuthHandler : DelegatingHandler
    {
        private readonly SessionAuthData loginSession;

        public HttpAuthHandler(SessionAuthData loginSession)
        {
            this.loginSession = loginSession;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("x-access-token", loginSession.AccessToken);
            request.Headers.Add("x-refresh-token", loginSession.RefreshToken);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}