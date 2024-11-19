using Newtonsoft.Json;

namespace AppDocManager.Models
{
    public class Empresa
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("cnpj")]
        public string Cnpj { get; set;}

        [JsonProperty("email")]
        public string Email { get; set;}

        [JsonProperty("telefone")]
        public string Telefone { get; set;}
    }
}
