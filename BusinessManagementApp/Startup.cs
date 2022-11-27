using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Remote;
using BusinessManagementApp.Services;
using BusinessManagementApp.Utils;
using Newtonsoft.Json;
using Refit;
using BusinessManagementApp.ViewModels;
using BusinessManagementApp.ViewModels.EditVMs;

namespace BusinessManagementApp
{
    public static class Startup
    {

        private static IHttpClientBuilder ConfigHttpClientBuilder(this IHttpClientBuilder builder, bool auth = false, bool logging = false)
        {
            String BASE_URL = ""; //TODO
            builder = builder.ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(BASE_URL);
            });
            if (auth)
            {
                // builder.AddHttpMessageHandler<HttpAuthHandler>(); //TODO
            }
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
                // service.AddRefitClient<IModelRemote>(settings).ConfigHttpClientBuilder(true, true);

                service.AddSingleton<IAuthenticator, Authenticator>();
                service.AddSingleton<ScheluderProvider>();

                var localRemote = new LocalModelRemote();
                service.AddSingleton<IBillRepository>(localRemote);
                service.AddSingleton<IContractRepository>(localRemote);
                service.AddSingleton<ICustomerRepository>(localRemote);
                service.AddSingleton<IDepartmentRepository>(localRemote);
                service.AddSingleton<IEmployeeRepository>(localRemote);
                service.AddSingleton<IPositionRepository>(localRemote);
                service.AddSingleton<IProductRepository>(localRemote);
                service.AddSingleton<IRecordRepository>(localRemote);
                service.AddSingleton<IVoucherRepository>(localRemote);

                //service.Decorate<IModelRemote, CacheModelRemote>();
            });
            return host;
        }

        /*public static IHostBuilder AddViewStates(this IHostBuilder host)
        {
            host.ConfigureServices((context, service) =>
            {
                service.AddSingleton<MainViewState>();
            });
            return host;
        }

        private static void AddViewModel<T>(this IServiceCollection c) where T : BaseViewModel
        {
            c.AddTransient<T>();
            c.AddSingleton<ViewModelCreator<T>>(s => s.GetRequiredService<T>);
        }*/

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
