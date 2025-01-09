using AppAwm.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAwm.Models
{
    [Table("AWM_USUARIO")]
    public class Usuario
    {

        [Key]
        [Column("CD_USUARIO", TypeName = "INT")]
        public int Cd_Usuario { get; set; }

        [Column("TIPOPERFIL", TypeName = "INT")]
        [Range(1, 32, ErrorMessage = "Selecione um perfil.")]
        [Display(Name = "Perfil")]
        public EnumPerfil Perfil { get; set; }

        [Display(Name = "Nome")]
        [Column("NOME", TypeName = "VARCHAR(100)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o nome do usuário.")]
        public string? Nome { get; set; }

        [Display(Name = "Cpf")]
        [Column("DOCUMENTO", TypeName = "VARCHAR(11)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o seu Cpf.")]
        public string? Documento { get; set; }

        [Display(Name = "Usuário")]
        [Column("LOGIN", TypeName = "VARCHAR(15)")]
        public string? Login { get; set; }

        [Display(Name = "Senha")]
        [Column("SENHA", TypeName = "VARCHAR(100)")]
        [StringLength(15, ErrorMessage = "Deve conter pelo menos 6 caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe uma senha.")]
        public string? Senha { get; set; }

        [NotMapped]
        [Display(Name = "Confirmar Senha")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "As senhas informada não coincidem.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe uma senha.")]
        public string? ConfirmPassword { get; set; }

        [Display(Name = "E-mail")]
        [Column("EMAIL", TypeName = "VARCHAR(100)")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail inválido.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o e-mail.")]
        public string? Email { get; set; }

        [Display(Name = "Celular")]
        [Column("TELEFONE", TypeName = "VARCHAR(16)")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Telefone inválido.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o telefone para contato.")]
        [StringLength(16, ErrorMessage = "Informe o número completo", MinimumLength = 15)]
        public string? Telefone { get; set; }

        [Column("MUDARSENHA")]
        public bool MudarSenha { get; set; }

        [Column("STATUS")]
        public bool Status { get; set; }

        [Column("DT_CRIACAO", TypeName = "DATETIME")]
        public DateTime Dt_Criacao { get; set; } = DateTime.Now.Date;

        [Column("CD_USUARIO_CRIACAO", TypeName = "VARCHAR(30)")]
        [MaxLength(30)]
        public string? Cd_Usuario_Criacao { get; set; }

        [Column("DT_ATUALIZACAO", TypeName = "DATETIME")]
        public DateTime? Dt_Atualizacao { get; set; }

        [Column("CD_USUARIO_ATUALIZACAO", TypeName = "VARCHAR(30)")]
        [MaxLength(30)]
        public string? Cd_Usuario_Atualizacao { get; set; }

        [Column("CD_EMPERSA", TypeName = "INT")]
        public int Cd_Empresa { get; set; }

        [NotMapped]
        public Empresa? Empresa { get; set; }

        [NotMapped]
        public Cliente? Cliente { get; set; }

        [NotMapped]
        public int? StatusFilter { get; set; }

    }
}
