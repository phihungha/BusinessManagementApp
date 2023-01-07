using Newtonsoft.Json;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Model
{
    public class SalesStats
    {
        [JsonProperty("revenue_by_day")] public List<decimal> RevenueByDay { get; set; }

        [JsonProperty("total_revenue")] public decimal TotalRevenue { get; set; }

        [JsonProperty("avg_revenue")] public decimal AvgRevenue { get; set; }

        [JsonProperty("avg_num_of_orders_per_employee")]
        public double AvgNumOfOrdersPerEmployee { get; set; }

        [JsonProperty("num_of_orders_completed")]
        public int NumOfOrdersCompleted { get; set; }

        [JsonProperty("num_of_orders_canceled")]
        public int NumOfOrdersCanceled { get; set; }

        [JsonProperty("num_of_orders_returned")]
        public int NumOfOrdersReturned { get; set; }

        [JsonProperty("num_of_orders_made")] public int NumOfOrdersMade { get; set; }

        [JsonProperty("product_category_stats")]
        public List<ProductCategoryStats> ProductCategoryStats { get; set; }

        [JsonProperty("product_stats")] public List<ProductStats> ProductStats { get; set; }

        [JsonProperty("customer_stats")] public List<CustomerStats> CustomerStats { get; set; }

        [JsonProperty("employee_stats")] public List<EmployeeStats> EmployeeStats { get; set; }
    }
}