using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAwm.Models
{
    [Table("AWM_ENDERECO", Schema = "dbo")]
    public class Endereco
    {

        [Key]
        [Column("CD_ENDERECO", TypeName = "INT")]
        public int Cd_Endereco { get; set; }

        [Column("CD_EMPRESA", TypeName = "INT")]
        public int? Cd_Empresa { get; set; }

        [Column("CD_FUNCIONARIO", TypeName = "INT")]
        public int? Cd_Funcionario_id { get; set; }

        [Display(Name = "Logradouro")]
        [Column("LOGRADOURO", TypeName = "VARCHAR(100)")]
        public string? Logradouro { get; set; }

        [Display(Name = "Número")]
        [Column("NUMERO", TypeName = "VARCHAR(15)")]
        [Required(ErrorMessage = "Informe o número da residencia.")]
        public string? Numero { get; set; }

        [Display(Name = "Referência")]
        [Column("DETALHES", TypeName = "VARCHAR(100)")]
        public string? Detalhes { get; set; }

        [Display(Name = "Bairro")]
        [Column("BAIRRO", TypeName = "VARCHAR(50)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o nome do bairro.")]
        public string? Bairro { get; set; }

        [Display(Name = "Cidade")]
        [Column("CIDADE", TypeName = "VARCHAR(50)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o nome da cidade.")]
        public string? Cidade { get; set; }

        [Display(Name = "Estado")]
        [Column("ESTADO", TypeName = "VARCHAR(2)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o UF.")]
        public string? Estado { get; set; }

        [Display(Name = "Cep")]
        [Column("CEP", TypeName = "VARCHAR(10)")]
        [RegularExpression(@"(^\d{5}\-\d{3}$)", ErrorMessage = "Cep inválido.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o Cep.")]
        public string? Cep { get; set; }

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

        [NotMapped]
        public string? Uf { get; set; }

        public virtual Empresa? Empresa { get; set; }

        public virtual Funcionario? Funcionario { get; set; }

        
    }
}
