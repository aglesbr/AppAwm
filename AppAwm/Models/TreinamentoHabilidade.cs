using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAwm.Models
{
    [Table("AWM_TREINAMENTO_HABILIDADE", Schema = "dbo")]
    public class TreinamentoHabilidade
    {
        [Key]
        [Column("CD_HABILIDADE", TypeName = "int", Order = 1)]
        public int Cd_Habilidade { get; set; }


        [Column("CD_TREINAMENTO_ID", TypeName = "int", Order = 10)]
        public int Cd_Treinamento_Id { get; set; }


        [Column("NOME", TypeName = "VARCHAR(50)", Order = 20)]
        public required string Nome { get; set; }


        [Column("STATUS", TypeName = "BIT", Order = 30)]
        public bool Status { get; set; }

        [NotMapped]
        public virtual Treinamento? Treinamento { get; set; } = null;
    }
}
