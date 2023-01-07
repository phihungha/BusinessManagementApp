using Newtonsoft.Json;

namespace BusinessManagementApp.Data.Model
{
    public class ContractType
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("base_salary")] public decimal BaseSalary { get; set; }

        [JsonProperty("period")] public int? Period { get; set; }
    }
}