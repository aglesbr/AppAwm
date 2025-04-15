using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppAwm.Models
{
    [Table("AWM_VIDEO")]
    public class Video
    {
        [Key]
        [Column("CD_VIDEO", TypeName = "INT", Order = 1)]
        public int Cd_Video { get; set; }

        [Display(Name = "Titulo do Video")]
        [Column("TITULO", TypeName = "VARCHAR(70)", Order = 5)]
        [MaxLength(length: 70, ErrorMessage = "Máximo de caracter excedido para o Título")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o titulo do video")]
        public string? Titulo { get; set; }

        [Display(Name = "Descrição do Video")]
        [Column("DESCRICAO", TypeName = "VARCHAR(150)", Order = 10)]
        [MaxLength(length: 150, ErrorMessage = "Máximo de caracter excedido para a Descrição")]
        public string? Descricao { get; set; }

        [Display(Name = "Link do Video")]
        [Column("URL", TypeName = "VARCHAR(100)", Order = 15)]
        [MaxLength(length: 100, ErrorMessage = "Máximo de caracter excedido para o Link")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o link do video")]
        public string? Url { get; set; }

        [Column("STATUS", TypeName = "INT", Order = 20)]
        public bool Status { get; set; }

        [NotMapped]
        public int? StatusFilter { get; set; }
    }
}

