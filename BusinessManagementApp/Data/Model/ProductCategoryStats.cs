using Newtonsoft.Json;

namespace BusinessManagementApp.Data.Model
{
    public class ProductCategoryStats
    {
        [JsonProperty("category")] public ProductCategory Category { get; set; }

        [JsonProperty("quantity_sold")] public int QuantitySold { get; set; }
    }
}