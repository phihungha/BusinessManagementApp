using Newtonsoft.Json;
using System;

namespace BusinessManagementApp.Data.Model
{
    public class ProductCategory : IEquatable<ProductCategory>
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        public bool Equals(ProductCategory? other)
        {
            if (other == null) return false;

            return Id == other.Id;
        }
    }
}