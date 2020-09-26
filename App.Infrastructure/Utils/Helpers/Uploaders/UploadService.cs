using App.Infrastructure.Utils.Helpers.Models.Shared.Cropper;
using App.Infrastructure.Utils.Helpers.Uploaders.Exceptions;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace App.Infrastructure.Utils.Helpers.Uploaders
{
    public class UploadService : IUploadService
    {
        private string _uploadPath;
        private readonly string[] _allowedExtensions = { ".jpeg", ".jpg", ".png" };
        public UploadService() { }

        public void SetUploadPath(string uploadPath)
        {
            _uploadPath = uploadPath;
        }

        public string Image
        (
            IFormFile image,
            string aditionalFolder = "",
            string[] allowedExtensions = null,
            int maxwidth = 1900
        )
        {
            allowedExtensions ??= _allowedExtensions;
            var extension = System.IO.Path.GetExtension(image.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                throw new FileExtensionException();
            }
            string filename = System.IO.Path.GetFileNameWithoutExtension(image.FileName);
            filename = filename + "_" + Guid.NewGuid().ToString().Split('-').First();
            filename = ToSHA256(filename);
            filename = FixName(filename) + extension;

            var fullPath = System.IO.Path.Combine(_uploadPath, aditionalFolder, filename);

            using Image loadedImage = SixLabors.ImageSharp.Image.Load(image.OpenReadStream());
            int width = loadedImage.Width;
            int height = loadedImage.Height;
            bool resize = false;
            if (loadedImage.Width >= loadedImage.Height && loadedImage.Width > 1900)
            {
                double dif = loadedImage.Width / 1900.0;
                width = 1900;

                height = Convert.ToInt32(loadedImage.Height / dif);
                resize = true;
            }
            else if (loadedImage.Height > loadedImage.Width && loadedImage.Height > 1900)
            {
                double dif = loadedImage.Height / 1900.0;
                height = 1900;

                width = Convert.ToInt32(loadedImage.Width / dif);
                resize = true;
            }

            if (resize) loadedImage.Mutate(x => x.Resize(width, height));


            if (extension == ".jpg" || extension == ".jpeg")
            {
                var encoder = new JpegEncoder()
                {
                    Quality = 90,
                };
                loadedImage.Save(fullPath, encoder);
            }
            else
            {
                loadedImage.Save(fullPath);
            }
            return filename;
        }

        public string CropImage
        (
            CropperOptions options,
            string aditionalFolder = "",
            string[] allowedExtensions = null
        )
        {
            var extension = System.IO.Path.GetExtension(options.Image.FileName).ToLower();
            string filename = System.IO.Path.GetFileNameWithoutExtension(options.Image.FileName);
            filename = filename + "_" + Guid.NewGuid().ToString().Split('-').First();
            filename = ToSHA256(filename);
            filename = FixName(filename) + extension;

            var fullPath = System.IO.Path.Combine(_uploadPath, aditionalFolder, filename);

            using Image image = SixLabors.ImageSharp.Image.Load(options.Image.OpenReadStream());
            image.Mutate(x => x.Crop(new Rectangle(options.X, options.Y, options.Width, options.Height)));

            var resizeWidth = 0;
            var resizeHeight = 0;

            if (options.Width >= options.Height && options.Width >= options.MaxSize)
            {
                double dif = options.Width / (1.0 * options.MaxSize);
                resizeWidth = options.MaxSize;

                resizeHeight = Convert.ToInt32(options.Height / dif);
            }
            else if (options.Height > options.Width && options.Height >= options.MaxSize)
            {
                double dif = options.Height / (1.0 * options.MaxSize);
                resizeHeight = options.MaxSize;

                resizeWidth = Convert.ToInt32(options.Width / dif);
            }

            if (resizeWidth > 0 && resizeHeight > 0)
                image.Mutate(x => x.Resize(resizeWidth, resizeHeight));

            if (extension == ".jpg" || extension == ".jpeg")
            {
                var encoder = new JpegEncoder()
                {
                    Quality = 90,
                };
                image.Save(fullPath, encoder);
            }
            else
            {
                image.Save(fullPath);
            }
            return filename;
        }

        public string File
        (
            IFormFile data,
            string aditionalFolder = "",
            string[] allowedExtensions = null
        )
        {
            if (data == null)
            {
                return "";
            }
            var extension = System.IO.Path.GetExtension(data.FileName).ToLower();
            string filename = System.IO.Path.GetFileNameWithoutExtension(data.FileName);
            filename = filename + "_" + Guid.NewGuid().ToString().Split('-').First();
            filename = ToSHA256(filename);
            filename = FixName(filename) + extension;

            var fullPath = System.IO.Path.Combine(_uploadPath, aditionalFolder, filename);

            using (System.IO.FileStream output = System.IO.File.Create(fullPath))
                data.CopyToAsync(output);

            return filename;
        }

        public void Delete(string fileName, string additionalPath)
        {
            var fullPath = Path.Combine(_uploadPath, additionalPath, fileName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }

        public string UploadBase64(string base64)
        {
            if (!base64.Contains(","))
            {
                throw new InvalidBase64FormatException("Invalid.Base64Format");
            }

            var base64Info = base64.Split(',');
            var metaInfo = base64Info[0];
            var imageBase64 = base64Info[1];

            var uniqueName = Guid.NewGuid().ToString();
            var fileName = ToSHA256(uniqueName);
            var extension = metaInfo.Split('/')[1].Split(';')[0];
            fileName = FixName(fileName) + '.' + extension;
            var fullPath = Path.Combine(_uploadPath, fileName);

            byte[] imageBytes = Convert.FromBase64String(imageBase64);
            System.IO.File.WriteAllBytes(fullPath, imageBytes);

            return fileName;
        }

        public string ConvertToBase64(string fileName)
        {
            try
            {
                var extension = Path.GetExtension(fileName);
                var fullPath = Path.Combine(_uploadPath, fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(fullPath);
                    return $"data:image/{extension};base64," + Convert.ToBase64String(imageBytes);
                }
                throw new Exception($"File not found! - {fileName}");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        private string FixName(string name)
        {
            name = Regex.Replace(name, @"[^a-zA-Zა-ჰ0-9\s-]", "-");
            name = Regex.Replace(name, @"\s+", " ").Trim();
            name = name.Substring(0, name.Length <= 100 ? name.Length : 100).Trim();
            name = Regex.Replace(name, @"\s", "-");
            name = Regex.Replace(name, @"\-+", "-").Trim();
            name = Regex.Replace(name, @"\-$", "").Trim();

            return name;
        }

        static string ToSHA256(string rawData)
        {
            using SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

}
