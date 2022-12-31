using BusinessManagementApp.Data.Model;
using Refit;
using System;

namespace BusinessManagementApp.Data.Api
{
    // TODO
    public interface IConfigApi
    {
        [Get("/")]
        public IObservable<Config> GetConfig();

        [Patch("/")]
        public IObservable<Config> SaveConfig([Body] Config config);
    }
}