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
using Newtonsoft.Json.Converters;

namespace BusinessManagementApp
{
    public static class Startup
    {
        // private const string BaseApiUrl = "http://localhost:3000/";
        private const string BaseApiUrl = "https://evident-castle-371707.as.r.appspot.com/";

        private static IHttpClientBuilder ConfigHttpClientBuilder(
            this IHttpClientBuilder builder,
            string subUrl,
            bool authRequired)
        {
            string apiEndpointUrl = $"{BaseApiUrl}{subUrl}";
            apiEndpointUrl = apiEndpointUrl.EndsWith("/") ? apiEndpointUrl.Substring(0, apiEndpointUrl.Length - 1) : apiEndpointUrl;
            builder = builder.ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(apiEndpointUrl);
            });

            builder.AddHttpMessageHandler<HttpLoggingHandler>();

            if (authRequired)
            {
                builder.AddHttpMessageHandler<HttpAuthHandler>();
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
                service.AddSingleton<SessionAuthData>();
                service.AddSingleton<SchedulerProvider>();
                service.AddTransient<HttpAuthHandler>();
                service.AddTransient<HttpLoggingHandler>();

                var jsonSettings = new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Converters =
                    {
                        new StringEnumConverter()
                    }
                };
                RefitSettings settings = new RefitSettings(new NewtonsoftJsonContentSerializer(jsonSettings));

                service.AddRefitClient<IAuthApi>(settings).ConfigHttpClientBuilder("", false);
                service.AddRefitClient<IBonusRecordsApi>(settings).ConfigHttpClientBuilder("api/bonuses", true);
                service.AddRefitClient<IBonusTypesApi>(settings).ConfigHttpClientBuilder("api/bonus-types", true);
                service.AddRefitClient<IConfigApi>(settings).ConfigHttpClientBuilder("api/config", true);
                service.AddRefitClient<IContractTypesApi>(settings).ConfigHttpClientBuilder("api/contract-types", true);
                service.AddRefitClient<ICustomersApi>(settings).ConfigHttpClientBuilder("api/customers", true);
                service.AddRefitClient<IDepartmentsApi>(settings).ConfigHttpClientBuilder("api/departments", true);
                service.AddRefitClient<IEmployeesApi>(settings).ConfigHttpClientBuilder("api/employees", true);
                service.AddRefitClient<IOrdersApi>(settings).ConfigHttpClientBuilder("api/orders", true);
                service.AddRefitClient<IOverviewApi>(settings).ConfigHttpClientBuilder("api/overview", true);
                service.AddRefitClient<IPositionsApi>(settings).ConfigHttpClientBuilder("api/positions", true);
                service.AddRefitClient<IProductsApi>(settings).ConfigHttpClientBuilder("api/products", true);
                service.AddRefitClient<IProductCategoriesApi>(settings).ConfigHttpClientBuilder("api/product-categories", true);
                service.AddRefitClient<IProvidersApi>(settings).ConfigHttpClientBuilder("api/providers", true);
                service.AddRefitClient<ISalaryRecordsApi>(settings).ConfigHttpClientBuilder("api/salary", true);
                service.AddRefitClient<IOvertimeRecordsApi>(settings).ConfigHttpClientBuilder("api/overtime", true);
                service.AddRefitClient<ISkillRecordsApi>(settings).ConfigHttpClientBuilder("api/skill-ratings", true);
                service.AddRefitClient<ISkillTypesApi>(settings).ConfigHttpClientBuilder("api/skill-types", true);
                service.AddRefitClient<ISalesReportsApi>(settings).ConfigHttpClientBuilder("api/sales-reports", true);
                service.AddRefitClient<IVouchersApi>(settings).ConfigHttpClientBuilder("api/vouchers", true);
                service.AddRefitClient<IVoucherTypesApi>(settings).ConfigHttpClientBuilder("api/voucher-types", true);
            });
            return host;
        }

        // Add new repositories here.
        public static IHostBuilder AddRepositories(this IHostBuilder host)
        {
            host.ConfigureServices((context, service) =>
            {
                service.AddSingleton<BonusRecordsRepo>();
                service.AddSingleton<BonusTypesRepo>();
                service.AddSingleton<ConfigRepo>();
                service.AddSingleton<ContractTypesRepo>();
                service.AddSingleton<CustomersRepo>();
                service.AddSingleton<DepartmentsRepo>();
                service.AddSingleton<EmployeeRepo>();
                service.AddSingleton<OrdersRepo>();
                service.AddSingleton<OverviewRepo>();
                service.AddSingleton<OvertimeRecordsRepo>();
                service.AddSingleton<PositionsRepo>();
                service.AddSingleton<ProductsRepo>();
                service.AddSingleton<ProductCategoriesRepo>();
                service.AddSingleton<ProvidersRepo>();
                service.AddSingleton<SalaryRecordsRepo>();
                service.AddSingleton<SkillRecordsRepo>();
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
                service.AddTransient<CreateOrderCustomerVM>();
                service.AddTransient<BonusesVM>();
                service.AddTransient<BonusTypesVM>();
                service.AddTransient<BonusTypeDetailsVM>();
                service.AddTransient<ContractTypesVM>();
                service.AddTransient<ContractTypeDetailsVM>();
                service.AddTransient<CustomersVM>();
                service.AddTransient<CustomerDetailsVM>();
                service.AddTransient<ConfigVM>();
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