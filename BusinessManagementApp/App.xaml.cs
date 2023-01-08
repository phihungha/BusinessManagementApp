using BusinessManagementApp.ViewModels.BusyIndicator;
using BusinessManagementApp.Views.Dialogs;
using Microsoft.Extensions.Hosting;
using Refit;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace BusinessManagementApp
{
    public partial class App : Application
    {
        public new static App Current => (App)Application.Current;

        private readonly IHost host;

        public IServiceProvider ServiceProvider => host.Services;

        public static string? CurrencySymbol { get; } = NumberFormatInfo.CurrentInfo.CurrencySymbol;

        public App()
        {
            SetupCulture();
            host = CreateHostBuilder().Build();
            Dispatcher.UnhandledException += Dispatcher_UnhandledException;
        }

        private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var apiErr = e.Exception as ApiException;

            if (apiErr != null && MainWindow.IsLoaded)
            {
                e.Handled = true;
                BusyIndicatorUtils.SetBusyIndicator(false);
                new ErrorDialog("API Error", apiErr.Message).Show();
            }
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
                .AddApiClient()
                .AddRepositories()
                .AddViewModels();
        }
    }
}