using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAwm.Models
{
    [Table("AWM_DOCUMENTO_CARGO", Schema = "dbo")]
    public class DocumentacaoCargo
    {
        [Key]
        [Column("CD", TypeName = "INT", Order = 1)]
        public int Cd { get; set; }

        [Column("CD_CARGO_ID", TypeName = "INT", Order = 5)]
        public int Cd_Cargo_Id { get; set; }

        [Column("CD_DOCUMENTO_ID", TypeName = "INT", Order = 10)]
        public int Cd_Documento_Id { get; set; }

        [Column("STATUS", TypeName = "BIT", Order = 15)]
        public bool Status { get; set; } = true;

        [NotMapped]
        public virtual Cargo? Cargo { get; }
    }
}
