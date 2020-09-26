using App.API.Controllers.Shared;
using App.API.Utils.Extensions;
using App.Domain.Interfaces.Users;
using App.Domain.Models.Shared.Pagination;
using App.Domain.Models.Shared.Queries;
using App.Domain.Models.Users;
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
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get registered users
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="400">If error</response>
        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(PaginationResponse<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync([FromQuery] PagingParameters paging,
            [FromQuery] CommonQuery query)
        {
            try
            {
                var userSettings = GetUserSettings();

                var result = await _userService.GetAsync(paging, query, userSettings.UserId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());

                return BadRequest();
            }
        }

        /// <summary>
        /// Get user info
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="400">If error</response>
        [HttpGet("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync(string id)
        {
            try
            {
                var result = await _userService.GetByIdAsync(id);

                return Ok(result);
            }
            catch (EntityNotFoundException ex)
            {
                Logger.LogError(ex.ToString());
                ModelState.AddModelError("NotFound", "Entity.NotFound");

                var errors = ModelState.GetErrors();
                return NotFound(errors);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());

                return BadRequest();
            }
        }

        /// <summary>
        /// Get profile info
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        /// <response code="400">If error</response>
        [HttpGet()]
        [Authorize(Roles = "admin,user")]
        [Route("profile")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProfileAsync()
        {
            try
            {
                var signedInUser = GetUserSettings();

                var result = await _userService.GetByIdAsync(signedInUser.UserId);

                return Ok(result);
            }
            catch (EntityNotFoundException ex)
            {
                Logger.LogError(ex.ToString());
                ModelState.AddModelError("NotFound", "Entity.NotFound");

                var errors = ModelState.GetErrors();
                return NotFound(errors);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());

                return BadRequest();
            }
        }
    }
}
