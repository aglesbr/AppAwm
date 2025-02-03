using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AppAwm.Comando
{
    public class ComandoDocumentoCargo
    {
        [JsonProperty("cd_Cargo_Id")]
        [JsonPropertyName("cd_Cargo_Id")]
        public int Cd_Cargo_Id { get; set; }

        [JsonProperty("cd_Documento_Id")]
        [JsonPropertyName("cd_Documento_Id")]
        public int Cd_Documento_Id { get; set; }

        [JsonProperty("cd_Empresa")]
        [JsonPropertyName("cd_Empresa")]
        public int Cd_Empresa { get; set; }

        [JsonProperty("vinculado")]
        [JsonPropertyName("vinculado")]
        public bool Vinculado { get; set; }

        [JsonProperty("status")]
        [JsonPropertyName("status")]
        public bool Status { get; set; }
    }
}
