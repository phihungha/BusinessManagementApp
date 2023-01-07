using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BusinessManagementApp.Data
{
    public class ConfigRepo
    {
        private IConfigApi api;

        public Config Config { get; private set; } = new();

        public ConfigRepo(IConfigApi api)
        {
            this.api = api;
        }

        public async Task<Config> LoadConfig()
        {
            Config = await api.GetConfig();
            return Config;
        }

        public IObservable<Config> SaveConfig(Config config)
        {
            return api.SaveConfig(config);
        }
    }
}