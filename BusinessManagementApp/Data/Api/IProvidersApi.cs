using BusinessManagementApp.Data.Model;
using Refit;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Api
{
    public interface IProvidersApi
    {
        [Get("/")]
        IObservable<List<Provider>> GetProviders();

        [Get("/{id}")]
        IObservable<Provider> GetProvider(int id);

        [Post("/")]
        IObservable<Provider> SaveProvider([Body] Provider provider);

        [Patch("/{id}")]
        IObservable<Provider> UpdateProvider(int id, [Body] Provider provider);

        [Delete("/{id}")]
        IObservable<object> DeleteProvider(int id);
    }
}