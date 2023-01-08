using Newtonsoft.Json;
using System;

namespace BusinessManagementApp.Data.Model
{
    public class BonusType : IEquatable<BonusType>
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("amount")] public decimal Amount { get; set; }

        public bool Equals(BonusType? other)
        {
            if (other == null) return false;

            return Id == other.Id;
        }
    }
}