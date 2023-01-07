using Newtonsoft.Json;
using System;

namespace BusinessManagementApp.Data.Model
{
    public class OvertimeRecord
    {
        [JsonProperty("employee_id")] public string EmployeeId { get; set; }

        [JsonProperty("date")] public DateTime Date { get; set; }

        [JsonProperty("num_of_hours")] public int NumOfHours { get; set; }
    }
}