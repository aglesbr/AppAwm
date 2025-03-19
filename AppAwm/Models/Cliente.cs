using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAwm.Models
{

    [Table("AWM_CLIENTE")]
    public class Cliente
    {
        [Key]
        [Column("CD_CLIENTE", TypeName = "INT", Order = 1)]
        public int Cd_Cliente { get; set; }

        [Display(Name = "Cliente")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o nome do cliente")]
        [Column("NOME", TypeName = "VARCHAR(100)", Order = 5)]
        public string? Nome { get; set; }

        [Display(Name = "CNPJ")]
        [Column("CNPJ", TypeName = "VARCHAR(14)", Order = 6)]
        [RegularExpression(@"(^\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}$)", ErrorMessage = "CNPJ inválidos.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o CNPJ da empresa.")]
        public string? Cnpj { get; set; }

        [Column("USUARIOMQ", TypeName = "VARCHAR(20)", Order = 10)]
        public string? UsuarioMq { get; set; } = "guest";

        [Column("PASSWORDMQ", TypeName = "VARCHAR(20)", Order = 15)]
        public string? PasswordMq { get; set; } = "guest";

        [Column("HOSTMQ", TypeName = "VARCHAR(50)", Order = 20)]
        public string HostMq { get; set; } = "localhost";

        [Column("CANALMQ", TypeName = "VARCHAR(30)", Order = 21)]
        public string CanalMq { get; set; } = "operacao";

        [Column("ROUTINGKEYMQ", TypeName = "VARCHAR(30)", Order = 22)]
        public string? RoutingKeyMq { get; set; }

        [Column("STATUS", TypeName = "BIT", Order = 25)]
        public bool Status { get; set; }

        [Display(Name = "Plano de Vidas")]
        [Column("PLANO_PACOTE_VIDAS", TypeName = "VARCHAR(50)", Order = 26)]
        public string? PlanoPacoteVidas { get; set; }

        [Display(Name = "Número de Vidas")]
        [Column("PLANO_VIDAS", TypeName = "INT", Order = 27)]
        public int PlanoVidas { get; set; }

        [Display(Name = "Vidas Ativas")]
        [Column("PLANO_VIDAS_ATIVADAS", TypeName = "INT", Order = 28)]
        public int PlanoVidasAtivadas { get; set; }

        [Column("DT_CRIACAO", TypeName = "DATE")]
        public DateTime Dt_Criacao { get; set; } = DateTime.Now;

        [Column("CD_USUARIO_CRIACAO", TypeName = "VARCHAR(30)")]
        [MaxLength(30)]
        public string? Cd_UsuarioCriacao { get; set; }

        [Column("DT_ATUALIZACAO", TypeName = "DATE")]
        public DateTime? Dt_Atualizacao { get; set; }

        [Column("CD_USUARIO_ATUALIZACAO", TypeName = "VARCHAR(30)")]
        [MaxLength(30)]
        public string? Cd_UsuarioAtualizacao { get; set; }

        [Display(Name = "Período de Teste")]
        [Column("PERIODO_TESTE", TypeName = "BIT", Order = 60)]
        public bool Periodo_Teste { get; set; }

        [Display(Name = "Vencimento do Período")]
        [Column("VENCIMENTO_PERIODO_TESTE", TypeName = "DATE", Order = 65)]
        public DateTime? Vencimento_Periodo_Teste { get; set; }

        [NotMapped]
        public int? StatusFilter { get; set; }

        [NotMapped]
        public virtual ICollection<Empresa>? Empresas { get; } = null;
    }
}
