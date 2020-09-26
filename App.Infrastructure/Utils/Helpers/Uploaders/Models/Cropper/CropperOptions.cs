using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace App.Infrastructure.Utils.Helpers.Models.Shared.Cropper
{
    public class CropperOptions
    {
        public CropperOptions()
        {
            MaxSize = 1900;
        }

        [Required]
        public IFormFile Image { get; set; }
        public string FieldName { get; set; }
        public string Format { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int ResizeWidth { get; set; }
        public int MaxSize { get; set; }
        public int Rotate { get; set; }
    }
}
