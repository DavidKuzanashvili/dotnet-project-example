using App.API.Controllers.Shared;
using App.Domain.Interfaces.Info;
using App.Domain.Models.Info;
using App.Domain.Models.Shared.Pagination;
using App.Domain.Models.Shared.Queries;
using App.Infrastructure.Exceptions.Shared;
using App.Infrastructure.Utils.StaticContent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace App.API.Controllers
{
    public class BlogController : BaseController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        /// <summary>
        /// Get blogs admin privilages is required
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="400">If error</response>
        /// <response code="401">If unothorized</response>
        /// <response code="403">If forbidden</response>
        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(PaginationResponse<BlogDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAsync([FromQuery] PagingParameters pagination, [FromQuery] BlogQuery q)
        {
            try
            {
                var result = await _blogService.GetAsync(pagination, q);

                return Ok(result);

            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());

                return BadRequest();
            }
        }


        /// <summary>
        /// Get blogs admin privilages is required
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="400">If error</response>
        [HttpGet]
        [Route("localed")]
        [ProducesResponseType(typeof(PaginationResponse<BlogTranslatedDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetLocaledAsync([FromQuery] PagingParameters pagination,
            [FromQuery] BlogQuery q)
        {
            try
            {
                var result = await _blogService.GetLocaledAsync(pagination, q, LangCode);

                return Ok(result);

            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());

                return BadRequest();
            }
        }

        /// <summary>
        /// Get blog by slug
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="400">If error</response>
        /// <response code="401">If unothorized</response>
        /// <response code="403">If forbidden</response>
        [HttpGet("{slug}")]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(BlogDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetBySlugAsync(string slug)
        {
            try
            {
                var result = await _blogService.GetBySlugAsync(slug);

                return Ok(result);

            }
            catch (EntityNotFoundException ex)
            {
                Logger.LogError(ex.ToString());

                return NotFound();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());

                return BadRequest();
            }
        }


        /// <summary>
        /// Get blog by slug
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="400">If error</response>
        /// <response code="404">If entity not found</response>
        [HttpGet("localed/{slug}")]
        [ProducesResponseType(typeof(BlogTranslatedDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBySlugLocaledAsync(string slug)
        {
            try
            {
                var result = await _blogService.GetBySlugLocaledAsync(slug, LangCode);

                return Ok(result);

            }
            catch (EntityNotFoundException ex)
            {
                Logger.LogError(ex.ToString());

                return NotFound();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());

                return BadRequest();
            }
        }

        /// <summary>
        /// Creats blog, admin privilages is requred
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="400">If error</response>
        /// <response code="401">If unothorized</response>
        /// <response code="403">If forbidden</response>
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(BlogTranslatedDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PostAsync([FromBody] BlogDTO model)
        {
            try
            {
                var result = await _blogService.CreateAsync(model);

                return Ok(result);

            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());

                return BadRequest();
            }
        }

        /// <summary>
        /// Update blog, admin privilages is requred
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="400">If error</response>
        /// <response code="401">If unothorized</response>
        /// <response code="403">If forbidden</response>
        /// <response code="404">If forbidden</response>
        [HttpPut]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(BlogTranslatedDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutAsync([FromBody] BlogDTO model)
        {
            try
            {
                var result = await _blogService.UpdateAsync(model);

                return Ok(result);

            }
            catch (EntityNotFoundException ex)
            {
                Logger.LogError(ex.ToString());

                return NotFound();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());

                return BadRequest();
            }
        }


        /// <summary>
        /// Delete blog, admin privilages is requred
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="400">If error</response>
        /// <response code="401">If unothorized</response>
        /// <response code="403">If forbidden</response>
        /// <response code="404">If forbidden</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(BlogTranslatedDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var result = await _blogService.DeleteByIdAsync(id);

                return Ok(result);

            }
            catch (EntityNotFoundException ex)
            {
                Logger.LogError(ex.ToString());

                return NotFound();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());

                return BadRequest();
            }
        }
    }
}
