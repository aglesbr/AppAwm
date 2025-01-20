using AppAwm.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAwm.Models
{
    public class Login
    {
        [Column("TIPOPERFIL", TypeName = "INT")]
        [Range(1, 32, ErrorMessage = "Selecione um perfil.")]
        [Display(Name = "Perfil")]
        public EnumPerfil Perfil { get; set; }

        [Display(Name = "Cpf")]
        [Column("LOGIN", TypeName = "VARCHAR(15)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe seu usuário.")]
        public string? UserName { get; set; }

        [Display(Name = "Senha")]
        [Column("PASSWORD ", TypeName = "VARCHAR(100)")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe uma senha.")]
        public string? Password { get; set; }

        public MessageResposta Message { get; set; } = new();

        public bool Operacao { get; set; }


    }

    public class MessageResposta()
    {
        public bool Success { get; set; }

        public string? Message { get; set; }
    }
}
