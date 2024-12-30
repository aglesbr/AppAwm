using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAwm.Models
{

    [Table("AWM_CLIENTE", Schema = "dbo")]
    public class Cliente
    {
        [Key]
        [Column("CD_CLIENTE", TypeName = "INT", Order = 1)]
        public int Cd_Cliente { get; set; }

        [Column("NOME", TypeName = "VARCHAR(100)", Order = 5)]
        public string? Nome { get; set; }

        [Column("USUARIOMQ", TypeName = "VARCHAR(30)", Order = 10)]
        public string? UsuarioMq { get; set; }

        [Column("PASSWORDMQ", TypeName = "VARCHAR(100)", Order = 15)]
        public string? PasswordMq { get; set; }

        [Column("HOSTMQ", TypeName = "VARCHAR(100)", Order = 20)]
        public string? HostMq { get; set; }

        [Column("CANALMQ", TypeName = "VARCHAR(30)", Order = 21)]
        public string? CanalMq { get; set; }

        [Column("STATUS", TypeName = "BIT", Order = 25)]
        public bool Status { get; set; }

        [Column("PLANO_PACOTE_VIDAS", TypeName = "VARCHAR(50)", Order = 26)]
        public string? PlanoPacoteVidas { get; set; }

        [Column("PLANO_VIDAS", TypeName = "INT", Order = 27)]
        public int PlanoVidas { get; set; }

        [Column("PLANO_VIDAS_ATIVADAS", TypeName = "INT", Order = 28)]
        public int PlanoVidasAtivadas { get; set; }

        [NotMapped]
        public virtual ICollection<Empresa>? Empresas { get; } = null;
    }
}
