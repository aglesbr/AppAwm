using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAwm.Models
{
    [Table("AWM_LOG_EXCEPTION")]
    public class LogException
    {
        [Key]
        [Column("CD_CARGO", TypeName = "INT", Order = 1)]
        public int Cd_Erro { get; set; }

        [Column("METODO", TypeName = "VARCHAR(100)", Order = 5)]
        public string? Metodo { get; set; }

        [Column("ORIGEMTRACE", TypeName = "VARCHAR(300)", Order = 6)]
        public string? OrigemTrace { get; set; }

        [Column("DATAEXCEPTION", TypeName = "DATE", Order = 10)]
        public  DateTime DataException { get; set; } = DateTime.Now;

        [Column("ERROR", TypeName = "VARCHAR(500)", Order = 15)]
        public string? Error { get; set; }
    }
}
