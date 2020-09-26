using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace App.API.Models.Uploaders
{
    public class FileUploadDTO
    {
        [Required(ErrorMessage = "File.Required")]
        public IFormFile File { get; set; }
    }
}
