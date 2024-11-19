using Newtonsoft.Json;
using System;

namespace AppDocManager.Models
{
    public class Funcionario
    {
        [JsonProperty("cd_Funcionario")]
        public int Cd_Funcionario { get; set; }

        [JsonProperty("id_Empresa")]
        public int Id_Empresa { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("foto")]
        public byte[] Foto { get; set; }

        [JsonProperty("nascimento")]
        public DateTime Nascimento { get; set; }

        [JsonProperty("documento")]
        public string Documento { get; set; }

        [JsonProperty("telefone")]
        public string Telefone { get; set; }

        [JsonProperty("nacionalidade")]
        public bool Nacionalidade { get; set; }

        [JsonProperty("pcd")]
        public bool Pcd { get; set; }

        [JsonProperty("cd_Criacao")]
        public DateTime Dt_Criacao { get; set; } = DateTime.Now;

        [JsonProperty("cd_UsuarioCriacao")]
        public string Cd_UsuarioCriacao { get; set; }

        [JsonProperty("cd_Atualizacao")]
        public DateTime? Dt_Atualizacao { get; set; }

        [JsonProperty("cd_UsuarioAtualizacao")]
        public string Cd_UsuarioAtualizacao { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("observacao")]
        public string Observacao { get; set; }

        [JsonProperty("cargo")]
        public Cargo Cargo { get; set; }

        [JsonProperty("empresa")]
        public Empresa Empresa { get; set; }
    }
}
