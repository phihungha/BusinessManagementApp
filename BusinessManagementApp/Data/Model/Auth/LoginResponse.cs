using Newtonsoft.Json;

namespace BusinessManagementApp.Data.Model.Auth
{
    public class LoginResponse
    {

        [JsonProperty("x-access-token")]
        public string? AccessToken { get; set; }

        [JsonProperty("x-refresh-token")]
        public string? RefreshToken { get; set; }

        [JsonProperty(Required = Required.Always, PropertyName = "authenticated")]
        public bool IsAuthenticated { get; set; } 

    }
}
