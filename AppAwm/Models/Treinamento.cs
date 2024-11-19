using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAwm.Models
{
    [Table("AWM_TREINAMENTO", Schema = "dbo")]
    public class Treinamento
    {
        [Key]
        [Column("CD_TREINAMENTO", TypeName = "int", Order = 1)]
        public int Cd_Treinamento { get; set; }

        [Column("NOME", TypeName = "VARCHAR(50)", Order = 10)]
        public required string Nome { get; set; }

        [NotMapped]
        public required DateTime Validade_Treinamento { get; set; }

        [Column("STATUS", TypeName = "BIT", Order = 30)]
        public bool Status { get; set; }

        [NotMapped]
        public virtual ICollection<TreinamentoHabilidade>? Habilidades { get; set; } =  null;

    }
}
