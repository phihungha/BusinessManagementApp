using Newtonsoft.Json;
using System;

namespace BusinessManagementApp.Data.Model
{
    public class Customer
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("gender")] public Gender Gender { get; set; }

        [JsonProperty("birthday")] public DateTime Birthday { get; set; }

        [JsonProperty("email")] public string Email { get; set; }

        [JsonProperty("phone")] public string Phone { get; set; }

        [JsonProperty("address")] public string Address { get; set; }
    }
}