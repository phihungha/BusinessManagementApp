using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

namespace BusinessManagementApp
{
    public partial class App : Application
    {
        public new static App Current => (App)Application.Current;

        private readonly IHost host;

        public IServiceProvider ServiceProvider => host.Services;

        public App()
        {
            SetupCulture();
            host = CreateHostBuilder().Build();
        }

        private void SetupCulture()
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement), 
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
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