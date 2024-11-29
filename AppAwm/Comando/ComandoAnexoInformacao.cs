using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AppAwm.Comando
{
    public class ComandoAnexoInformacao
    {
        [JsonProperty("codigo")]
        [JsonPropertyName("codigo")]
        public string? Codigo { get; set; }

        [JsonProperty("descricao")]
        [JsonPropertyName("descricao")]
        public string? Descricao { get; set; }

        [JsonProperty("dataValidade")]
        [JsonPropertyName("dataValidade")]
        public string? DataValidade { get; set; }

        [JsonProperty("scope")]
        [JsonPropertyName("scope")]
        public string? Scope { get; set; }

        [JsonProperty("documento")]
        [JsonPropertyName("documento")]
        public string? Documento { get; set; }

        [JsonProperty("tipoAnexo")]
        [JsonPropertyName("tipoAnexo")]
        public string? TipoAnexo { get; set; }

        [JsonProperty("codigoEmpresa")]
        [JsonPropertyName("codigoEmpresa")]
        public string? CodigoEmpresa { get; set; }

        [JsonProperty("CodigoCargo")]
        [JsonPropertyName("CodigoCargo")]
        public string? CodigoCargo { get; set; }

    }
}
