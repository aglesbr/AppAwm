using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAwm.Models
{
    [Table("AWM_FUNCIONARIO", Schema = "dbo")]
    public class Funcionario
    {

        [Key]
        [Column("CD_FUNCIONARIO", TypeName = "INT", Order = 1)]
        public int Cd_Funcionario { get; set; }

        [Column("ID_EMPRESA", TypeName = "INT", Order = 2)]
        [Range(1, 1000, ErrorMessage = "Selecione uma empresa")]
        public int Id_Empresa { get; set; }

        [Display(Name = "Nome *")]
        [Column("NOME", TypeName = "VARCHAR(100)", Order = 3)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o nome do colaborador.")]
        public string? Nome { get; set; }

        [Display(Name = "Nascimento *")]
        [Column("NASCIMENTO", TypeName = "DATE", Order = 4)]
        [Range(typeof(DateTime), "1/1/1900", "1/1/2099", ErrorMessage = "Informe a data de nascimento.")]
        public DateTime Nascimento { get; set; }

        [Display(Name = "Cpf *")]
        [Column("DOCUMENTO", TypeName = "VARCHAR(15)", Order = 5)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o Cpf.")]
        public string? Documento { get; set; }

        [Display(Name = "Sexo")]
        [Column("SEXO", TypeName = "VARCHAR(1)", Order = 15)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Selecione o sexo.")]
        public string? Sexo { get; set; }

        [Display(Name = "Estrangeiro")]
        [Column("ESTRANGEIRO", TypeName = "BIT", Order = 16)]
        public bool Estrangeiro { get; set; }

        [Display(Name = "Estado Civil")]
        [Column("ESTADOCIVIL", TypeName = "INT", Order = 20)]
        public int EstadoCivil { get; set; }

        [Display(Name = "Tel. Cel")]
        [Column("TELEFONE", TypeName = "VARCHAR(15)", Order = 25)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o telefone.")]
        public string? Telefone { get; set; }

        [Display(Name = "Escolaridade")]
        [Column("ESCOLARIDADE", TypeName = "INT", Order = 30)]
        [Range(1,50, ErrorMessage = "Selecione o grau de escolaridade.")]
        public int Escolaridade { get; set; }

        [Display(Name = "Tipo de Contrato")]
        [Column("TIPOCONTRATO", TypeName = "INT", Order = 35)]
        [Range(1,20, ErrorMessage = "Selecione o tipo do contrato.")]
        public int TipoContrato { get; set; }

        [Display(Name = "Cargo|Função *")]
        [Column("CARGO", TypeName = "INT", Order = 40)]
        [Range(1, 20000, ErrorMessage = "Informe o Cargo/Função.")]
        public int Cd_Cargo { get; set; }

        [Display(Name = "Pcd")]
        [Column("PCD",  Order = 46)]
        public bool Pcd { get; set; }

        [Display(Name = "Admissão *")]
        [Column("DT_ADMISSAO", TypeName = "DATE", Order = 55)]
        public DateTime Dt_Admissao{ get; set; } = DateTime.Now.Date;

        [Column("DT_CRIACAO", TypeName = "DATE", Order = 60)]
        public DateTime Dt_Criacao { get; set; } = DateTime.Now;

        [Column("CD_USUARIO_CRIACAO", TypeName = "VARCHAR(30)", Order = 65)]
        [MaxLength(30)]
        public string? Cd_UsuarioCriacao { get; set; }

        [Column("DT_ATUALIZACAO", TypeName = "DATE", Order = 70)]
        public DateTime? Dt_Atualizacao { get; set; }

        [Column("CD_USUARIO_ATUALIZACAO", TypeName = "VARCHAR(30)", Order = 75)]
        [MaxLength(30)]
        public string? Cd_UsuarioAtualizacao { get; set; }

        [Column("STATUS", Order = 80)]
        public bool Status { get; set; }

        [Display(Name = "Observações")]
        [Column("OBSERVACAO", TypeName = "VARCHAR(200)", Order = 85)]
        public string? Observacao { get; set; }


        [Column("FOTO", TypeName = "BINARY", Order = 90)]
        public byte[]? Foto { get; set; }

        public virtual Empresa? Empresa { get; set; }

        public virtual Endereco Endereco { get; set; } =  new();

        [NotMapped]
        public virtual Cargo? Cargo { get; set; } = null;

        public virtual ICollection<FuncionarioVinculoObra>? VinculoObras { get; set; }

        [NotMapped]
        public virtual ICollection<Anexo>? Anexos { get; set; }

       [NotMapped]
        public int? StatusFilter { get; set; }
    }
}
