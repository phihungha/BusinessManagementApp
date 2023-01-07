using Newtonsoft.Json;

namespace BusinessManagementApp.Data.Model
{
    public class OrderItem
    {
        [JsonProperty("order_id")] public int OrderId { get; set; }

        [JsonProperty("product")] public Product Product { get; set; }

        [JsonProperty("unit_price")] public decimal UnitPrice { get; set; }

        [JsonProperty("quantity")] public int Quantity { get; set; }
    }
}