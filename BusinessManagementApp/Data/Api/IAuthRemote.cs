using BusinessManagementApp.Data.Model.Auth;
using Refit;
using System;

namespace BusinessManagementApp.Data.Api
{
    public interface IAuthRemote
    {
        [Post("/login")]
        IObservable<LoginResponse> Login([Body(BodySerializationMethod.UrlEncoded)] LoginRequest request);
    }
}
