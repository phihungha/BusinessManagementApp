using System;

namespace BusinessManagementApp.Services;

public interface IAuthenticator
{
    IObservable<bool> Authenticate(string username, string password);
}