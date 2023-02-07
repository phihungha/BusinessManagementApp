using Newtonsoft.Json;
using System;

namespace BusinessManagementApp.Data.Model
{
    public class Department : IEquatable<Department>
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("phone_number")] public string PhoneNumber { get; set; }

        // Only include Id and Name
        [JsonProperty("head_employee")] public Employee Head { get; set; }

        // Supports WPF ComboBox binding.
        // Comparing Id is usually enough.
        public bool Equals(Department? other)
        {
            if (other == null) return false;

            return Id == other.Id;
        }
    }
}