using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppAwm.Models
{
    [Table("AWM_FUNCIONARIO_VINCULO_OBRA", Schema = "dbo")]
    public class FuncionarioVinculoObra
    {
        [Key]
        [Column("ID", TypeName = "int", Order = 1)]
        public int Id { get; set; }

        [Column("CD_FUNCIONARIO_ID", TypeName = "int", Order = 5)]
        public int Cd_Funcionario_Id { get; set; }

        [Column("CD_EMPRESA_ID", TypeName = "int", Order = 10)]
        public int Cd_Empresa_Id { get; set; }

        [Column("CD_OBRA_ID", TypeName = "int", Order = 15)]
        public int Cd_Obra_Id { get; set; }

        [Column("VINCULADO", Order = 20)]
        public bool Vinculado { get; set; }

        [Column("DT_VINCULADO", TypeName = "DATE", Order = 25)]
        public DateTime? DataVinculado { get; set; }

        [Column("DT_DESVINCULADO", TypeName = "DATE", Order = 30)]
        public DateTime? DataDesvinculado { get; set; }

        [Column("CD_USUARIO_CRIACAO", TypeName = "VARCHAR(30)", Order = 35)]
        [MaxLength(30)]
        public string? Cd_UsuarioCriacao { get; set; }

        [Column("CD_USUARIO_ATUALIZACAO", TypeName = "VARCHAR(30)", Order = 40)]
        [MaxLength(30)]
        public string? Cd_UsuarioAtualizacao { get; set; }

        public virtual Funcionario? Funcionario { get; }

    }
}
