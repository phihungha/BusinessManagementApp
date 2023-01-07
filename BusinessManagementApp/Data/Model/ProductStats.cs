using Newtonsoft.Json;

namespace BusinessManagementApp.Data.Model
{
    public class ProductStats
    {
        // Returns only Id, Name
        [JsonProperty("product")] public Product Product { get; set; }

        [JsonProperty("quantity_sold")] public int QuantitySold { get; set; }

        [JsonProperty("revenue")] public decimal Revenue { get; set; }
    }
}