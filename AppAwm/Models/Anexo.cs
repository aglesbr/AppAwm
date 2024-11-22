﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AppAwm.Models.Enum;

namespace AppAwm.Models
{
    [Table("AWM_ANEXO", Schema = "dbo")]
    public class Anexo
    {
        [Key]
        [Column("CD_ANEXO", TypeName = "INT")]
        public int Cd_Anexo { get; set; }

        [Column("CD_EMPRESA_ID", TypeName = "INT")]
        public int? Cd_Empresa_Id { get; set; }

        [Column("CD_FUNCIONARIO_ID", TypeName = "INT")]
        public int? Cd_Funcionario_Id { get; set; }

        [Column("NOME", TypeName = "VARCHAR(50)")]
        public string? Nome { get; set; }

        [Display(Name = "Descrição")]
        [Column("DESCRICAO", TypeName = "VARCHAR(300)")]
        public string? Descricao { get; set; }

        [Display(Name = "Anexo")]
        [Column("ANEXO", TypeName = "VARBINARY(MAX)")]
        public byte[]? Arquivo { get; set; }

        [Column("DT_CRIACAO", TypeName = "DATE")]
        public DateTime Dt_Criacao { get; set; } = DateTime.Now;

        [Column("DT_VALIDADE_DOCUMENTO", TypeName = "DATE")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o prazo de validade.")]
        public DateTime Dt_Validade_Documento { get; set; }

        [Column("CD_USUARIO_CRIACAO", TypeName = "VARCHAR(50)")]
        [MaxLength(50)]
        public string? Cd_UsuarioCriacao { get; set; }

        [Column("ID_USUARIO_CRIACAO", TypeName = "INT")]
        public int Id_UsuarioCriacao { get; set; }

        [Column("CD_USUARIO_ANALISTA", TypeName = "VARCHAR(50)")]
        [MaxLength(50)]
        public string? Cd_UsuarioAnalista { get; set; }

        [Display(Name = "Motivo")]
        [Column("MOTIVO_REJEICAO", TypeName = "VARCHAR(100)")]
        public string? MotivoRejeicao { get; set; }

        [Display(Name = "Resalva")]
        [Column("MOTIVO_RESALVA", TypeName = "VARCHAR(100)")]
        public string? MotivoResalva { get; set; }

        [Display(Name = "Tipo do documento")]
        [Column("TIPO_ANEXO", TypeName = "VARCHAR(4)")]
        public string? TipoAnexo { get; set; }

        [Display(Name = "Status")]
        public EnumStatusDocs? Status { get; set; }

        [NotMapped]
        public virtual Funcionario? Funcionario  { get; set; }

        [NotMapped]
        public virtual Empresa? Empresa { get;}
    }
}