using System;
using System.Windows;
using BusinessManagementApp.Repositories;
using BusinessManagementApp.ViewModels;
using BusinessManagementApp.ViewModels.EditVMs;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessManagementApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        public App()
        {
            Services = ConfigureServices();
        }

        /// <summary>
        /// Setup classes used for dependency injection.
        /// </summary>
        /// <returns>DI service provider</returns>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<EmployeesRepository>();

            services.AddTransient<MainWindowVM>();
            services.AddTransient<LoginVM>();
            services.AddTransient<WorkspaceVM>();
            services.AddTransient<OverviewVM>();
            services.AddTransient<OrdersVM>();
            services.AddTransient<EmployeeInfoVM>();
            services.AddTransient<EmployeeInfoDetailsVM>();

            return services.BuildServiceProvider();
        }
    }
}
