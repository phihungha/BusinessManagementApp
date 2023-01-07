using Newtonsoft.Json;

namespace BusinessManagementApp.Data.Model
{
    public class SkillType
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("description")] public string Description { get; set; }
    }
}