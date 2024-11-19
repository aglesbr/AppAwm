using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AppAwm.Comando
{
    public class ComandoObra
    {
        [JsonProperty("cd_Obra")]
        [JsonPropertyName("cd_Obra")]
        public int Cd_Obra { get; set; }

        [JsonProperty("cd_Empresa_id")]
        [JsonPropertyName("cd_Empresa_id")]
        public int Cd_Empresa_Id { get; set; }

        [JsonProperty("nome")]
        [JsonPropertyName("nome")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o nome da Obra.")]
        public string? Nome { get; set; }

        [JsonProperty("descricao")]
        [JsonPropertyName("descricao")]
        public string? Descricao { get; set; }

        [JsonProperty("status")]
        [JsonPropertyName("status")]
        public bool Status { get; set; }
    }
}
