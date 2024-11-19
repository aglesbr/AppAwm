using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppAwm.Models
{
    [Table("AWM_CARGO", Schema = "dbo")]
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
        public virtual Funcionario? Funcionario { get; set; }
    }
}
