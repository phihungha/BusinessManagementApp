using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data
{
    public class ProvidersRepo
    {
        private IProvidersApi api;

        public ProvidersRepo(IProvidersApi api)
        {
            this.api = api;
        }

        public IObservable<object> DeleteProvider(int id)
        {
            return api.DeleteProvider(id);
        }

        public IObservable<Provider> GetProvider(int id)
        {
            return api.GetProvider(id);
        }

        public IObservable<List<Provider>> GetProviders()
        {
            return api.GetProviders();
        }

        public IObservable<Provider> AddProvider(Provider provider)
        {
            return api.SaveProvider(provider);
        }

        public IObservable<Provider> UpdateProvider(int providerId, Provider provider)
        {
            return api.UpdateProvider(providerId, provider);
        }
    }
}