using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAwm.Models
{
    [Table("AWM_OBRA")]
    public class Obra
    {
        [Key]
        [Column("CD_OBRA", TypeName = "INT", Order = 1)]
        public int Cd_Obra { get; set; }

        [Column("CD_EMPRESA_ID", TypeName = "INT", Order = 5)]
        public int Cd_Empresa_Id { get; set; }

        [Column("NOME", TypeName = "VARCHAR(100)", Order = 10)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o nome da Obra.")]
        public string? Nome { get; set; }

        [Column("DESCRICAO", TypeName = "VARCHAR(200)", Order = 15)]
        public string? Descricao { get; set; }

        [Column("STATUS", Order = 20)]
        public bool Status { get; set; }

        [Column("DT_CRIACAO", TypeName = "DATETIME", Order = 25)]
        public DateTime Dt_Criacao { get; set; } = DateTime.Now.Date;

        [Column("CD_USUARIO_CRIACAO", TypeName = "VARCHAR(30)", Order = 30)]
        [MaxLength(30)]
        public string? Cd_Usuario_Criacao { get; set; }

        [Column("DT_ATUALIZACAO", TypeName = "DATETIME", Order = 35)]
        public DateTime? Dt_Atualizacao { get; set; }

        [Column("CD_USUARIO_ATUALIZACAO", TypeName = "VARCHAR(30)", Order = 40)]
        [MaxLength(30)]
        public string? Cd_Usuario_Atualizacao { get; set; }

        [Column("CD_CLIENTE", TypeName = "INT", Order = 95)]
        public int Cd_Cliente { get; set; }

        [Column("ID_USUARIO_CRIACAO", TypeName = "INT", Order = 100)]
        public int Id_UsuarioCriacao { get; set; }

        [NotMapped]
        public virtual Empresa? Empresa { get; set; }
    }
}
