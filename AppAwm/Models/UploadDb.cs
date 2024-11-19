using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AppAwm.Models
{


    public class AppFile
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }
    }

    public class BufferedSingleFileUploadDbModel : PageModel
    {

        [BindProperty]
        public BufferedSingleFileUploadDb FileUpload { get; set; }

}

    public class BufferedSingleFileUploadDb
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
    }
}
