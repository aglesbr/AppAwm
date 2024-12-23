﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAwm.Models
{
    [Table("AWM_EMPRESA", Schema = "dbo")]
    public class Empresa
    {
        [Key]
        [Column("CD_EMPRESA", TypeName = "int", Order = 1)]
        public int Cd_Empresa { get; set; }

        [Display(Name = "CNPJ")]
        [Column("CNPJ", TypeName = "VARCHAR(14)", Order = 2)]
        [RegularExpression(@"(^\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}$)", ErrorMessage = "CNPJ inválidos.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o CNPJ da empresa.")]
        public string? Cnpj { get; set; }

        [Display(Name = "Empresa")]
        [Column("NOME", TypeName = "VARCHAR(100)", Order = 3)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o nome da empresa")]
        public string? Nome { get; set; }

        [Display(Name = "Equity")]
        [Column("EQUITY", TypeName = "INT", Order = 4)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o nome da empresa")]
        public int Equity { get; set; } = 0;

        [Display(Name = "Nome Fantasia")]
        [Column("NOMEFANTASIA", TypeName = "VARCHAR(100)", Order = 5)]
        public string? NomeFantasia { get; set; }

        [Display(Name = "E-mail")]
        [Column("EMAIL", TypeName = "VARCHAR(100)", Order = 6)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o email da empresa")]
        public string? Email { get; set; }

        [Display(Name = "Telefone")]
        [Column("TELEFONE", TypeName = "VARCHAR(15)", Order = 8)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o telefone da empresa")]
        public string? Telefone { get; set; }

        [Column("STATUS", Order = 9)]
        public bool Status { get; set; }

        [Column("DT_CRIACAO", TypeName = "DATE", Order = 10)]
        public DateTime Dt_Criacao { get; set; } = DateTime.Now;

        [Column("CD_USUARIO_CRIACAO", TypeName = "VARCHAR(30)", Order = 11)]
        [MaxLength(30)]
        public string? Cd_UsuarioCriacao { get; set; }

        [Column("DT_ATUALIZACAO", TypeName = "DATE", Order = 12)]
        public DateTime? Dt_Atualizacao { get; set; }

        [Column("CD_USUARIO_ATUALIZACAO", TypeName = "VARCHAR(30)", Order = 13)]
        [MaxLength(30)]
        public string? Cd_UsuarioAtualizacao { get; set; }
        
        [Column("COMPLEMENTO", TypeName = "VARCHAR(5000)", Order = 14)]
        public string? Complemento { get; set; }
        
        public virtual Endereco Endereco { get; set; } = new();

        public virtual ICollection<Anexo>? Anexos { get; set; }

        public virtual ICollection<Funcionario> Funcionarios { get; } = [];

        public virtual ICollection<Obra> Obras { get; } = [];

        [NotMapped]
        public int? StatusFilter { get; set; }
    }
}
