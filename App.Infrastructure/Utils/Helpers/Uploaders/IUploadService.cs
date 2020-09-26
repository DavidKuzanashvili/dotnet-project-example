using App.Infrastructure.Utils.Helpers.Models.Shared.Cropper;
using Microsoft.AspNetCore.Http;

namespace App.Infrastructure.Utils.Helpers.Uploaders
{
    public interface IUploadService
    {
        string CropImage
        (
            CropperOptions settings,
            string aditionalFolder = "",
            string[] allowedExtensions = null
        );
        string Image
        (
            IFormFile data,
            string aditionalFolder = "",
            string[] allowedExtensions = null,
            int maxwidth = 1900
        );
        string File
        (
            IFormFile data,
            string aditionalFolder = "",
            string[] allowedExtensions = null
        );
        void Delete(string fileName, string additionalPath);
        string UploadBase64(string base64);
        string ConvertToBase64(string fileName);
    }
}
