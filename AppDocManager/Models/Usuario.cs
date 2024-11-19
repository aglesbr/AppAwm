using Newtonsoft.Json;

namespace AppDocManager.Models
{
    public class Usuario
    {

        [JsonProperty("cd_Usuario")]
        public int Cd_Usuario { get; set; }

        [JsonProperty("perfil")]
        public int Perfil { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("documento")]
        public string Documento { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("telefone")]
        public string Telefone { get; set; }
    }
}
