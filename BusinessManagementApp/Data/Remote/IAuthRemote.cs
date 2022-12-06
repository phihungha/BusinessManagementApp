using System;
using BusinessManagementApp.Data.Model.Auth;
using Refit;

namespace BusinessManagementApp.Data.Remote
{
    public interface IAuthRemote
    {
        [Post("/login")]
        IObservable<LoginResponse> Login([Body(BodySerializationMethod.UrlEncoded)] LoginRequest request);
    }
}
