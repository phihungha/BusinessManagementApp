using System;
using BusinessManagementApp.Data.Model.Auth;
using Refit;

namespace BusinessManagementApp.Data.Remote
{
    public interface IAuthRemote
    {

        [Post("/login")]
        IObservable<LoginResponse> login([Body(BodySerializationMethod.UrlEncoded)] LoginRequest request);

    }
}
