using System;
using System.Reactive.Linq;
using BusinessManagementApp.Data.Model.Auth;
using BusinessManagementApp.Data.Remote;
using BusinessManagementApp.Services;
using Microsoft.Extensions.Logging;

namespace BusinessManagementApp.Data;

public class Authenticator : IAuthenticator
{

    private readonly ILogger logger;
    private readonly IAuthRemote remote;
    private readonly LoginSession session;

    public Authenticator(ILogger<Authenticator> logger, IAuthRemote remote, LoginSession session)
    {
        this.logger = logger;
        this.remote = remote;
        this.session = session;
    }

    public IObservable<bool> Authenticate(string username, string password)
    {
        return remote.Login(new LoginRequest()
        {
            Username = username,
            Password = password
        }).Select(response =>
        {
            if (response.IsAuthenticated)
            {
                session.AccessToken = response.AccessToken;
                session.RefreshToken = response.RefreshToken;
                return true;
            }

            return false;
        });
    }
}