using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Model
{
    public class OvertimeOverview
    {
        [JsonProperty("month_year")] public DateTime MonthYear { get; set; }

        // Only returns Id and Name
        [JsonProperty("employee")] public Employee Employee { get; set; }

        [JsonProperty("num_of_overtime_days")] public int NumOfOvertimeDays { get; set; }

        [JsonProperty("avg_overtime_duration")]
        public double AvgOvertimeDuration { get; set; }

        [JsonProperty("total_overtime_pay")] public decimal TotalOvertimePay { get; set; }

        // Only returns when getting details
        [JsonProperty("records")] public List<OvertimeRecord> Records { get; set; }
    }
}