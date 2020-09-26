using App.API.Controllers.Shared;
using App.API.Models.Uploaders;
using App.API.Utils.Extensions;
using App.Domain.Utils.Settings;
using App.Infrastructure.Utils.Helpers.Models.Shared.Cropper;
using App.Infrastructure.Utils.Helpers.Uploaders;
using App.Infrastructure.Utils.Helpers.Uploaders.Exceptions;
using App.Infrastructure.Utils.StaticContent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace App.API.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class UploadController : BaseController
    {
        private readonly IUploadService _uploadService;

        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        [HttpPost]
        [Route("Image")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Image([FromForm] FileUploadDTO model)
        {
            try
            {
                if (model != null)
                {
                    var name = _uploadService.Image(model.File);

                    var fullpath = Path.Combine(AppSettings.UploadPath, name);

                    return Ok(fullpath);
                }

                var errors = ModelState.GetErrors();
                return UnprocessableEntity(errors);

            }
            catch (FileExtensionException ex)
            {
                Logger.LogError(ex.ToString());
                ModelState.AddModelError("File", "File.Invalid");

                var errors = ModelState.GetErrors();
                return UnprocessableEntity(errors);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("CropImage")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult CropImage([FromForm] CropperOptions cropperOptions, string folder = "")
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var name = _uploadService.CropImage(cropperOptions, folder);

                    var fullpath = Path.Combine(AppSettings.UploadPath, name);

                    return Ok(fullpath);
                }

                var errors = ModelState.GetErrors();

                return UnprocessableEntity(errors);
            }
            catch (FileExtensionException ex)
            {
                Logger.LogError(ex.ToString());
                ModelState.AddModelError("Image", "Image.Invalid");

                var errors = ModelState.GetErrors();
                return UnprocessableEntity(errors);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("File")]
        public IActionResult File([FromForm] FileUploadDTO model)
        {
            try
            {
                if (model != null)
                {
                    //check file extension
                    var name = _uploadService.File(model.File);

                    var data = new
                    {
                        fullPath = $"{AppSettings.UploadPath}/{name}",
                        name,
                    };

                    return Ok(data);
                }

                var errors = ModelState.GetErrors();
                return UnprocessableEntity(errors);

            }
            catch (FileExtensionException ex)
            {
                Logger.LogError(ex.ToString());
                ModelState.AddModelError("File", "File.Invalid");

                var errors = ModelState.GetErrors();
                return UnprocessableEntity(errors);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                return BadRequest();
            }
        }
    }
}
