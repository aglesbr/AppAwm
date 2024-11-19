﻿using AppAwm.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppAwm.Models
{
    public class Login
    {
        [Column("TIPOPERFIL", TypeName = "INT")]
        [Range(1, 32, ErrorMessage = "Selecione um perfil.")]
        [Display(Name = "Perfil")]
        public EnumPerfil Perfil { get; set; }

        [Display(Name = "Usuário")]
        [Column("LOGIN", TypeName = "VARCHAR(15)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe seu usuário.")]
        public string? UserName { get; set; }

        [Display(Name = "Senha")]
        [Column("PASSWORD ", TypeName = "VARCHAR(100)")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe uma senha.")]
        public string? Password { get; set; }

        public bool Operacao { get; set; }
    }
}
