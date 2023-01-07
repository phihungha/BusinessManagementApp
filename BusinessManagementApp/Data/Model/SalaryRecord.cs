using Newtonsoft.Json;

namespace BusinessManagementApp.Data.Model
{
    public class SalaryRecord
    {
        // Only returns Id, Name
        [JsonProperty("employee")] public Employee Employee { get; set; }

        [JsonProperty("month")] public int Month { get; set; }

        [JsonProperty("year")] public int Year { get; set; }

        [JsonProperty("base_salary")] public decimal BaseSalary { get; set; }

        [JsonProperty("supplement_salary")] public decimal SupplementSalary { get; set; }

        [JsonProperty("bonus_salary")] public decimal BonusSalary { get; set; }

        [JsonProperty("total_overtime_pay")] public decimal TotalOvertimePay { get; set; }

        [JsonProperty("total_salary")] public decimal TotalSalary { get; set; }
    }
}