using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppAwm.Models
{
    [Table("AWM_DEPARTAMENTO", Schema = "dbo")]
    public class Departamento
    {
        [Key]
        [Column("CD_DEPARTAMENTO", TypeName = "INT", Order = 1)]
        public int Cd_Departamento { get; set; }

        [Display(Name = "Departamento")]
        [Column("NOME", TypeName = "VARCHAR(50)", Order = 5)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o nome do departamento")]
        public string? Nome { get; set; }

        [Column("DT_CRIACAO", TypeName = "DATE", Order = 10)]
        public DateTime Dt_Criacao { get; set; } = DateTime.Now;

        [Column("CD_USUARIO_CRIACAO", TypeName = "VARCHAR(30)", Order = 15)]
        [MaxLength(30)]
        public string? Cd_UsuarioCriacao { get; set; }

        [Column("DT_ATUALIZACAO", TypeName = "DATE", Order = 20)]
        public DateTime? Dt_Atualizacao { get; set; }

        [Column("CD_USUARIO_ATUALIZACAO", TypeName = "VARCHAR(30)", Order = 25)]
        [MaxLength(30)]
        public string? Cd_UsuarioAtualizacao { get; set; }

        [Column("STATUS", Order = 30)]
        public bool Status { get; set; }

        [NotMapped]
        public int? StatusFilter { get; set; }

        public virtual ICollection<Cargo>? Cargos { get; set; }
    }
}
