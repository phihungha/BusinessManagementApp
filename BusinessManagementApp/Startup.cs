using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Services;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels;
using BusinessManagementApp.ViewModels.EditVMs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Refit;
using System;
using System.Windows;

namespace BusinessManagementApp
{
    public static class Startup
    {
        private static IHttpClientBuilder ConfigHttpClientBuilder(this IHttpClientBuilder builder, bool auth = false, bool logging = false)
        {
            string BASE_URL = "";
            builder = builder.ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(BASE_URL);
            });

            if (logging)
            {
                builder.AddHttpMessageHandler(() => new HttpLoggingHandler());
            }
            return builder;
        }

        public static IHostBuilder Application(this IHostBuilder host, Application application)
        {
            host.ConfigureServices((context, service) =>
            {
                service.AddSingleton(application);
            });
            return host;
        }

        public static IHostBuilder AddLogging(this IHostBuilder host)
        {
            host.ConfigureServices((context, service) =>
            {
                service.AddLogging();
            });
            return host;
        }

        public static IHostBuilder AddStores(this IHostBuilder host)
        {
            host.ConfigureServices((context, service) =>
            {
                service.AddSingleton<LoginSession>();

                RefitSettings settings = new RefitSettings(new NewtonsoftJsonContentSerializer(new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                }));
                service.AddTransient<HttpAuthHandler>();
                service.AddRefitClient<IAuthRemote>(settings).ConfigHttpClientBuilder(false, true);

                service.AddSingleton<IAuthenticator, Authenticator>();
                service.AddSingleton<SchedulerProvider>();

                var localRemote = new LocalModelRemote();
                service.AddSingleton<IOrderApi>(localRemote);
                service.AddSingleton<IContractApi>(localRemote);
                service.AddSingleton<ICustomerApi>(localRemote);
                service.AddSingleton<IDepartmentApi>(localRemote);
                service.AddSingleton<IEmployeeApi>(localRemote);
                service.AddSingleton<IPositionApi>(localRemote);
                service.AddSingleton<IProductApi>(localRemote);
                service.AddSingleton<IRecordApi>(localRemote);
                service.AddSingleton<IVoucherApi>(localRemote);
            });
            return host;
        }

        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices((context, service) =>
            {
                service.AddTransient<MainWindowVM>();
                service.AddTransient<LoginVM>();
                service.AddTransient<WorkspaceVM>();
                service.AddTransient<OverviewVM>();
                service.AddTransient<OrdersVM>();
                service.AddTransient<EmployeeInfoVM>();
                service.AddTransient<EmployeeInfoDetailsVM>();
            });
            return host;
        }
    }
}