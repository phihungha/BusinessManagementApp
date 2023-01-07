using Newtonsoft.Json;
using System;

namespace BusinessManagementApp.Data.Model
{
    public class PositionRecord
    {
        [JsonProperty("employee_id")] public string EmployeeId { get; set; }

        [JsonProperty("start_date")] public DateTime StartDate { get; set; }

        [JsonProperty("end_date")] public DateTime? EndDate { get; set; }

        [JsonProperty("is_current")] public bool IsCurrent { get; set; }

        [JsonProperty("position")] public Position Position { get; set; }
    }
}