using Newtonsoft.Json;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Model
{
    public class Overview
    {
        [JsonProperty("num_of_pending_orders")]
        public int NumOfPendingOrders { get; set; }

        [JsonProperty("total_stock")] public int TotalStock { get; set; }

        [JsonProperty("today_revenue")] public int TodayRevenue { get; set; }

        [JsonProperty("pending_orders")] public List<Order> PendingOrders { get; set; }
    }
}