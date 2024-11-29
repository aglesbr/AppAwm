using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAwm.Models
{
    [Table("AWM_DOCUMENTO_COMPLEMENTAR", Schema = "dbo")]
    public class DocumentacaoComplementar
    {
        [Key]
        [Column("CD_DOCUMENTACO_COMPLEMENTAR", TypeName = "INT", Order = 1)]
        public int Cd_Documentaco_Complementar { get; set; }

        [Column("CD_DOCUMENTOCOMPLEMENTAR_ID", TypeName = "VARCHAR(6)", Order = 5)]
        public string? Cd_DocumentoComplementar_Id { get; set; }

        [Column("NOME", TypeName = "VARCHAR(150)", Order = 10)]
        public string? Nome { get; set; }

        [Column("STATUS", Order = 15)]
        public bool Status { get; set; }
    }
}
