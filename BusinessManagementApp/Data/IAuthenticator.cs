using System;

namespace BusinessManagementApp.Data;

public interface IAuthenticator
{
    IObservable<bool> Authenticate(string username, string password);
}