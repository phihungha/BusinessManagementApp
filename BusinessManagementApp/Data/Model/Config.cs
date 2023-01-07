using Newtonsoft.Json;

namespace BusinessManagementApp.Data.Model
{
    public class Config
    {
        [JsonProperty("overtime_hourly_rate")] public decimal OvertimeHourlyRate { get; set; }

        [JsonProperty("max_num_of_overtime_hours")]
        public int MaxNumOfOvertimeHours { get; set; }

        [JsonProperty("vat_rate")] public double VATRate { get; set; }
    }
}