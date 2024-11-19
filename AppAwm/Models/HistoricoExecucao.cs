using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAwm.Models
{
    [Table("AWM_HISTORICO_EXECUCAO", Schema = "dbo")]
    public class HistoricoExecucao
    {
        [Key]
        [Column("ID", TypeName = "INT", Order = 1)]
        public int Id { get; set; }

        [Column("DT_EXECUCAO", TypeName = "DATE", Order = 10)]
        public DateTime Dt_Execucao{ get; set; }
    }
}
