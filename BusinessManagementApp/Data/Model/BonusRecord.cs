using Newtonsoft.Json;
using System;

namespace BusinessManagementApp.Data.Model
{
    public class BonusRecord
    {
        [JsonProperty("month_year")] public DateTime MonthYear { get; set; }

        [JsonProperty("employee")] public Employee Employee { get; set; }

        [JsonProperty("bonus_type")] public BonusType Type { get; set; }

        [JsonProperty("amount")] public decimal Amount { get; set; }
    }
}