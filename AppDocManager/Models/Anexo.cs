using AppDocManager.Models.Enum;
using Newtonsoft.Json;
using System;

namespace AppDocManager.Models
{
    public class Anexo
    {
        [JsonProperty("cd_Anexo")]
        public int? Cd_Anexo { get; set; }

        [JsonProperty("cd_Funcionario_Id")]
        public int? Cd_Funcionario_Id { get; set; }
        
        [JsonProperty("cd_Empresa_Id")]
        public int? Cd_Empresa_Id { get; set; }
        
        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("tipoAnexo")]
        public string TipoAnexo { get; set; }

        [JsonProperty("status")]
        public EnumStatusDocs Status { get; set; }

        [JsonProperty("dt_Criacao")]
        public DateTime? Dt_Criacao { get; set; }

        [JsonProperty("cd_UsuarioCriacao")]
        public string Cd_UsuarioCriacao { get; set; }

        [JsonProperty("totalRegistro")]
        public int TotalRegistro { get; set; }

        [JsonProperty("totalPaginas")]
        public int TotalPaginas { get; set; }

        [JsonProperty("arquivo")]
        public byte[] Arquivo { get; set; }

        [JsonProperty("funcionario")]
        public Funcionario Funcionario { get; set; }    
    }
}
