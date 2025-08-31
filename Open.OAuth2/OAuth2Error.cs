using System.Text.Json.Serialization;

namespace Open.OAuth2
{
    public class OAuth2Error
    {
        [JsonPropertyName("error")]
        public string Error { get; set; }
        [JsonPropertyName("error_description")]
        public string ErrorDescription { get; set; }
    }
}
