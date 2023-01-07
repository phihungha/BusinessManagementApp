using Newtonsoft.Json;

namespace BusinessManagementApp.Data.Model
{
    public class CustomerStats
    {
        [JsonProperty("customer")] public Customer Customer { get; set; }

        [JsonProperty("num_of_orders")] public int NumOfOrders { get; set; }

        [JsonProperty("revenue")] public decimal Revenue { get; set; }
    }
}