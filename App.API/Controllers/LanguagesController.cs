using App.API.Controllers.Shared;
using App.Domain.Entities.Languages;
using App.Domain.Interfaces.Languages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace App.API.Controllers
{
    public class LanguagesController : BaseController
    {
        private readonly ILanguageService _languageService;

        public LanguagesController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        /// <summary>
        /// Get registered langugages
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="400">If error</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Language>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get()
        {
            try
            {
                var result = _languageService.Get();

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());

                return BadRequest();
            }
        }
    }
}
