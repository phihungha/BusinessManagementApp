using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BusinessManagementApp.Data.Model
{
    public class SkillOverview
    {
        // Only returns Id, Name
        [JsonProperty("employee")] public Employee Employee { get; set; }

        [JsonProperty("last_updated_time")] public DateTime LastUpdatedTime { get; set; }

        // Only returns when getting details
        [JsonProperty("skills")] public List<SkillRecord> Skills { get; set; }
    }
}