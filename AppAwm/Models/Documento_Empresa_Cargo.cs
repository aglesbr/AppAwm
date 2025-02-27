using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AppAwm.Models
{
    public class Documento_Empresa_Cargo
    {
        public int Cd { get; set; }
        public int Origem { get; set; }
        public string? Nome { get; set; }
        public bool Status { get; set; }

    }
}
