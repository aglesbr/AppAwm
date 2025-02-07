using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppAwm.Models
{
    [Table("AWM_DOWNLOAD")]
    public class Download
    {
        [Key]
        [Column("CD_DOWNLOAD", TypeName = "INT", Order = 1)]
        public int Cd_Download { get; set; }

        [Column("NOME", TypeName = "VARCHAR(50)", Order = 5)]
        public required string  Nome { get; set; }

        [Column("DESCRICAO", TypeName = "VARCHAR(150)", Order = 10)]
        public string? Descricao { get; set; }

        [Column("ANEXO", Order = 15)]
        public required byte[] Anexo { get; set; }

        [Column("DT_CRIACAO", TypeName = "DATE", Order = 20)]
        public DateTime Dt_Criacao { get; set; } = DateTime.Now;

        [Column("CD_USUARIO_CRIACAO", TypeName = "VARCHAR(50)", Order =25)]
        [MaxLength(50)]
        public string? Cd_UsuarioCriacao { get; set; }
    }
}
