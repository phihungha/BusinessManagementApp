using Newtonsoft.Json;

namespace BusinessManagementApp.Data.Model
{
    public class Product
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("unit")] public string Unit { get; set; }

        [JsonProperty("price")] public decimal Price { get; set; }

        [JsonProperty("stock")] public int Stock { get; set; }

        // Only returns Id and Name
        [JsonProperty("category")] public ProductCategory Category { get; set; }

        // Only returns Id and Name
        [JsonProperty("provider")] public Provider Provider { get; set; }
    }
}