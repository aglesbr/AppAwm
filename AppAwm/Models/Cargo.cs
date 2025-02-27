using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAwm.Models
{
    [Table("AWM_CARGO")]
    public class Cargo
    {
        [Key]
        [Column("CD_CARGO", TypeName = "INT", Order = 1)]
        public int Cd_Cargo { get; set; }

        [Column("NOME", TypeName = "VARCHAR(150)", Order = 5)]
        public string? Nome { get; set; }

        [Column("STATUS", Order = 10)]
        public bool Status { get; set; } = true;

        [NotMapped]
        public virtual ICollection<DocumentacaoCargo>? DocumentoComplementar { get; }

        [NotMapped]
        public virtual Colaborador? Colaborador { get; set; }
    }
}
