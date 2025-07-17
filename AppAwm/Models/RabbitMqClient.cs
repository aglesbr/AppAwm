using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AppAwm.Models
{
    public class RabbitMqClient
    {
        [JsonProperty("username")]
        [JsonPropertyName("username")]
        public string? UserName { get; set; }

        [JsonProperty("password")]
        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonProperty("host")]
        [JsonPropertyName("host")]
        public string? Host { get; set; }

        [JsonProperty("port")]
        [JsonPropertyName("port")]
        public int Port { get; set; } = 5672;

        [JsonProperty("canal")]
        [JsonPropertyName("canal")]
        public string? Canal { get; set; }

        [JsonProperty("rota")]
        [JsonPropertyName("rota")]
        public string? Rota { get; set; }
    }
}
