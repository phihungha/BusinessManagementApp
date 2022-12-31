using BusinessManagementApp.Data;
using BusinessManagementApp.Data.Api;
using BusinessManagementApp.Data.Model;
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
        private static IHttpClientBuilder ConfigHttpClientBuilder(this IHttpClientBuilder builder, bool auth = false,
            bool logging = false, string subUrl = "api")
        {
            string BaseApiUrl = "https://evident-castle-371707.as.r.appspot.com/" + subUrl;
            BaseApiUrl = BaseApiUrl.EndsWith("/") ? BaseApiUrl : BaseApiUrl + "/";
            builder = builder.ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(BaseApiUrl);
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

        public static IHostBuilder AddApiClient(this IHostBuilder host)
        {
            host.ConfigureServices((context, service) =>
            {
                service.AddSingleton<LoginSession>();
                service.AddSingleton<SchedulerProvider>();

                RefitSettings settings = new RefitSettings(new NewtonsoftJsonContentSerializer(new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                }));
                service.AddTransient<HttpAuthHandler>();
                service.AddRefitClient<IAuthRemote>(settings).ConfigHttpClientBuilder(false, true, "");

                service.AddRefitClient<IContractApi>(settings).ConfigHttpClientBuilder(true, true);
                service.AddRefitClient<ICustomerApi>(settings).ConfigHttpClientBuilder(true, true);
                service.AddRefitClient<IDepartmentApi>(settings).ConfigHttpClientBuilder(true, true);
                service.AddRefitClient<IEmployeeApi>(settings).ConfigHttpClientBuilder(true, true);
                service.AddRefitClient<IOrderApi>(settings).ConfigHttpClientBuilder(true, true);
                service.AddRefitClient<IPositionApi>(settings).ConfigHttpClientBuilder(true, true);
                service.AddRefitClient<IProductApi>(settings).ConfigHttpClientBuilder(true, true);
                service.AddRefitClient<IRecordApi>(settings).ConfigHttpClientBuilder(true, true);
                service.AddRefitClient<IVoucherApi>(settings).ConfigHttpClientBuilder(true, true);
            });
            return host;
        }

        // Add new repositories here.
        public static IHostBuilder AddRepositories(this IHostBuilder host)
        {
            host.ConfigureServices((context, service) =>
            {
                service.AddSingleton<BonusesRepo>();
                service.AddSingleton<BonusTypesRepo>();
                service.AddSingleton<ConfigRepo>();
                service.AddSingleton<ContractTypesRepo>();
                service.AddSingleton<CustomersRepo>();
                service.AddSingleton<DepartmentsRepo>();
                service.AddSingleton<EmployeeRepo>();
                service.AddSingleton<OrdersRepo>();
                service.AddSingleton<OverviewRepo>();
                service.AddSingleton<OvertimeRepo>();
                service.AddSingleton<PositionsRepo>();
                service.AddSingleton<ProductsRepo>();
                service.AddSingleton<ProductCategoriesRepo>();
                service.AddSingleton<ProvidersRepo>();
                service.AddSingleton<SalaryRecordsRepo>();
                service.AddSingleton<SkillsRepo>();
                service.AddSingleton<SkillTypesRepo>();
                service.AddSingleton<SessionsRepo>();
                service.AddSingleton<SalesReportRepo>();
                service.AddSingleton<VouchersRepo>();
                service.AddSingleton<VoucherTypesRepo>();
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
                service.AddTransient<BonusTypeDetailsVM>();
                service.AddTransient<ContractTypesVM>();
                service.AddTransient<ContractTypeDetailsVM>();
                service.AddTransient<CustomersVM>();
                service.AddTransient<CustomerDetailsVM>();
                service.AddTransient<DepartmentsVM>();
                service.AddTransient<DepartmentDetailsVM>();
                service.AddTransient<EmployeesVM>();
                service.AddTransient<EmployeeDetailsVM>();
                service.AddTransient<OrdersVM>();
                service.AddTransient<OrderDetailsVM>();
                service.AddTransient<OvertimeVM>();
                service.AddTransient<OvertimeDetailsVM>();
                service.AddTransient<OverviewVM>();
                service.AddTransient<PositionsVM>();
                service.AddTransient<PositionDetailsVM>();
                service.AddTransient<ProductsVM>();
                service.AddTransient<ProductDetailsVM>();
                service.AddTransient<ProductCategoriesVM>();
                service.AddTransient<ProductCategoryDetailsVM>();
                service.AddTransient<ProvidersVM>();
                service.AddTransient<ProviderDetailsVM>();
                service.AddTransient<SalaryReportVM>();
                service.AddTransient<SalesReportVM>();
                service.AddTransient<SelectCustomersVM>();
                service.AddTransient<SelectProductsVM>();
                service.AddTransient<SelectProductOrderItemsVM>();
                service.AddTransient<SkillRatingVM>();
                service.AddTransient<SkillRatingDetailsVM>();
                service.AddTransient<SkillTypesVM>();
                service.AddTransient<SkillTypeDetailsVM>();
                service.AddTransient<VouchersVM>();
                service.AddTransient<VoucherDetailsVM>();
                service.AddTransient<VoucherTypesVM>();
                service.AddTransient<VoucherTypeDetailsVM>();
            });
            return host;
        }
    }
}