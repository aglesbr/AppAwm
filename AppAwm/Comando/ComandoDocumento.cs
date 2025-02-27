using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AppAwm.Comando
{
    public class ComandoDocumento
    {
        [JsonProperty("cd_Cargo_Id")]
        [JsonPropertyName("cd_Cargo_Id")]
        public int Cd_Cargo_Id { get; set; }

        [JsonProperty("cd_Documento_Id")]
        [JsonPropertyName("cd_Documento_Id")]
        public int Cd_Documento_Id { get; set; }

        [JsonProperty("cd_Documento_Complementar")]
        [JsonPropertyName("cd_Documento_Complementar")]
        public int Cd_Documento_Complementar { get; set; }

        [JsonProperty("cd_Empresa")]
        [JsonPropertyName("cd_Empresa")]
        public int Cd_Empresa_Id { get; set; }

        [JsonProperty("origem")]
        [JsonPropertyName("origem")]
        public int Origem { get; set; }

        [JsonProperty("vinculado")]
        [JsonPropertyName("vinculado")]
        public bool Vinculado { get; set; }

        [JsonProperty("status")]
        [JsonPropertyName("status")]
        public bool Status { get; set; }
    }
}
