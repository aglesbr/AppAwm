using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAwm.Models
{
    [Table("AWM_DOCUMENTO_EMPRESA")]
    public class DocumentacaoEmpresa
    {
        [Key]
        [Column("CD", TypeName = "INT", Order = 1)]
        public int Cd { get; set; }

        [Column("CD_EMPRESA_ID", TypeName = "INT", Order = 5)]
        public int Cd_Empresa_Id { get; set; }

        [Column("CD_DOCUMENTO_ID", TypeName = "INT", Order = 10)]
        public int Cd_Documento_Id { get; set; }

        [Column("CD_DOCUMENTOS_COMPLEMENTARES_ID", TypeName = "VARCHAR(80)", Order = 11)]
        public string? Cd_Documentos_Complementares_Id { get; set; }

        [Column("STATUS", TypeName = "INT", Order = 15)]
        public bool Status { get; set; }

        public virtual Empresa? Empresa { get; set; }
    }
}
