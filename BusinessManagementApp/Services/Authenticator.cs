using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model.Auth;
using BusinessManagementApp.Services;
using System;
using System.Reactive.Linq;

namespace BusinessManagementApp.Services;

public class Authenticator : IAuthenticator
{
    private readonly IAuthRemote remote;
    private readonly LoginSession session;

    public Authenticator(IAuthRemote remote, LoginSession session)
    {
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