using Newtonsoft.Json;

namespace BusinessManagementApp.Data.Model
{
    public class EmployeeStats
    {
        // Returns only Id, Name
        [JsonProperty("employee")] public Employee Employee { get; set; }

        [JsonProperty("num_of_orders")] public int NumOfOrders { get; set; }

        [JsonProperty("revenue")] public decimal Revenue { get; set; }
    }
}