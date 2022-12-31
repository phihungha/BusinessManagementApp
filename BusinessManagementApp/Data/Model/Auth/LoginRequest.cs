using Newtonsoft.Json;

namespace BusinessManagementApp.Data.Model.Auth
{
    public class LoginRequest
    {
        [JsonProperty("username")]
        public string? UserName { get; set; }

        [JsonProperty("password")]
        public string? Password { get; set; }
    }
}
