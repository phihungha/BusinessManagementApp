using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Services;
using BusinessManagementApp.Utils;
using BusinessManagementApp.ViewModels;
using BusinessManagementApp.ViewModels.DetailsVMs;
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

        public static IHostBuilder AddApis(this IHostBuilder host)
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
            });
            return host;
        }

        public static IHostBuilder AddRepositories(this IHostBuilder host)
        {
            host.ConfigureServices((context, service) =>
            {
                service.AddSingleton<EmployeeRepo>();
                service.AddSingleton<DepartmentsRepo>();
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

                service.AddTransient<BonusesVM>();
                service.AddTransient<BonusTypesVM>();
                service.AddTransient<ContractTypesVM>();
                service.AddTransient<ContractTypeDetailsVM>();
                service.AddTransient<CustomersVM>();
                service.AddTransient<CustomerDetailsVM>();
                service.AddTransient<DepartmentsVM>();
                service.AddTransient<DepartmentDetailsVM>();
                service.AddTransient<EmployeeInfoVM>();
                service.AddTransient<EmployeeInfoDetailsVM>();
                service.AddTransient<OrdersVM>();
                service.AddTransient<OrderDetailsVM>();
                service.AddTransient<OvertimeRecordsVM>();
                service.AddTransient<OvertimeRecordDetailsVM>();
                service.AddTransient<OverviewVM>();
                service.AddTransient<PositionsVM>();
                service.AddTransient<PositionDetailsVM>();
                service.AddTransient<ProductsVM>();
                service.AddTransient<ProductDetailsVM>();
                service.AddTransient<ProvidersVM>();
                service.AddTransient<ProviderDetailsVM>();
                service.AddTransient<SalaryReportVM>();
                service.AddTransient<SalesReportVM>();
                service.AddTransient<SelectOrderItemsVM>();
                service.AddTransient<SkillRatingVM>();
                service.AddTransient<SkillTypesVM>();
                service.AddTransient<VouchersVM>();
                service.AddTransient<VoucherDetailsVM>();
                service.AddTransient<VoucherTypesVM>();
                service.AddTransient<VoucherTypeDetailsVM>();
            });
            return host;
        }
    }
}