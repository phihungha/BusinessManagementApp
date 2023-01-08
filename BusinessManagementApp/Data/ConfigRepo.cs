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

        public async Task<Config> SaveConfig(Config config)
        {
            Config = await api.SaveConfig(config);
            return Config;
        }
    }
}