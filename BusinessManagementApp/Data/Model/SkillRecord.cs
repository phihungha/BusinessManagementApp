using Newtonsoft.Json;
using System;

namespace BusinessManagementApp.Data.Model
{
    public class SkillRecord
    {
        [JsonProperty("employee_id")] public string EmployeeId { get; set; }

        [JsonProperty("skill")] public SkillType SkillType { get; set; }

        [JsonProperty("level")] public SkillLevel Level { get; set; }

        [JsonProperty("last_updated_time")] public DateTime LastUpdatedTime { get; set; }
    }
}