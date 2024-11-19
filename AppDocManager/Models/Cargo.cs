
using Newtonsoft.Json;

namespace AppDocManager.Models
{
    public class Cargo
    {
        [JsonProperty("cd_Cargo")]
        public int Cd_Cargo { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; } = true;
    }
}
