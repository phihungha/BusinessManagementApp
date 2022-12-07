using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace BusinessManagementApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public new static App Current => (App)Application.Current;

        private readonly IHost host;

        public IServiceProvider ServiceProvider => host.Services;

        public App()
        {
            host = CreateHostBuilder().Build();
        }

        private IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .Application(this)
                .AddLogging()
                .AddApis()
                .AddRepositories()
                .AddViewModels();
        }
    }
}