using BusinessManagementApp.Data.Model;
using BusinessManagementApp.Views;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class ProviderRepo
    {
        public IObservable<object> DeleteProvider(int id)
        {
            throw new NotImplementedException();
        }

        public IObservable<Provider> GetProvider(int id)
        {
            var provider = new Provider()
            {
                Id = 1,
                Name = "alibaba",
                Description = "den tu chau Au"
            };
            return Observable.FromAsync(() => Task.FromResult(provider));
        }

        public IObservable<List<Provider>> GetProviders()
        {
            var providers = new List<Provider>()
            {
                new Provider()
                {
                    Id = 1,
                    Name = "alibaba",
                    Description = "den tu chau au"
                },
                new Provider()
                {
                    Id = 2,
                    Name = "shoppee",
                    Description = "den tu chau a"
                },
            };
            return Observable.FromAsync(() => Task.FromResult(providers));
        }

        public IObservable<Provider> AddProvider(Provider provider)
        {
            throw new NotImplementedException();
        }

        public IObservable<Provider> UpdateProvider(int providerId, Provider provider)
        {
            throw new NotImplementedException();
        }
    }
}