using Newtonsoft.Json;
using System;
using Newtonsoft.Json;

namespace BusinessManagementApp.Data.Model
{
    public class Contract
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("employee_id")] public string EmployeeId { get; set; }

        [JsonProperty("company_representative_employee_id")]
        public string CompanyRepresentativeEmployeeId { get; set; }

        [JsonProperty("type")] public ContractType Type { get; set; }

        [JsonProperty("is_current")] public bool IsCurrent { get; set; }

        [JsonProperty("start_date")] public DateTime StartDate { get; set; }

        [JsonProperty("end_date")] public DateTime? EndDate { get; set; }
    }
}