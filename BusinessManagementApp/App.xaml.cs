using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BusinessManagementApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BusinessManagementApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public new static App Current => (App)Application.Current;

        private readonly IHost _host;

        public IServiceProvider ServiceProvider => _host.Services;

        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        private IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return CreateHostBuilder(args)
                .Application(this)
                .AddLogging()
                .AddStores()
                .AddViewModels();
        }
    }
}
