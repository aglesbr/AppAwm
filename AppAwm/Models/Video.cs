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
        [Column("TITULO", TypeName = "VARCHAR(100)", Order = 5)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o titulo do video")]
        public string? Titulo { get; set; }

        [Display(Name = "Descrição do Video")]
        [Column("DESCRICAO", TypeName = "VARCHAR(100)", Order = 10)]
        public string? Descricao { get; set; }

        [Display(Name = "Link do Video")]
        [Column("URL", TypeName = "VARCHAR(200)", Order = 15)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o link do video")]
        public string? Url { get; set; } 

        [Column("STATUS", TypeName = "INT", Order = 20)]
        public bool Status { get; set; }

        [NotMapped]
        public int? StatusFilter { get; set; }
    }
}

