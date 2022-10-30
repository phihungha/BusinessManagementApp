using System;
using System.Reactive.Linq;
using BusinessManagementApp.Data.Model.Auth;
using BusinessManagementApp.Data.Remote;
using BusinessManagementApp.Services;
using Microsoft.Extensions.Logging;

namespace BusinessManagementApp.Data;

public class Authenticator : IAuthenticator
{

    private readonly ILogger _logger;
    private readonly IAuthRemote _remote;
    private readonly LoginSession _session;

    public Authenticator(ILogger<Authenticator> logger, IAuthRemote remote, LoginSession session)
    {
        _logger = logger;
        _remote = remote;
        _session = session;
    }

    public IObservable<bool> Authenticate(string username, string password)
    {
        return _remote.login(new LoginRequest()
        {
            Username = username,
            Password = password
        }).Select(response =>
        {
            if (!response.IsAuthenticated) return response.IsAuthenticated;

            _session.AccessToken = response.AccessToken;
            _session.RefreshToken = response.RefreshToken;

            return response.IsAuthenticated;
        });
    }
}