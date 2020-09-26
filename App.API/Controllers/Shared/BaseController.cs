using App.Domain.Utils.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;

namespace App.API.Controllers.Shared
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected string LangCode => HttpContext.Request.Headers["Accept-Language"];

        protected ILogger<BaseController> Logger =>
            HttpContext.RequestServices.GetService<ILogger<BaseController>>();

        protected UserSettings GetUserSettings()
        {
            var userId = User.FindFirstValue("sub");

            var result = new UserSettings()
            {
                UserId = userId,
                Roles = User.FindAll(ClaimTypes.Role)
                    .Select(x => x.Value)
                    .ToArray()
            };

            return result;
        }
    }
}
