using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Model
{
    public class Employee : IEquatable<Employee>
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("gender")] public Gender Gender { get; set; }

        [JsonProperty("citizen_id")] public string CitizenId { get; set; }

        [JsonProperty("birthday")] public DateTime BirthDate { get; set; }

        [JsonProperty("education")] public string Education { get; set; }

        [JsonProperty("phone")] public string PhoneNumber { get; set; }

        [JsonProperty("email")] public string Email { get; set; }

        [JsonProperty("address")] public string Address { get; set; }

        [JsonProperty("username")] public string? UserName { get; set; }

        [JsonProperty("new_password")] public string? NewPassword { get; set; }

        // Only include Id and Name
        [JsonProperty("department")] public Department Department { get; set; }

        [JsonProperty("current_position")] public Position? CurrentPosition { get; set; }

        [JsonProperty("position_records")] public List<PositionRecord> PositionRecords { get; set; }

        [JsonProperty("current_contract")] public Contract? CurrentContract { get; set; }

        [JsonProperty("contracts")] public List<Contract> Contracts { get; set; }

        [JsonProperty("termination_date")] public DateTime? TerminationDate { get; set; }

        public bool Equals(Employee? other)
        {
            if (other == null) return false;

            return Id == other.Id;
        }
    }
}